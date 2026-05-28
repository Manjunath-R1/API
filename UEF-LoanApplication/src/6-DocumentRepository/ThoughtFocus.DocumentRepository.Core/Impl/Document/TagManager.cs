using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Document;
using ThoughtFocus.DocumentRepository.Domain.Enumeration;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain.Response;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.Common.Exceptions.BusinessException;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class TagManager : ITagManager
    {
        private readonly ILogger<TagManager> _logger;
        private IAuthorizer _authorizer;
        private IDocumentInformationProvider _documentInformationProvider;
        private ITagRepository _tagRepository;
        private IActionLogger _actionLogger;
        private IDocumentTagValueValidator _documentTagValueValidator;
        private IDocumentTagRepository _documentTagRepository;
        private ITagValueRepository _tagValueRepository;
        private IUserRepository _userProvider;
        private RepositoryUser repositoryUser = null;
        private IAccessRoleRepository _accessRoleRepository;

        public TagManager(IAuthorizer authorizer, IDocumentInformationProvider documentInformationProvider,
            ITagRepository tagRepository, IActionLogger actionLogger,
            IDocumentTagValueValidator documentTagValueValidator, 
            IDocumentTagRepository documentTagRepository, ITagValueRepository tagValueRepository, 
            IUserRepository userProvider, IAccessRoleRepository accessRoleRepository,
            ILogger<TagManager> logger
            )
        {
            _authorizer = authorizer;
            _documentInformationProvider = documentInformationProvider;
            _tagRepository = tagRepository;
            _actionLogger = actionLogger;
            _documentTagValueValidator = documentTagValueValidator;
            _documentTagRepository = documentTagRepository;
            _tagValueRepository = tagValueRepository;
            _userProvider = userProvider;
            _accessRoleRepository = accessRoleRepository;
            _logger = logger;
        }

        public DocumentBaseResponse AddTag(TagRequest tagRequest)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            DateTime current = DateTime.Now;
            try
            {
                #region TagTypeValidation
                ValidationResponse tagTypeValidationResponse = this._documentInformationProvider.ValidateTagType(tagRequest.TagTypeID);
                if (!(tagTypeValidationResponse.IsSuccess && tagTypeValidationResponse.IsValid))
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "Tag type is invalid.";
                    return documentBaseResponse;

                }
                #endregion

                #region TagDuplicateValidation
                ValidationResponse tagDuplicateValidationResponse = this._documentInformationProvider.IsDuplicateTag(tagRequest.TagName);
                if (!(tagDuplicateValidationResponse.IsSuccess && !tagDuplicateValidationResponse.IsValid))
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "Tag with same name already exists.";
                    return documentBaseResponse;

                }
                #endregion

                #region Authorize
                //AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(tagRequest.LoggerInUserID, (long)RoleEnumeration.TagManager);
                //if (!authorizationResponse.IsAllowed)
                //{
                //    documentBaseResponse.IsSuccess = false;
                //    documentBaseResponse.Message = "You are not authorized to perform this action";
                //    return documentBaseResponse;
                //}
                #endregion

                Tag tag = new Tag();
                tag.CreatedByUserID = tagRequest.LoggerInUserID;
                tag.CreatedDateTime = current;
                tag.LastModifiedDateTime = current;
                tag.LastModifiedByUserID = tagRequest.LoggerInUserID;
                tag.IsActive = true;
                tag.Name = tagRequest.TagName;
                tag.TagTypeID = tagRequest.TagTypeID;
                tag.TagID = Guid.NewGuid();

                if (tag.TagTypeID == (int)TagTypeEnumeration.List && tagRequest.Values != null)
                {
                    tag.TagValues = new List<TagValue>();
                    foreach (var value in tagRequest.Values)
                    {
                        TagValue tagValue = new TagValue();
                        tagValue.CreatedByUserID = tagRequest.LoggerInUserID;
                        tagValue.LastModifiedByUserID = tagRequest.LoggerInUserID;
                        tagValue.CreatedDateTime = current;
                        tagValue.LastModifiedDateTime = current;
                        tagValue.IsActive = true;
                        tagValue.Value = value;
                        tag.TagValues.Add(tagValue);
                    }

                }

                this._tagRepository.Save(tag);
                documentBaseResponse.IsSuccess = true;
                documentBaseResponse.Message = "Tag added successfully.";

                //Activity log added with Tag
                ActivityLog activityLog = new ActivityLog();
                repositoryUser = _userProvider.GetUser(tagRequest.LoggerInUserID);
                if (repositoryUser != null)
                    activityLog.UserGuID = repositoryUser.UserGuID;
                activityLog.ActivityName = ActivityNameEnumeration.AddedContent.ToString();
                activityLog.NodeName = NodeNameEnumeration.Tag.ToString();
                activityLog.NodeKeyName = NodeKeyNameEnumeration.TagID.ToString();
                activityLog.KeyValue = tag.TagID.ToString();
                activityLog.Custom1 = tag.TagID.ToString();
                this._actionLogger.LogUserActivity(activityLog);

            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while adding tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while adding tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while adding tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentBaseResponse;
        }

        public TagListResponse GetTags(long userID)
        {
            TagListResponse tagListResponse = new TagListResponse();
            try
            {
                #region Authorize
                //AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(userID, (int)RoleEnumeration.TagManager);
                //if (!authorizationResponse.IsAllowed)
                //{
                //    tagListResponse.IsSuccess = false;
                //    tagListResponse.Message = "You are not authorized to perform this action";
                //    return tagListResponse;
                //}
                #endregion


                tagListResponse.Tags = this._tagRepository.GetAll();
                tagListResponse.IsSuccess = true;
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while fetching tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            return tagListResponse;
        }

        public DocumentBaseResponse ManageDocumentTags(List<DocumentTagViewModel> documentTagViewModel, long userID, long ImpersonatedUser)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            DateTime currentDate = DateTime.Now;
            DocumentTag documentTag = new DocumentTag();
            List<DocumentTag> documentTags = new List<DocumentTag>();
            try
            {

                #region Authorize
                AuthorizeRequest authorizeRequest = new AuthorizeRequest();
                repositoryUser = _userProvider.GetUser(ImpersonatedUser);
                if (repositoryUser.UserGuID != Guid.Empty)
                    authorizeRequest.LoggedInUserID = repositoryUser.UserGuID;
                authorizeRequest.ContentNodeName = NodeNameEnumeration.Document.ToString();
                authorizeRequest.ContentKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                authorizeRequest.ContentID = documentTagViewModel.Select(x => x.DocumentID).LastOrDefault();
                authorizeRequest.ActionName = ActionNameEnumeration.Modify.ToString();

                AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(authorizeRequest);
                if (!authorizationResponse.IsAllowed)
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "You are not authorized to perform this action";
                    return documentBaseResponse;
                }
                #endregion Authorize

                if (documentTagViewModel != null)
                {
                    foreach (var documentTagModel in documentTagViewModel)
                    {
                        ValidationResponse validationResponse = new ValidationResponse();
                        if (documentTagModel.DocumentTagID == Guid.Empty && !documentTagModel.IsRemoved)
                        {
                            #region DocumentStateValidation
                            ValidationResponse stateValidationResponse = this._documentInformationProvider.CanModifyDocumentState(documentTagModel.DocumentID, userID);
                            if (!(stateValidationResponse.IsSuccess && stateValidationResponse.IsValid))
                            {

                                documentBaseResponse.IsSuccess = false;
                                documentBaseResponse.Message = "The file is locked by some other user. Please try after sometime.";
                                return documentBaseResponse;
                            }
                            #endregion


                            #region ValidateTagValue
                            validationResponse = this._documentTagValueValidator.ValidateTagValue(documentTagModel);
                            if (!(validationResponse.IsSuccess && validationResponse.IsValid))
                            {
                                documentBaseResponse.IsSuccess = false;
                                documentBaseResponse.IsValid = false;
                                documentBaseResponse.Message = String.Format("Value entered for tag-{0} is invalid.The allowed values should be of type {1}", documentTagModel.TagName, documentTagModel.TagTypeName);
                                return documentBaseResponse;
                            }
                            #endregion

                            #region DuplicateTag
                            //documentTag = this._documentTagRepository.FirstOrDefault(a => a.DocumentID == documentTagModel.DocumentID && a.IsActive == true && a.TagID == documentTagModel.TagID && a.Value == documentTagModel.Value);

                            documentTag = _documentTagRepository.GetTagById(documentTagModel.DocumentID, documentTagModel.TagID);
                            if (documentTag != null)
                            {
                                documentBaseResponse.IsSuccess = false;
                                documentBaseResponse.IsValid = false;
                                documentBaseResponse.Message = String.Format("Tag {0} already exists for this document", documentTagModel.TagName);
                                return documentBaseResponse;
                            }
                            #endregion

                            documentTag = new DocumentTag();
                            documentTag.DocumentTagID = Guid.NewGuid();
                            documentTag.CreatedByUserID = userID;
                            documentTag.CreatedDateTime = currentDate;
                            documentTag.LastModifiedByUserID = userID;
                            documentTag.LastModifiedDateTime = currentDate;
                            documentTag.IsActive = true;
                            documentTag.IsDefault = false;
                            documentTag.DocumentID = documentTagModel.DocumentID;
                            documentTag.TagID = documentTagModel.TagID;
                            documentTag.Value = documentTagModel.Value;
                            documentTags.Add(documentTag);


                        }
                        else if (documentTagModel.IsRemoved && documentTagModel.DocumentTagID != Guid.Empty)
                        {
                            #region DocumentStateValidation
                            ValidationResponse stateValidationResponse = this._documentInformationProvider.CanModifyDocumentState(documentTagModel.DocumentID, userID);
                            if (!(stateValidationResponse.IsSuccess && stateValidationResponse.IsValid))
                            {

                                documentBaseResponse.IsSuccess = false;
                                documentBaseResponse.Message = "The file is locked by some other user. Please try after sometime.";
                                return documentBaseResponse;
                            }
                            #endregion

                            #region ValidateDefaultTag
                            if (documentTagModel.IsDefault)
                            {
                                documentBaseResponse.IsSuccess = false;
                                documentBaseResponse.IsValid = false;
                                documentBaseResponse.Message = String.Format("Tag -{0} is a default tag and cannot be deleted.", documentTagModel.TagName);
                                return documentBaseResponse;
                            }
                            #endregion
                            documentTag = new DocumentTag();
                            // documentTag = this._documentTagRepository.FirstOrDefault(a => a.DocumentID == documentTagModel.DocumentID && a.IsActive == true && a.TagID == documentTagModel.TagID);
                            documentTag = _documentTagRepository.GetTagById(documentTagModel.DocumentID, documentTagModel.TagID);
                            if (documentTag != null)
                            {
                                documentTag.IsActive = false;
                                documentTag.LastModifiedByUserID = userID;
                                documentTag.LastModifiedDateTime = currentDate;
                                documentTags.Add(documentTag);
                            }
                            else
                            {
                                _logger.LogError(String.Format("DocumentTag is null for DocumentID {0} and TagID {1}", documentTagModel.DocumentID, documentTagModel.TagID));
                            }
                        }
                        else if (documentTagModel.IsEdited && documentTagModel.DocumentTagID != null)
                        {
                            #region DocumentStateValidation
                            ValidationResponse stateValidationResponse = this._documentInformationProvider.CanModifyDocumentState(documentTagModel.DocumentID, userID);
                            if (!(stateValidationResponse.IsSuccess && stateValidationResponse.IsValid))
                            {

                                documentBaseResponse.IsSuccess = false;
                                documentBaseResponse.Message = "The file is locked by some other user. Please try after sometime.";
                                return documentBaseResponse;
                            }
                            #endregion

                            #region ValidateDefaultTag
                            if (documentTagModel.IsDefault)
                            {
                                documentBaseResponse.IsSuccess = false;
                                documentBaseResponse.IsValid = false;
                                documentBaseResponse.Message = String.Format("Tag -{0} is a default tag and cannot be edited.", documentTagModel.TagName);
                                return documentBaseResponse;
                            }
                            #endregion

                            #region ValidateTagValue
                            validationResponse = this._documentTagValueValidator.ValidateTagValue(documentTagModel);
                            if (!(validationResponse.IsSuccess && validationResponse.IsValid))
                            {
                                documentBaseResponse.IsSuccess = false;
                                documentBaseResponse.IsValid = false;
                                documentBaseResponse.Message = String.Format("Value entered for tag-{0} is invalid.The allowed values should be of type {1}", documentTagModel.TagName, documentTagModel.TagTypeName);
                                return documentBaseResponse;
                            }
                            #endregion

                            documentTag = new DocumentTag();
                            //documentTag = this._documentTagRepository.FirstOrDefault(a => a.DocumentID == documentTagModel.DocumentID && a.IsActive == true && a.TagID == documentTagModel.TagID);
                            documentTag = _documentTagRepository.GetTagById(documentTagModel.DocumentID, documentTagModel.TagID);
                            if (documentTag != null)
                            {
                                documentTag.LastModifiedByUserID = userID;
                                documentTag.LastModifiedDateTime = currentDate;
                                documentTag.Value = documentTagModel.Value;
                                documentTags.Add(documentTag);
                            }
                            else
                            {
                                _logger.LogError(String.Format("DocumentTag is null for DocumentID {0} and TagID {1}", documentTagModel.DocumentID, documentTagModel.TagID));
                            }
                        }

                    }
                    this._documentTagRepository.SaveDocumentTags(documentTags);

                    foreach (var docTag in documentTags)
                    {
                        //Activity log added with Tag
                        ActivityLog activityLog = new ActivityLog();
                        repositoryUser = _userProvider.GetUser(userID);
                        if (repositoryUser != null)
                            activityLog.UserGuID = repositoryUser.UserGuID;
                        if (docTag.IsActive)
                        {
                            activityLog.ActivityName = ActivityNameEnumeration.Modifyied.ToString();
                            activityLog.Custom2 = "Tag";
                            activityLog.Custom3 = "Added";
                        }
                        else
                        {
                            activityLog.ActivityName = ActivityNameEnumeration.Modifyied.ToString();
                            activityLog.Custom2 = "Tag";
                            activityLog.Custom3 = "Removed";
                        }

                        activityLog.NodeName = NodeNameEnumeration.Document.ToString();
                        activityLog.NodeKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                        activityLog.KeyValue = docTag.DocumentID.ToString();
                        activityLog.Custom1 = docTag.DocumentID.ToString();
                        activityLog.Custom4 = docTag.TagID.ToString();                        
                        this._actionLogger.LogUserActivity(activityLog);

                    }
                    documentBaseResponse.IsSuccess = true;
                    documentBaseResponse.IsValid = true;

                }
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while managing tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while managing tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while managing tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentBaseResponse;

        }

        public DocumentTagResponse FetchDocumentTags(DocumentTagRequest documentTagRequest)
        {
            DocumentTagResponse documentTagResponse = new DocumentTagResponse();
            List<DocumentTag> documentTags = new List<DocumentTag>();
            List<DocumentTagViewModel> documentTagViewModels = new List<DocumentTagViewModel>();
            try
            {
                #region Authorize
                //AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(documentTagRequest.UserID, (int)RoleEnumeration.DocumentTagManager);
                //if (!authorizationResponse.IsAllowed)
                //{
                //    documentTagResponse.IsSuccess = false;
                //    documentTagResponse.Message = "You are not authorized to perform this action";
                //    return documentTagResponse;
                //}
                #endregion

                //documentTags = this._documentTagRepository.FindBy(a => a.IsActive == true && a.DocumentID == documentTagRequest.DocumentID).ToList();
                documentTags = _documentTagRepository.GetTagListByDocumentId(documentTagRequest.DocumentID);
                if (documentTags != null)
                {
                    foreach (var documentTag in documentTags)
                    {
                        List<TagValue> tagsValues = new List<TagValue>();
                        List<TagValueModel> tagsValueEntities = new List<TagValueModel>();
                        DocumentTagViewModel documentTagViewModel = new DocumentTagViewModel();
                        documentTagViewModel.DocumentTagID = documentTag.DocumentTagID;
                        documentTagViewModel.DocumentID = documentTag.DocumentID;
                        documentTagViewModel.TagID = documentTag.TagID;
                        documentTagViewModel.TagTypeID = documentTag.Tag == null ? 0 : documentTag.Tag.TagTypeID;
                        documentTagViewModel.TagTypeName = documentTag.Tag == null ? String.Empty : documentTag.Tag.TagType == null ? String.Empty : documentTag.Tag.TagType.Name;
                        documentTagViewModel.TagName = documentTag.Tag == null ? String.Empty : documentTag.Tag.Name;
                        documentTagViewModel.Value = documentTag.Value;
                        documentTagViewModel.IsDefault = documentTag.IsDefault;

                        if (documentTagViewModel.TagTypeID == (long)TagTypeEnumeration.List)
                        {
                            //tagsValues = this._tagValueRepository.FindBy(a => a.IsActive == true && a.TagID == documentTag.TagID).ToList();

                            tagsValues = this._tagValueRepository.GetTagValueListById(documentTag.TagID);

                            if (tagsValues != null)
                            {
                                documentTagViewModel.TagValues = new List<TagValueModel>();
                                foreach (var value in tagsValues)
                                {
                                    TagValueModel tagValueEntity = new TagValueModel();
                                    tagValueEntity.TagValueID = value.TagValueID;
                                    tagValueEntity.Value = value.Value;
                                    tagsValueEntities.Add(tagValueEntity);
                                }
                                documentTagViewModel.TagValues.AddRange(tagsValueEntities);
                            }
                        }
                        documentTagViewModels.Add(documentTagViewModel);
                    }
                }
                documentTagResponse.DocumentTags = documentTagViewModels;
                documentTagResponse.IsSuccess = true;
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching document tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while fetching document tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching document tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentTagResponse;
        }

        public TagListResponse SearchTagsByName(string searchText)
        {
            TagListResponse tagListResponse = new TagListResponse();
            tagListResponse.DocumentTags = new List<DocumentTagViewModel>();
            List<TagValueModel> tagValueModelList = new List<TagValueModel>();
            try
            {
                tagListResponse.Tags = this._tagRepository.SearchByName(searchText);
                foreach (var tag in tagListResponse.Tags)
                {
                    DocumentTagViewModel documentTagViewModel = new DocumentTagViewModel();
                    documentTagViewModel.TagID = tag.TagID;
                    documentTagViewModel.TagName = tag.Name;
                    documentTagViewModel.TagTypeID = tag.TagTypeID;

                    if (tag.TagTypeID == (long)TagTypeEnumeration.List)
                    {
                        tag.TagValues = this._tagValueRepository.GetTagValueListById(tag.TagID);
                        if (tag.TagValues != null)
                        {
                            documentTagViewModel.TagValues = new List<TagValueModel>();
                            foreach (var value in tag.TagValues)
                            {
                                TagValueModel tagValueModel = new TagValueModel();
                                tagValueModel.TagValueID = value.TagValueID;
                                tagValueModel.Value = value.Value;
                                tagValueModelList.Add(tagValueModel);
                            }
                            documentTagViewModel.TagValues.AddRange(tagValueModelList);
                        }
                    }
                    tagListResponse.DocumentTags.Add(documentTagViewModel);
                }
                tagListResponse.IsSuccess = true;
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while fetching tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching tag -{0}", ex));
                throw new BusinessException("", ex);
            }
            return tagListResponse;
        }
    }
}
