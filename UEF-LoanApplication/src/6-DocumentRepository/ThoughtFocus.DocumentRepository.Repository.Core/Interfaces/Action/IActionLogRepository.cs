using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain.Document;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface IActionLogRepository : IBaseRepository<UserActionLog>
    {
        void SaveActivityLog(ActivityLog actionLog);

        List<ActivityLog> GetActivityLog(ActivityLog activityLog);
    }
}
