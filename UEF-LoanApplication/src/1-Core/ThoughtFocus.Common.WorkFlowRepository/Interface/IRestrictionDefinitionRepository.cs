using System.Collections.Generic;
using ThoughtFocus.Common.Workflow.Core.Model;


namespace ThoughtFocus.Common.WorkFlowRepository.Interface
{
    public interface IRestrictionDefinitionRepository : IEFApplicationBaseRepository<RestrictionDefinition>
    {
        void SaveOrUpdate(List<RestrictionDefinition> restrictionDefinitions);
    }
}
