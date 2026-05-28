using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DocumentRepository.Repository.Neo4j;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using Neo4jClient;
using ThoughtFocus.Common.Exceptions;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl
{
    public class DocumentVersionHistoryRepository : AbstractNeo4jBaseRepository<DocumentVersionHistory>, IDocumentVersionHistoryRepository
    {

        //private static readonly ILogger<DocumentVersionHistoryRepository> _logger;
        private readonly IGraphClient _graphClient;

        #region Constructors

        public DocumentVersionHistoryRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        #endregion Constructors

        public void SaveDocumentVersionHistories(List<DataAccess.Neo4j.DocumentVersionHistory> documentVersions)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

              var query=  _graphClient.Cypher
                    .Unwind(documentVersions, "documentVersion")
                    .Match("(document:Document)")
                    .Where("document.DocumentID = documentVersion.DocumentID")
                    .Merge("(d:DocumentVersionHistory {DocumentVersionHistoryID : documentVersion.DocumentVersionHistoryID})")
                    .CreateUnique("(document)-[r:HAS]->(d)")
                    .Set("d = documentVersion");


              query.ExecuteWithoutResultsAsync();

                _graphClient.Cypher.CreateUniqueConstraint("d:DocumentVersionHistory", "d.DocumentVersionHistoryID").ExecuteWithoutResultsAsync();
            }

            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while inserting the Document Version History", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while inserting the Document Version History", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while inserting the Document Version History ", ex);
            }
        }

        public void SaveDocumentVersionHistory(DocumentVersionHistory documentVersions)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                if (documentVersions != null)
                {

                    
                    var Insertquery = _graphClient.Cypher
                                        .Merge("(d:DocumentVersionHistory { DocumentVersionHistoryID: {id} })")
                                        .OnCreate().Set("d = {documentVersions}")
                                        .OnMatch().Set("d = {documentVersions}")
                                       
                                        .WithParams(new
                                        {
                                            id = documentVersions.DocumentVersionHistoryID,
                                            documentVersions
                                        });
                                        Insertquery.ExecuteWithoutResultsAsync();

                     var query = _graphClient.Cypher
                                .Match("(document:Document { DocumentID: {id} })", "(d:DocumentVersionHistory { DocumentID: {id} })")
                                .CreateUnique("(document)-[r:HAS]->(d)")
                                 .WithParams(new
                                 {
                                     id = documentVersions.DocumentID,
                                     documentVersions
                                 });
                          query.ExecuteWithoutResultsAsync();

                }                

            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while inserting the Document Version History", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while inserting the Document Version History", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while inserting the Document Version History ", ex);
            }
        }


        public List<DocumentVersionHistory> GetDocumentVersionHistoryId(Guid documentID)
        {
            try
            {
                var documentVersionHistoryQuery = _graphClient.Cypher
                    .Match("(d:DocumentVersionHistory)")
                    .Where((DocumentVersionHistory d) => d.DocumentID == documentID && d.IsActive == true)
                    .Return(d => d.As<DocumentVersionHistory>());

                var documentVersionHistory = documentVersionHistoryQuery.ResultsAsync.Result.ToList();

                return documentVersionHistory;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetDocumentVersionHistoryId repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetDocumentVersionHistoryId repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetDocumentVersionHistoryId repository", ex);
            }
        }


        public DocumentVersionHistory GetDocumentVersionHistoryById(Guid documentID)
        {
            try
            {
                var documentVersionHistoryQuery = _graphClient.Cypher
                    .Match("(d:DocumentVersionHistory)")
                    .Where((DocumentVersionHistory d) => d.DocumentID == documentID && d.IsActive == true)
                    .Return(d => d.As<DocumentVersionHistory>());

                var documentVersionHistory = documentVersionHistoryQuery.ResultsAsync.Result.OrderByDescending(d =>d.CreatedDateTime).FirstOrDefault();

                return documentVersionHistory;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetDocumentVersionHistoryId repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetDocumentVersionHistoryId repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetDocumentVersionHistoryId repository", ex);
            }
        }


        public DocumentVersionHistory GetDocumentVersionHistoryByVersionKey(string documentVersionKey)
        {
            try
            {
                var documentVersionHistoryQuery = _graphClient.Cypher
                    .Match("(d:DocumentVersionHistory)")
                    .Where((DocumentVersionHistory d) => d.VersionSalt == documentVersionKey && d.IsActive == true)
                    .Return(d => d.As<DocumentVersionHistory>());

                var documentVersionHistory = documentVersionHistoryQuery.ResultsAsync.Result.FirstOrDefault();

                return documentVersionHistory;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetDocumentVersionHistoryId repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetDocumentVersionHistoryId repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetDocumentVersionHistoryId repository", ex);
            }
        }
    }
}
