namespace ThoughtFocus.Repository.Interfaces.Master
{

    using ThoughtFocus.DataAccess.Models.Master;

    public interface IAgreementRepository : IEFApplicationBaseRepository<Agreement>
    {
        #region Methods

        /// <summary>
        /// This method is used to save and update the agreementname
        /// </summary>  
        /// <returns>Agreement</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateAgreement(Agreement agreement, long? userID);

        #endregion Methods

    }
}