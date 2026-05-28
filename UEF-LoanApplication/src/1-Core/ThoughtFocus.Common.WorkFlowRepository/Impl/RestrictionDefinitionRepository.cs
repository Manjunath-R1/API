using log4net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic; 
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Common.Workflow.Core.Model;
using ThoughtFocus.Common.WorkFlowDataAccess;
using ThoughtFocus.Common.WorkFlowRepository.Interface;

namespace ThoughtFocus.Common.WorkFlowRepository.impl
{
    public class RestrictionDefinitionRepository : AbstractEFApplicationBaseRepository<RestrictionDefinition>, IRestrictionDefinitionRepository
    {
        private WorkFlowContext context;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RestrictionDefinitionRepository));
        #region Constructors

        public RestrictionDefinitionRepository(WorkFlowContext context)
            : base(context)
        {
            this.context = context;
        }

        #endregion Constructors

        public void SaveOrUpdate(List<RestrictionDefinition> restrictionDefinitions)
        {
            try
            {
                foreach (var restrictionDefinition in restrictionDefinitions)
                {
                   
                        if (restrictionDefinition.ID > 0)
                            this.context.Entry(restrictionDefinition).State = EntityState.Deleted;
                        else
                            this.context.RestrictionDefinition.Add(restrictionDefinition);

                }
                this.context.SaveChanges();
              
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception  in SaveOrUpdate", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception  in SaveOrUpdate", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception  in SaveOrUpdate", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception  in SaveOrUpdate", ex);
            }
        }
    }
}
