using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ThoughtFocus.Common.Workflow.Core.Model;
using ThoughtFocus.Common.Workflow.Core.PersistanceModel;
using ThoughtFocus.Common.Workflow.Core.Runtime;
using ThoughtFocus.Common.WorkFlowDataAccess;

namespace ThoughtFocus.Common.Workflow.DbPersistance
{
    public class DbWorkflowPersistanceProvider : DbProvider,IWorkflowProvider
    {
        private WorkFlowContext _context;

        public DbWorkflowPersistanceProvider(string connectionString)
            : base(connectionString)
        {
        }

        public DbWorkflowPersistanceProvider(WorkFlowContext context, string AppDBConnectionString)
            : base(AppDBConnectionString)
        {
            _context = context;
        }

        public Core.Model.ProcessInstance CreateNewWorkflow(long processId, string workflowName, IDictionary<string, IEnumerable<object>> parameters)
        {
            throw new NotImplementedException();
        }

        public Core.Model.ProcessInstance CreateNewWorkflowScheme(long processId, string workflowName, IDictionary<string, IEnumerable<object>> parameters)
        {
            throw new NotImplementedException();
        }

        public Core.Model.ProcessInstance GetWorkflowInstance(long processId, long workFlowDefinationID)
        {
               WorkflowProcessInstance workflowProcessInstance = null;
               ProcessInstance processInstance = null;
            using (var context = CreateContext())
            {
                workflowProcessInstance = _context.WorkflowProcessInstances.FirstOrDefault(a => a.ProcessInstanceID == processId && a.WorkflowDefinitionID == workFlowDefinationID);
                if (workflowProcessInstance != null)
                {
                    processInstance = new ProcessInstance();
                    processInstance.ProcessId = workflowProcessInstance.ProcessInstanceID;
                    processInstance.CurrentState = workflowProcessInstance.StateName;
                    processInstance.IsDeterminingParametersChanged = 
                        workflowProcessInstance.IsDeterminingParametersChanged;
                    //processInstance.CurrentActivityName = workflowProcessInstance.ActivityName;
                    //Change the way you get
                    processInstance.Workflow = _context.WorkflowDefinition.FirstOrDefault(a => a.ID == workFlowDefinationID);
                    processInstance.SchemeId = workflowProcessInstance.SchemeId;
                    processInstance.WorkFlowID = workFlowDefinationID;

                }
                else
                {
                    Logger.Log.Error(String.Format("workflowProcessInstance is null for processId {0} ", processId));
                }
            }
            return processInstance;
        }
        public Core.Model.ProcessInstance GetProcessInstanceByProcessID(long processId)
        {
            WorkflowProcessInstance workflowProcessInstance = null;
            ProcessInstance processInstance = null;
            using (var context = CreateContext())
            {
                workflowProcessInstance = _context.WorkflowProcessInstances.FirstOrDefault(a => a.ProcessInstanceID == processId);
                if (workflowProcessInstance != null)
                {
                    processInstance = new ProcessInstance();
                    processInstance.ProcessId = workflowProcessInstance.ProcessInstanceID;
                    processInstance.CurrentState = workflowProcessInstance.StateName;
                    processInstance.IsDeterminingParametersChanged =
                        workflowProcessInstance.IsDeterminingParametersChanged;
                    //processInstance.CurrentActivityName = workflowProcessInstance.ActivityName;
                    //Change the way you get
                    //processInstance.Workflow = _context.WorkflowDefinition.FirstOrDefault(a => a.ID == workFlowDefinationID);
                    processInstance.SchemeId = workflowProcessInstance.SchemeId;
                    processInstance.WorkFlowID = workflowProcessInstance.WorkflowDefinitionID;

                }
                else
                {
                    Logger.Log.Error(String.Format("workflowProcessInstance is null for processId {0} ", processId));
                }
            }
            return processInstance;
        }
        public Core.Model.WorkflowDefinition GetWorkflowDefinition(long workflowId)
        {
            using (var context = CreateContext())
            {
                return _context.WorkflowDefinition.FirstOrDefault(a => a.ID == workflowId);
            }
        }

