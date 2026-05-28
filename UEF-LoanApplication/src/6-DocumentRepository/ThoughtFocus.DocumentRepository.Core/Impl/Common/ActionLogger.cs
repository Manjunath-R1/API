using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.Domain.Document;
using ThoughtFocus.DocumentRepository.Repository.Core;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class ActionLogger : IActionLogger
    {
        private readonly ILogger<ActionLogger> _logger;
        private IActionLogRepository _actionLogRepository;

        public ActionLogger(IActionLogRepository actionLogRepository, ILogger<ActionLogger> logger)
        {
            _actionLogRepository = actionLogRepository;
            _logger = logger;
        }

        public void LogUserActivity(ActivityLog activityLog)
        {
            try
            {

                this._actionLogRepository.SaveActivityLog(activityLog);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while logging User Activity.", ex));
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while logging User Activity.", ex));
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while logging User Activity.", ex));
            }
        }

        public List<ActivityLog> GetActivityLog(ActivityLog activityLog)
        {
            try
            {

                return this._actionLogRepository.GetActivityLog(activityLog);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching Activity.", ex));
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching Activity.", ex));
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching Activity.", ex));
            }
            return null;
        }
    }
}
