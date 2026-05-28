using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface ISharedDocumentLogRepository : IBaseRepository<SharedDocumentLog>
    {
        void SaveLogs(List<SharedDocumentLog> sharedDocumentLogs);

        SharedDocumentLog GetSharedDocumentLog(string DocumentVersionKey, string EmailAddresses);
    }
}
