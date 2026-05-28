using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain.Response;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.Common.Exceptions.BusinessException;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class DocumentSeeker : IDocumentSeeker
    {
        private readonly ILogger<DocumentSeeker> _logger;
        private IDocumentRepository _documentRepository;
        private IAuthorizer _authorizer;
        //private IStorageService _storageService;

        public DocumentSeeker(IDocumentRepository documentRepository, IAuthorizer authorizer, 
            ILogger<DocumentSeeker> logger)
        {
            _documentRepository = documentRepository;
            _authorizer = authorizer;
            _logger = logger;
           
        }

        public FilterResponse SearchDocuments(FilterRequest filterRequest)
        {
            var nameFilter = filterRequest.Filters.FirstOrDefault(f => f.Property == "SearchText" && f.Value != null && !string.IsNullOrEmpty(f.Value.ToString()));
            var filterResponse = new FilterResponse();
            var contentSearchResult = new ContentSearchResult();

            if (nameFilter != null)
            {
                contentSearchResult = SearchText(nameFilter.Value.ToString());
                filterRequest.Filters.Add(new FilterItem { Property = "ContentSearchResult", Value = contentSearchResult });                
            }

            filterResponse = _documentRepository.SearchDocuments(filterRequest);
            filterResponse.ContentSearchResult = contentSearchResult;
            filterResponse.DocumentList = new List<DocumentEntity>();

            foreach (var document in filterResponse.Documents)
            {
                DocumentEntity documentEntity = new DocumentEntity();
                documentEntity.DocumentID = document.DocumentID;
                documentEntity.CreatedDateTime = document.CreatedDateTime;
                documentEntity.LastModifiedDateTime = document.LastModifiedDateTime;
                documentEntity.CreatedByUserID = document.CreatedByUserID;
                documentEntity.LastModifiedByUserID = document.LastModifiedByUserID;
                documentEntity.IsActive = document.IsActive;
                documentEntity.Name = document.Name;
                documentEntity.Path = document.Path;
                documentEntity.MajorVersion = document.MajorVersion;
                documentEntity.MinorVersion = document.MinorVersion;
                documentEntity.Number = document.Number;
                documentEntity.Key = document.Key;
                documentEntity.StorageKey = document.StorageKey;
                documentEntity.IsLocked = document.IsLocked;
                documentEntity.LockedByUserID = document.LockedByUserID;
                documentEntity.FileExtensionTypeID = document.FileExtensionTypeID;
                documentEntity.FileSize = document.FileSize;
                documentEntity.LastUploadedDate = document.LastUploadedDate;
                filterResponse.DocumentList.Add(documentEntity);
            }

            return filterResponse;
        }

        public ContentSearchResult SearchText(string searchText)
        {
            var textSearchResponse = new ContentSearchResult();

            try
            {
                TextSearchRequest request = new TextSearchRequest();
                request.SearchText = searchText;
                request.HighLighter = "{\"content\": {\"format\": \"text\", \"max_phrases\": 5 , pre_tag:'<em><strong>', post_tag:'</strong></em>'}}";

                textSearchResponse = null;//this._storageService.SearchText(request);
                if (textSearchResponse.IsSuccess)
                {
                    List<Guid> documentKeys = new List<Guid>();
                    foreach (var searchResult in textSearchResponse.SearchResults)
                    {
                        Guid searchResultkey;
                        if (Guid.TryParse(searchResult.SearchResponseID, out searchResultkey))
                        {
                            documentKeys.Add(searchResultkey);
                        }
                    }
                    Guid[] documentKeysArray = documentKeys.ToArray();
                    textSearchResponse.CloudSearchedDocuments = documentKeysArray;
                }
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while Searching text -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while Searching text -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while Searching text -{0}", ex));
                throw new BusinessException("", ex);
            }
            return textSearchResponse;
        }
    }
}
