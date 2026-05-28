using System;
using System.Collections.Generic;

namespace ThoughtFocus.Common.Workflow.Core.Runtime
{
    public interface IWorkflowRoleProvider
    {
        bool IsInRole(Guid identityId, string roleId,long processID);
        IEnumerable<Guid> GetAllInRole(string roleId);
    }
}
