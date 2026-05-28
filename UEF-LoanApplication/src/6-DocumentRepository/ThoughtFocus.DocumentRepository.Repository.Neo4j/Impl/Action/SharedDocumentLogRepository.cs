using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DocumentRepository.Repository.Neo4j;
using ThoughtFocus.Common.Exceptions;
using Neo4jClient;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl.Action
{
    public class SharedDocumentLogRepository: AbstractNeo4jBaseRepository<SharedDocumentLog>, ISharedDocumentLogRepository
    {
        private IGraphClient _graphClient;
        //private static readonly ILogger<SharedDocumentLogRepository> Logger;

        #region Constructors

        public SharedDocumentLogRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        #endregion Constructors

        public void SaveLogs(List<SharedDocumentLog> sharedDocumentLogs)
        {
            try
            {
                foreach (var log in sharedDocumentLogs)
                {
                     if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                            .Merge("(s:SharedDocumentLog { SharedDocumentLogID: {id} })")
                            .OnCreate().Set("s = {log}")
                            .OnMatch().Set("s = {log}")
                            .WithParams(new
                             {
                                 id = log.SharedDocumentLogID,
                                 log
                             });

                  query.ExecuteWithoutResultsAsync();
                }
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while inserting the SharedDocumentLog", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while inserting the SharedDocumentLog", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while inserting the SharedDocumentLog ", ex);
            }
        }

        public SharedDocumentLog GetSharedDocumentLog(string DocumentVersionKey, string EmailAddresses)
        {
            try
            {
                var documentSharedDocumentLogQuery = _graphClient.Cypher
                    .Match("(s:SharedDocumentLog)")
                    .Where((SharedDocumentLog s) => s.DocumentVersionSalt == DocumentVersionKey && s.EmailAddress == EmailAddresses)
                    .Return(s => s.As<SharedDocumentLog>());

                var documentSharedDocumentLog = documentSharedDocumentLogQuery.ResultsAsync.Result.FirstOrDefault();

                return documentSharedDocumentLog;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetSharedDocumentLog repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetSharedDocumentLog repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetSharedDocumentLog repository", ex);
            }
        }
    }
}
