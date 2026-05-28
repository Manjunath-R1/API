namespace ThoughtFocus.Repository.Impl.Master
{
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.Repository.Interfaces.Master;
    using System.Data.SqlClient;
    using ThoughtFocus.Common.Exceptions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using ThoughtFocus.DataAccess.Models.Admin;

    public class HelpfulGuideTemplateRepositoryImpl : AbstractEFApplicationBaseRepository<HelpfulGuideTemplate>, IHelpfulGuideTemplateRepository
    {
        private ApplicationDBContext _Context;

        #region Constructors

        public HelpfulGuideTemplateRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
        }

        #endregion Constructors

        #region Methods

        public HelpfulGuideTemplate GetHelpfulGuideById(long tamplateID)
        {
            try
            {
                return this._Context.HelpfulGuideTemplates.Where(a => a.ID == tamplateID).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at HelpfulGuideTemplateRepositoryImpl-> GetHelpfulGuideById", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at HelpfulGuideTemplateRepositoryImpl-> GetHelpfulGuideById", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at HelpfulGuideTemplateRepositoryImpl-> GetHelpfulGuideById", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in HelpfulGuideTemplateRepositoryImpl-> GetHelpfulGuideById", ex);
            }
        }

        public void SaveOrUpdateHelpfulGuideTemplate(HelpfulGuideTemplate helpfulGuideTemplateInfo, long userID)
        {
            try
            {
                if (helpfulGuideTemplateInfo.ID > 0)
                {
                    _Context.Entry(helpfulGuideTemplateInfo).State = EntityState.Modified;
                }
                else
                {
                    _Context.HelpfulGuideTemplates.Add(helpfulGuideTemplateInfo);
                }

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in HelpfulGuideTemplateRepositoryImpl-> SaveOrUpdateHelpfulGuideTemplate", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in HelpfulGuideTemplateRepositoryImpl-> SaveOrUpdateHelpfulGuideTemplate", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in HelpfulGuideTemplateRepositoryImpl-> SaveOrUpdateHelpfulGuideTemplate", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in HelpfulGuideTemplateRepositoryImpl-> SaveOrUpdateHelpfulGuideTemplate", ex);
            }
        }


        #endregion Methods
    }
}