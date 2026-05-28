using log4net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic; 
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Common.Workflow.Core.Model;
using ThoughtFocus.Common.Workflow.Core.PersistanceModel;
using ThoughtFocus.Common.WorkFlowDataAccess;
using ThoughtFocus.Common.WorkFlowRepository.Interface;

namespace ThoughtFocus.Common.WorkFlowRepository.impl
{
    public class WorkflowProcessTransitionHistoryRepository : AbstractEFApplicationBaseRepository<WorkflowProcessTransitionHistory>, IWorkflowProcessTransitionHistoryRepository
    {
        private WorkFlowContext context;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RestrictionDefinitionRepository));
        #region Constructors

        public WorkflowProcessTransitionHistoryRepository(WorkFlowContext context)
            : base(context)
        {
            this.context = context;
        }

        #endregion Constructors
         
    }
}
