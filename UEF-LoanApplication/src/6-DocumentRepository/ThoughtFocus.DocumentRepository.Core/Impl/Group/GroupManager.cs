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
    public class GroupManager : IGroupManager
    {
        #region Fields

        private readonly ILogger<GroupManager> _logger;
        private IGroupRepository _groupRepository;
        private IUserRepository _userProvider;
        private IActionLogger _actionLogger;
        private RepositoryUser repositoryUser = null;

        #endregion Fields

        #region Constructors

        public GroupManager(IGroupRepository groupRepository, IUserRepository userProvider, 
            IActionLogger actionLogger, ILogger<GroupManager> logger)
        {
            _groupRepository = groupRepository;
            _userProvider = userProvider;
            _actionLogger = actionLogger;
            _logger = logger;
        }

        #endregion Constructors

        #region Methods

        public List<GroupEntity> GetGroups()
        {
            try
            {
                List<GroupEntity> groupEntityList = new List<GroupEntity>();
                var groups = _groupRepository.GetGroups();

                foreach(var group in groups)
                {
                    GroupEntity groupEntity = new GroupEntity();
                    groupEntity.GroupID = group.GroupID;
                    groupEntity.Name = group.Name;
                    groupEntity.Description = group.Description;
                    groupEntityList.Add(groupEntity);
                }

                return groupEntityList;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching groups -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching groups -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching groups -{0}", ex));
                throw new BusinessException("", ex);
            }
        }

        public DocumentBaseResponse AddGroup(GroupRequest groupRequest)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            DateTime current = DateTime.Now;
            try
            {
                #region Validation
                ValidationResponse groupNameValidation = this.ValidateGroupName(groupRequest.Name);
                if (!(groupNameValidation.IsSuccess && groupNameValidation.IsValid))
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "Group Name already exists";
                    return documentBaseResponse;

                }
                #endregion Validation

                Group group = new Group();
                group.GroupID = groupRequest.GroupID;
                group.CreatedByUserID = groupRequest.LoggerInUserID;
                group.CreatedDateTime = current;
                group.LastModifiedDateTime = current;
                group.LastModifiedByUserID = groupRequest.LoggerInUserID;
                group.IsActive = true;
                group.Name = groupRequest.Name;
                group.Description = groupRequest.Description;

                this._groupRepository.SaveGroup(group);
                documentBaseResponse.IsSuccess = true;
                documentBaseResponse.Message = "Group added successfully.";

            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while adding group -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while adding group -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while adding group -{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentBaseResponse;
        }

        public DocumentBaseResponse SaveUserGroupRelation(Guid groupID, Guid userID)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            DateTime current = DateTime.Now;
            try
            {
                #region Validation
                ValidationResponse userValidation = this.ValidateUser(userID, groupID);
                if (!(userValidation.IsSuccess && userValidation.IsValid))
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.ID = groupID;
                    documentBaseResponse.Message = "User already exists in the group selected";
                    return documentBaseResponse;

                }
                #endregion Validation

                this._groupRepository.SaveUserGroupRelation(groupID, userID);
                documentBaseResponse.ID = groupID;
                documentBaseResponse.IsSuccess = true;
                documentBaseResponse.Message = "User added to group successfully.";
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while adding user group relation -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while adding user group relation -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while adding user group relation -{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentBaseResponse;
        }

        public ValidationResponse ValidateGroupName(string name)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            validationResponse.IsSuccess = true;
            try
            {
                List<Group> groups = new List<Group>();
                groups = this._groupRepository.GetGroups().ToList();
                if (groups.Any(a => a.Name.ToLower() == name.ToLower()))
                {
                    validationResponse.IsValid = false;
                }
                else
                {
                    validationResponse.IsValid = true;
                }
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);

            }
            return validationResponse;
        }

        public List<UserGroupEntity> GetUserGroupRelations()
        {
            try
            {
                List<UserGroupEntity> userGroupList = new List<UserGroupEntity>();
                var userGroups = _groupRepository.GetUserGroupRelations();

                foreach (var userGroup in userGroups)
                {
                    UserGroupEntity userGroupEntity = new UserGroupEntity();
                    userGroupEntity.GroupID = userGroup.GroupID;
                    userGroupEntity.GroupName = userGroup.GroupName;
                    userGroupEntity.RepositoryUserID = userGroup.RepositoryUserID;
                    userGroupEntity.UserName = userGroup.UserName;  
                    userGroupList.Add(userGroupEntity);
                }

                return userGroupList;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching users and groups -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching users and groups -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching users and groups -{0}", ex));
                throw new BusinessException("", ex);
            }
        }

        public DocumentBaseResponse DeleteGroup(DeleteRequest deleteRequest)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();

            try
            {
                Group group = _groupRepository.GetGroupByID(deleteRequest.GroupID);

                if (group != null)
                {
                    this._groupRepository.DeleteGroupRelations(group);

                    group.IsActive = false;
                    group.LastModifiedDateTime = DateTime.Now;
                    group.LastModifiedByUserID = deleteRequest.LoggedInUser;

                    this._groupRepository.SaveGroup(group);
                }
                
                documentBaseResponse.IsSuccess = true;
                documentBaseResponse.Message = "Group deleted successfully.";

                //Activity log added with Uploaded document
                ActivityLog activityLog = new ActivityLog();
                repositoryUser = _userProvider.GetUser(deleteRequest.LoggedInUser);
                if (repositoryUser != null)
                    activityLog.UserGuID = repositoryUser.UserGuID;
                activityLog.ActivityName = ActivityNameEnumeration.Deleted.ToString();
                activityLog.NodeName = NodeNameEnumeration.Group.ToString();
                activityLog.NodeKeyName = NodeKeyNameEnumeration.GroupID.ToString();
                activityLog.KeyValue = deleteRequest.GroupID.ToString();
                this._actionLogger.LogUserActivity(activityLog);

                return documentBaseResponse;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while deleting the group", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while deleting the group", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while deleting the group", ex));
                throw new BusinessException("", ex);
            }
        }

        public DocumentBaseResponse DeleteUserGroup(DeleteRequest deleteRequest)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();

            try
            {
                if (deleteRequest != null)
                {
                    RepositoryUserGroupMapping userGroup = new RepositoryUserGroupMapping();

                    userGroup.GroupID = deleteRequest.GroupID;
                    userGroup.RepositoryUserID = deleteRequest.UserID;

                    this._groupRepository.DeleteUserGroupRelations(userGroup);
                }

                documentBaseResponse.IsSuccess = true;
                documentBaseResponse.Message = "User is deleted from group successfully.";

                //Activity log added with Uploaded document
                ActivityLog activityLog = new ActivityLog();
                repositoryUser = _userProvider.GetUser(deleteRequest.LoggedInUser);
                if (repositoryUser != null)
                    activityLog.UserGuID = repositoryUser.UserGuID;
                activityLog.ActivityName = ActivityNameEnumeration.Deleted.ToString();
                activityLog.NodeName = NodeNameEnumeration.RepositoryUser.ToString();
                activityLog.NodeKeyName = NodeKeyNameEnumeration.UserGuID.ToString();
                activityLog.KeyValue = deleteRequest.UserID.ToString();
                this._actionLogger.LogUserActivity(activityLog);

                return documentBaseResponse;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while deleting the user", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while deleting the user", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while deleting the user", ex));
                throw new BusinessException("", ex);
            }
        }

        public List<UserGroupEntity> GetUsersByGroup(Guid groupID)
        {
            try
            {
                List<UserGroupEntity> userGroupList = new List<UserGroupEntity>();
                var userGroups = _groupRepository.GetUsersByGroup(groupID);

                foreach (var userGroup in userGroups)
                {
                    UserGroupEntity userGroupEntity = new UserGroupEntity();
                    userGroupEntity.GroupID = userGroup.GroupID;
                    userGroupEntity.GroupName = userGroup.GroupName;
                    userGroupEntity.RepositoryUserID = userGroup.RepositoryUserID;
                    userGroupEntity.UserName = userGroup.UserName;
                    userGroupList.Add(userGroupEntity);
                }

                return userGroupList;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching users and groups -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching users and groups -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching users and groups -{0}", ex));
                throw new BusinessException("", ex);
            }
        }

        public ValidationResponse ValidateUser(Guid userID, Guid groupID)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            validationResponse.IsSuccess = true;
            try
            {
                List<Group> groups = new List<Group>();
                groups = this._groupRepository.GetUserBelongsTo(userID);
                if (groups.Any(a => a.GroupID == groupID))
                {
                    validationResponse.IsValid = false;
                }
                else
                {
                    validationResponse.IsValid = true;
                }
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);

            }
            return validationResponse;
        }

        #endregion Methods
    }
}
