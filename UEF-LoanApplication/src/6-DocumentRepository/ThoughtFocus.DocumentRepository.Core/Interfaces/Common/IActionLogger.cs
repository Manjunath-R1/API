using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.Domain.Document;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IActionLogger
    {
        void LogUserActivity(ActivityLog activityLog);

        List<ActivityLog> GetActivityLog(ActivityLog activityLog);
         

    }
}
