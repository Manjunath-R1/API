namespace ThoughtFocus.Repository.Interfaces.Audit
{
    using System;

    using ThoughtFocus.DataAccess.Models.Audit;

    public interface ISessionDetailsRepository : IEFApplicationBaseRepository<AuditSessionDetails>
    {
        #region Methods

        AuditSessionDetails GetAuditDetailsBySessionID(int SessionID);

        void SaveUserSessionDetails(AuditSessionDetails auditSessionDetails);
        

        #endregion Methods
    }
}