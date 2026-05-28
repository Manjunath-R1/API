using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DocumentRepository.Repository.Neo4j;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain.Document;
using Neo4jClient;
using System.Text.RegularExpressions;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl.Action
{
    public class ActionLogRepository : AbstractNeo4jBaseRepository<UserActionLog>, IActionLogRepository
    {
       // private static readonly ILogger<ActionLogRepository> _logger;
        private readonly IGraphClient _graphClient;

        #region Constructors

        public ActionLogRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        #endregion Constructors

        public void SaveActivityLog(ActivityLog activityLog)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                //If ActivityLogID is null the sen new ID
                activityLog.ActivityLogID = activityLog.ActivityLogID == (default(Guid)) ? Guid.NewGuid() : activityLog.ActivityLogID;
                activityLog.Date = activityLog.Date == null ? DateTime.Now : activityLog.Date;

                //KeyValue is not an integer then append single quotes to each end
                if (!Regex.IsMatch(activityLog.KeyValue, @"^\d+$"))
                {
                    activityLog.KeyValue = "'" + activityLog.KeyValue + "'";
                }

                var query = _graphClient.Cypher
                                    .Match("(n:" + activityLog.NodeName + ")")
                                    .Where("n." + activityLog.NodeKeyName + "=" + activityLog.KeyValue)
                                    .Merge("(activityLog:ActivityLog {ActivityLogID : '" + activityLog.ActivityLogID + "' })")
                                    .CreateUnique("(n)-[r:HAS]->(activityLog)")
                                    .Set("activityLog ={ActivityLogID:'" + activityLog.ActivityLogID 
                                    + "',UserGuID:'" + activityLog.UserGuID
                                    + "' ,Date:'" + activityLog.Date
                                    + "' ,Custom1:'" + activityLog.Custom1
                                    + "' ,Custom2:'" + activityLog.Custom2
                                    + "' ,Custom3:'" + activityLog.Custom3
                                    + "' ,Custom4:'" + activityLog.Custom4
                                    + "' ,ActivityName:'" + activityLog.ActivityName
                                    + "'}");

                query.ExecuteWithoutResultsAsync();

                _graphClient.Cypher.CreateUniqueConstraint("activityLog:ActivityLog", "activityLog.ActivityLogID").ExecuteWithoutResultsAsync();

            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while inserting the ActionLog", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while inserting the ActionLog", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while inserting the ActionLog ", ex);
            }
        }

        public List<ActivityLog> GetActivityLog(ActivityLog activityLog)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();


                //KeyValue is not an integer then append single quotes to each end
                if (!Regex.IsMatch(activityLog.KeyValue, @"^\d+$"))
                {
                    activityLog.KeyValue = "'" + activityLog.KeyValue + "'";
                }

                var query = _graphClient.Cypher
                      .Match("(n:" + activityLog.NodeName + ")-[relation: HAS]->(activitylog:ActivityLog)")
                      .Where("n." + activityLog.NodeKeyName + "=" + activityLog.KeyValue)
                     .Return(activitylog => activitylog.As<ActivityLog>());

                return query.ResultsAsync.Result.ToList();
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while fetching the ActionLog", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while fetching the ActionLog", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while fetching the ActionLog ", ex);
            }
        }
    }



}
