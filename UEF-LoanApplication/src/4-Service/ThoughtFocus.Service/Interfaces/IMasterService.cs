using System;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.DataAccess.Models.FundingSource;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Master;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Service.Interfaces
{
    public interface IMasterService
    {
        #region Methods


        /// <summary>
        /// This method is used to fetch the global master data
        /// </summary>     
        /// <returns>MasterDataEntity</returns>       
        /// <exception cref="Exception">Exception</exception>
        MasterDataEntity GetAllMasterEntity();

        List<AccessPermissionViewEntity> GetAccessControlRolePermissions(long userId);


        List<SalutationEntity> GetAllSalutationEntity();

        List<StateEntity> GetAllStateEntity();


        string GetAgreementData(long ApplicationId);

        string GetAgreementText(string AgreementData);

        BusinessProfileMasterDataEntity GetMasterEntityForBusinessProfile();

        List<GenaralOption> GetMasterOption(string category);
        FundAgreementDocuments GetSPAData(long applicationID);
        string GetSPAHTMLText(long applicationId);
        ThresholdAmountResponse GetThresholdRequestAmount();
        int GetProgressReportId();
        string GetEmailSPAHTMLText(long applicationId);
        
        #endregion Methods
    }
}
