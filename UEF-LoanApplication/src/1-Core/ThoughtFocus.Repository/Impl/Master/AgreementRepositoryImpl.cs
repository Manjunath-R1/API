namespace ThoughtFocus.Repository.Impl.Master
{
    using System;
    using System.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class AgreementRepositoryImpl : AbstractEFApplicationBaseRepository<Agreement>, IAgreementRepository
    {
        #region Fields
        private ApplicationDBContext _Context;

        #endregion Fields

        #region Constructors

        public AgreementRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;

        }
        #endregion Constructors

        #region Methods
        public void SaveOrUpdateAgreement(Agreement agreementInfo, long? userID)
        {
            try
            {
                if (agreementInfo.AgreementID > 0)
                {
                    _Context.Entry(agreementInfo).State = EntityState.Modified;
                }
                else
                    _Context.Agreements.Add(agreementInfo);

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in AgreementRepositoryImpl-> SaveOrUpdateAgreement", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in AgreementRepositoryImpl-> SaveOrUpdateAgreement", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in AgreementRepositoryImpl-> SaveOrUpdateAgreement", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in AgreementRepositoryImpl-> SaveOrUpdateAgreement", ex);
            }
        }

        #endregion Methods

    }
}