        public Core.Model.WorkflowDefinition GetWorkflowDefinition(string workflowName)
        {
            using (var context = CreateContext())
            {
                //During migration remove Include which is not support in EF core, Lazy loading is already configured
                return _context.WorkflowDefinition.FirstOrDefault(a => a.Name == workflowName);
            }
        }

        public Core.Model.WorkflowDefinition GetWorkflowDefinition(string workflowName, IDictionary<string, IEnumerable<object>> parameters)
        {
            throw new NotImplementedException();
        }


        public ProcessInstance CreateNewWorkflow(long processId, long workFlowID, IDictionary<string, IEnumerable<object>> parameters)
        {
            WorkflowDefinition workFlowDefination = this.GetWorkflowDefinition(workFlowID);
            if (workFlowDefination==null)
            {
                Logger.Log.Error(String.Format("workFlowDefination is null for processId {0} ", processId));
            }
            ProcessInstance processInstance = new ProcessInstance();
            processInstance.ProcessId = processId;
            processInstance.WorkFlowID = workFlowID;
            processInstance.SchemeId = Guid.NewGuid();
            processInstance.Workflow = workFlowDefination;
            return processInstance;
        }
        public void UpdateProcessInstance(long processId, long workFlowID, long workFlowSpaId )
        {
            var wfi = _context.WorkflowProcessInstances.Where(wf => wf.ProcessInstanceID == processId && wf.WorkflowDefinitionID == workFlowID).FirstOrDefault();
            if(wfi != null)
            {
                wfi.WorkflowDefinitionID = workFlowSpaId;
                _context.SaveChanges();
            }
        }

        public void UpdateProcessInstanceState(long processId, long workFlowID, string stateName)
        {
            var wfi = _context.WorkflowProcessInstances.Where(wf => wf.ProcessInstanceID == processId && wf.WorkflowDefinitionID == workFlowID).FirstOrDefault();
            if (wfi != null)
            {
                wfi.ActivityName = stateName;
                wfi.StateName = stateName;
                _context.SaveChanges();
            }
        }
        public void UpdateRequestPaymentProcessInstanceStatus(long processId, long workFlowID)
        {
            var wfiStatus = _context.WorkflowProcessInstanceStatus.Where(wfs => wfs.ProcessInstanceID == processId && wfs.WorkFlowDefinationID == workFlowID).FirstOrDefault();
            if (wfiStatus != null)
            {
                wfiStatus.Status = 2;
                _context.SaveChanges();
            }
        }
        public void UpdateProcessInstanceStatus(long processId, long workFlowID, long workFlowSpaId)
        {
            var wfiStatus = _context.WorkflowProcessInstanceStatus.Where(wfs => wfs.ProcessInstanceID == processId && wfs.WorkFlowDefinationID == workFlowID).FirstOrDefault();
            if (wfiStatus != null)
            {
                wfiStatus.WorkFlowDefinationID = workFlowSpaId;
                _context.SaveChanges();
            }
        }
       public void AddPersistenceStateHistory(WorkflowProcessTransitionHistory history)
        {
            _context.WorkflowProcessTransitionHistories.Add(history);

            _context.SaveChanges();
        }
        public WorkflowDefinition GetWorkflowDefinition(long workFlowID, IDictionary<string, IEnumerable<object>> parameters)
        {
            throw new NotImplementedException();
        }

        public int UpdateWorkFlow(long loanApplicationId)
        {
            var rslt = 0;
            var workFlowProcessInstance = _context.WorkflowProcessInstances.FirstOrDefault(x => x.ProcessInstanceID == loanApplicationId);
            if(workFlowProcessInstance != null)
            {
                workFlowProcessInstance.SchemeId = Guid.NewGuid();
                workFlowProcessInstance.ActivityName = "FinalDisbursed";
                workFlowProcessInstance.StateName = "FinalDisbursed";
                workFlowProcessInstance.WorkflowDefinitionID = 2;
                workFlowProcessInstance.PreviousActivity = "CFOApproved";
                workFlowProcessInstance.PreviousState = "CFOApproved";
                workFlowProcessInstance.PreviousActivityForDirect = "CFOApproved";
                workFlowProcessInstance.PreviousStateForDirect = "CFOApproved";
                rslt = _context.SaveChanges();
            }
            return rslt;
        }
    }
}
