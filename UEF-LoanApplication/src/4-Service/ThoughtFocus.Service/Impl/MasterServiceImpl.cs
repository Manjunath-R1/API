using System;
using System.Collections.Generic;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.RoleProvider.Interfaces;
using System.Linq;
using ThoughtFocus.Business.Interfaces.Master;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.Domain.Master;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Repository.Interfaces.Master;
using AutoMapper;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ThoughtFocus.Repository.Interfaces.Application;
using System.Text;
using System.IO;
using ThoughtFocus.Repository.Interfaces.FundingSource;
using ThoughtFocus.DataAccess.Models.FundingSource;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.Constants;
using ThoughtFocus.DataAccess.Models;
using Microsoft.Extensions.Options;

namespace ThoughtFocus.Service.Impl
{
    public class MasterServiceImpl : IMasterService
    {
        #region Fields

        private readonly ILogger<MasterServiceImpl> _logger;
        private IListRole listRole;
        private IViewRole viewRole;

        private IStateRepository _stateRepository;
        private ISalutationRepository _salutationRepository;
        private IApplicationStatusRepository _applicationStatusRepository;
        private IApplicationTypeRepository _applicationTypeRepository;
        private IGenderRepository _genderRepository;
        private IBusinessTypeRepository _businessTypeRepository;
        private IIndustryTypeRepository _industryTypeRepository;
        private IAffiliateRepository _affiliateRepository;
        private IVeteranRepository _veteranRepository;
        private IRaceRepository _raceRepository;
        private IEthnicityRepository _ethnicityRepository;
        private IRoleRepository _roleRepository;
        private IBusinessRoleRepository _businessRoleRepository;
        private IFundingTypeRepository _fundingTypeRepository;
        private IAgreementRepository _agreementRepository;
        private readonly IMapper _mapper;
        private readonly ILoanApplicationRepository _LoanApplicationRepository;
        private readonly IGenaralOptionRepository _genaralOptionRepository;
        private IFundingSourceRepository _fundingSourceRepository;
        private readonly AppSettings _appSettings;
        #endregion Fields

        #region Constructors

        public MasterServiceImpl(IListRole listRole,
           IViewRole viewRole,
            ILogger<MasterServiceImpl> logger,
            IStateRepository StateRepository,
            ISalutationRepository SalutationRepository,
            IMapper Mapper,
            IApplicationStatusRepository ApplicationStatusRepository,
            IApplicationTypeRepository ApplicationTypeRepository,
            IGenderRepository GenderRepository,
            IBusinessTypeRepository BusinessTypeRepository,
            IIndustryTypeRepository IndustryTypeRepository,
            IAffiliateRepository AffiliateRepository,
            IVeteranRepository VeteranRepository,
            IRaceRepository RaceRepository,
            IEthnicityRepository EthnicityRepository,
            IRoleRepository RoleRepository,
            IBusinessRoleRepository BusinessRoleRepository,
            IFundingTypeRepository FundingTypeRepository,
            IAgreementRepository agreementRepository,
            ILoanApplicationRepository loanApplicationRepository,
            IGenaralOptionRepository genaralOptionRepository, IFundingSourceRepository fundingSourceRepository, IOptions<AppSettings> appSettings
            )
        {
            this.listRole = listRole ?? throw new ArgumentNullException(nameof(listRole));
            this.viewRole = viewRole ?? throw new ArgumentNullException(nameof(viewRole));
            this._logger = logger;
            _stateRepository = StateRepository;
            _mapper = Mapper;
            _salutationRepository = SalutationRepository;
            _affiliateRepository = AffiliateRepository;
            _applicationStatusRepository = ApplicationStatusRepository;
            _applicationTypeRepository = ApplicationTypeRepository;
            _businessRoleRepository = BusinessRoleRepository;
            _businessTypeRepository = BusinessTypeRepository;
            _ethnicityRepository = EthnicityRepository;
            _genderRepository = GenderRepository;
            _industryTypeRepository = IndustryTypeRepository;
            _raceRepository = RaceRepository;
            _roleRepository = RoleRepository;
            _veteranRepository = VeteranRepository;
            _fundingTypeRepository = FundingTypeRepository;
            _agreementRepository = agreementRepository;
            _LoanApplicationRepository = loanApplicationRepository;
            _genaralOptionRepository =genaralOptionRepository;
            _fundingSourceRepository =fundingSourceRepository;
            _appSettings = appSettings.Value;
        }
        #endregion Constructors

        #region Methods

        #region ListRole


        public List<RolePermissionViewEntity> GetGlobalRolePermissions(long userId)
        {
            List<RolePermissionViewEntity> rolePermissionViewEntities = new List<RolePermissionViewEntity>();
            try
            {
                List<RolePermission> rolePermissionEntities = new List<RolePermission>();
                rolePermissionEntities = this.listRole.GetGlobalRolePermissions(userId);
                if (rolePermissionEntities != null)
                {
                    foreach (var rolePermissionEntity in rolePermissionEntities)
                    {
                        rolePermissionViewEntities.Add(new RolePermissionViewEntity
                        {
                            RoleName = rolePermissionEntity.Role.RoleName,
                            Subject = rolePermissionEntity.Subject,
                            ActionName = rolePermissionEntity.ActionID == null ? "" : rolePermissionEntity.Action.Name,
                            IsAllowed = rolePermissionEntity.IsAllowed,
                            ActionID = rolePermissionEntity.ActionID == null ? 0 : rolePermissionEntity.ActionID.Value
                        });
                    }
                }
            }
            catch (BusinessException ex)
            {
                _logger.LogError("Exception in GetGlobalRolePermissions", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetGlobalRolePermissions", ex);
            }
            return rolePermissionViewEntities;
        }

        public List<AccessPermissionViewEntity> GetAccessControlRolePermissions(long userId)
        {
            List<AccessPermissionViewEntity> accessPermissionViewEntity = new List<AccessPermissionViewEntity>();
            List<RolePermission> rolePermissionEntities = new List<RolePermission>();
            rolePermissionEntities = this.listRole.GetGlobalRolePermissions(userId);

            foreach (var rolePermissionViewEntity in rolePermissionEntities)
            {
                if (rolePermissionViewEntity.IsAllowed)
                {
                    accessPermissionViewEntity.Add(new AccessPermissionViewEntity
                    {
                        subject = rolePermissionViewEntity.Subject,
                        actions = rolePermissionViewEntity.ActionID == null ? "" : rolePermissionViewEntity.Action.Name,
                        conditions = rolePermissionViewEntity.Condition
                    });
                }
            }
            return accessPermissionViewEntity;
        }

        #endregion ListRole


        #region Abstract Methods

        public MasterDataEntity GetAllMasterEntity()
        {
            MasterDataEntity MasterDataEntity = new MasterDataEntity();
            try
            {
                MasterDataEntity.StateList = GetAllStateEntity();
                MasterDataEntity.SalutationList = GetAllSalutationEntity();
                MasterDataEntity.GenderList = _mapper.Map<List<GenderEntity>>(_genderRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.AffiliateList = _mapper.Map<List<AffiliateEntity>>(_affiliateRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.ApplicationStatusList = _mapper.Map<List<ApplicationStatusEntity>>(_applicationStatusRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.ApplicationTypeList = _mapper.Map<List<ApplicationTypeEntity>>(_applicationTypeRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.BusinessTypeList = _mapper.Map<List<BusinessTypeEntity>>(_businessTypeRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.EthnicityList = _mapper.Map<List<EthnicityEntity>>(_ethnicityRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.IndustryTypeList = _mapper.Map<List<IndustryTypeEntity>>(_industryTypeRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.RaceList = _mapper.Map<List<RaceEntity>>(_raceRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.RoleList = _mapper.Map<List<RoleEntity>>(_roleRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.VeteranList = _mapper.Map<List<VeteranEntity>>(_veteranRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.BusinessRoleList = _mapper.Map<List<BusinessRoleEntity>>(_businessRoleRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.FundingTypeList = _mapper.Map<List<FundingTypeEntity>>(_fundingTypeRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());

            }
            catch (SqlException ex)
            {
                _logger.LogError("SQL Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
                throw new RepositoryException("Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("DBUpdate Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
                throw new RepositoryException("Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError("ObjectDisposed Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
                throw new RepositoryException("Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
                throw new RepositoryException("Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
            }

            return MasterDataEntity;
        }
        public BusinessProfileMasterDataEntity GetMasterEntityForBusinessProfile()
        {
            BusinessProfileMasterDataEntity MasterDataEntity = new BusinessProfileMasterDataEntity();
            try
            {
                MasterDataEntity.StateList = GetAllStateEntity();
                MasterDataEntity.GenderList = _mapper.Map<List<GenderEntity>>(_genderRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.BusinessTypeList = _mapper.Map<List<BusinessTypeEntity>>(_businessTypeRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.EthnicityList = _mapper.Map<List<EthnicityEntity>>(_ethnicityRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.IndustryTypeList = _mapper.Map<List<IndustryTypeEntity>>(_industryTypeRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.RaceList = _mapper.Map<List<RaceEntity>>(_raceRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
                MasterDataEntity.VeteranList = _mapper.Map<List<VeteranEntity>>(_veteranRepository.GetAll().Where(x => x.IsActive == true).OrderBy(y => y.DisplayOrder).ToList());
           
            }
            catch (SqlException ex)
            {
                _logger.LogError("SQL Exception in MasterServiceImpl-> GetMasterEntityForBusinessProfile", ex);
                throw new RepositoryException("Exception in MasterServiceImpl-> GetMasterEntityForBusinessProfile", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("DBUpdate Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
                throw new RepositoryException("Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError("ObjectDisposed Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
                throw new RepositoryException("Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
                throw new RepositoryException("Exception in MasterServiceImpl-> GetAllMasterEntity", ex);
            }

            return MasterDataEntity;
        }

        public List<SalutationEntity> GetAllSalutationEntity()
        {
            List<SalutationEntity> SalutationEntityList = new List<SalutationEntity>();
            try
            {
                List<Salutation> SalutationDBModel = _salutationRepository.GetAllSalutationData().Where(x => x.IsActive == true).OrderBy(x => x.DisplayOrder).ToList();
                SalutationEntityList = _mapper.Map<List<SalutationEntity>>(SalutationDBModel);
            }
            catch (Exception Exception)
            {
                //log the error
                throw Exception;
            }

            return SalutationEntityList;
        }


        public List<StateEntity> GetAllStateEntity()
        {
            List<StateEntity> StatesEntity = new List<StateEntity>();
            try
            {
                List<State> States = _stateRepository.GetAllStateData().Where(x => x.IsActive == true).OrderBy(x => x.DisplayOrder).ToList();
                StatesEntity = _mapper.Map<List<StateEntity>>(States);
            }
            catch (Exception Exception)
            {

                throw Exception;
            }

            return StatesEntity;
        }


        public string GetAgreementData(long ApplicationID)
        {
            try
            {

                var loanApplication = _LoanApplicationRepository.GetLoanApplicationByApplicationID(ApplicationID);
                if (loanApplication != null && loanApplication.FundingApplication != null && loanApplication.FundingApplication.FundingSource != null && loanApplication.FundingApplication.FundingSource.AgreementID != null && loanApplication.FundingApplication.FundingSource.AgreementID > 0)
                {
                    Agreement agreement = this._agreementRepository.FirstOrDefault(a => a.IsActive == true && a.AgreementID == loanApplication.FundingApplication.FundingSource.AgreementID);
                    return agreement?.Body;
                }

                return "No agreement available for this program.";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetAgreementText(string AgreementText)
        {
            try
            {
                StringBuilder template = new StringBuilder();
                template.Append("<html lang='en'>");
                template.Append("<head><link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.1.1/css/bootstrap.min.css' /></head>");
                template.Append("<body><div class='container'><div>@@AgreementText</div></div></body></html>");
                template.Replace("@@AgreementText", AgreementText);
                return template.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Abstract Methods
        public List<GenaralOption> GetMasterOption(string category)
        {
            try
            {

                return _genaralOptionRepository.GetMasterOption( category);              

               

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FundAgreementDocuments GetSPAData(long applicationID)
        {
            try
            {              
                    return _fundingSourceRepository.GetPaymentAgreementDocumentByApplicationId(applicationID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetSPAHTMLText(long applicationId)
        {
            var lstTansactions = _fundingSourceRepository.GetPaymentScheduleTransactionByLoanID(applicationId).Where(x => x.IsActive==true).OrderBy(x => x.TransactionDate).ToList();
            var loanApplication = this._LoanApplicationRepository.GetLoanApplicationByApplicationID(applicationId);
            var paymentScheduleSummary = this._fundingSourceRepository.GetPaymentScheduleSummaryByLoanID(applicationId);
            var sb = new StringBuilder();
            
           // sb.Append("<div><a href = \"/\"><img src = \"" + _appSettings.BaseUrl + "/img/NUL_Logo_Standalone.93b02716.png\" alt=\"logo\" style=\"height: 50px;margin-right:20px;\"><img src = \"" + _appSettings.BaseUrl + "/img/Urban-Empowerment-Fund-2020-cmyk@2x.13c4f952.png\" alt=\"logo\" style=\"height: 50px;margin-right:10px;\" ></a></div>");
            sb.Append("<div style= \"margin-top: 30px; border-top-width: 1px;border-top-style: solid;border-top-color: rgb(193, 20, 57);\"> </div>");
            sb.Append("<div style= \"margin-top: 20px; \"> </div>");
            sb.Append("<table border = \"0\" cellpadding = \"3\" cellspacing = \"0\" class=\"left\" style = \"width:100%\">");
            sb.Append("<tbody>");
            
            sb.Append("<tr>");
            sb.Append("<td style = \"background-color:#D3D3D3; width:400px\">");            
            sb.Append("<strong >Payment Schedule");
            sb.Append("</strong>");
            sb.Append("</td>");
            
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("<div style= \"margin-top: 20px; \"> </div>");
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("<table align = \"left\" border = \"0\" cellpadding = \"0\" cellspacing = \"0\" style = \"width:500px\" >");
            sb.Append("<tbody >");
            sb.Append("<tr>");
            sb.Append("<td style = \"width: 255px\" > Grant Number </td>");
            sb.Append("<td style = \"width: 17px\" >:</td>");
            sb.Append("<td style = \"width: 216px\" >");
            sb.Append(loanApplication?.LoanNumber);         
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style = \"width: 255px\" >Business Name");
            sb.Append("</td>");
            sb.Append("<td style = \"width: 17px\" >");
            sb.Append(":");
            sb.Append("</td>");
            sb.Append("<td style = \"width: 216px\" >");
            sb.Append(loanApplication?.LoanBusinessDetail?.BusinessName);            
            sb.Append("</td >");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style = \"width: 255px\" >Grant Program");
            sb.Append("</td >");
            sb.Append("<td style = \"width: 17px\" >");
            sb.Append(":");
            sb.Append("</td >");
            sb.Append("<td style = \"width: 216px\" >");
            sb.Append(loanApplication?.ProgramInvitation?.FundingSource?.ProgramName);
           
            sb.Append("</td >");
            sb.Append("</tr >");
            /*sb.Append("<tr >");
            sb.Append("<td style = \"width:255px\" >Fund Requested");
            sb.Append("</td >");
            sb.Append("<td style = \"width: 17px\" >");
            sb.Append(":");
            sb.Append("</td >");
            sb.Append(" <td style = \"width: 216px\" >");
            sb.Append(loanApplication?.FundingApplication?.RequestedFundAmount > 0 ? "$ "+String.Format("{0:#,#}", loanApplication.FundingApplication.RequestedFundAmount) : string.Empty);
            sb.Append("</td >");
            sb.Append("</tr >");*/
            sb.Append("<tr >");
            sb.Append("<td style = \"width: 255px\" >Funds Allocated");
            sb.Append("</td >");
            sb.Append("<td style = \"width: 17px\" >");
            sb.Append(":");
            sb.Append("</td >");
            sb.Append("<td style = \"width: 216px\" >");
            sb.Append(paymentScheduleSummary.FundAllocatedAmount > 0 ? "$ " +  String.Format("{0:#,#}", paymentScheduleSummary.FundAllocatedAmount) : string.Empty);
            sb.Append("</td >");
            sb.Append("</tr >");
            sb.Append("</tbody >");
            sb.Append("</table >");


            sb.Append("</td>");
            sb.Append("</tr>");
         
            //sb.Append("<tr>");
            //sb.Append("<td>");
            //sb.Append("</td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td>");
            //sb.Append("</td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td>");
            //sb.Append("</td>");
            //sb.Append("</tr>");

          

            

            /*sb.Append("<tr>");
            sb.Append("<td style = \"background-color:#D3D3D3; width:400px\">");
            sb.Append("<strong >Payment Schedule");
            sb.Append("</strong>");
            sb.Append("</td>");
            sb.Append("</tr>");*/

            //sb.Append("<tr>");
            //sb.Append("<td>");
            //sb.Append("<hr/>");
            //sb.Append("<table border = \"0\" cellpadding = \"0\" cellspacing = \"0\" class=\"left\">");
            //sb.Append("<tbody>");
            //sb.Append("<tr>");
            //sb.Append("<td style = \"text-align:left; width:100px\" >Sl.No.</td>");
            //sb.Append("<td style=\"text-align:left; width:200px\">Transaction Date</td>");
            //sb.Append("<td style = \"text-align:left; width:100px\" > Amount </td >");
            //sb.Append("<td style = \"text-align:left; width:100px\" > Status </td >");
            //sb.Append("</tr>");
            //sb.Append("</tbody>");
            //sb.Append("</table>");
            //sb.Append("<hr/>");

            //sb.Append("</td>");
            //sb.Append("</tr>");

            //sb.Append("<tr>");
            //sb.Append("<td>");
            //sb.Append("<table border= \"0\" cellpadding= \"0\" cellspacing= \"0\" class=\"left\">");
            //sb.Append("<tbody>");
            //int sn = 1;
            //foreach (var item in lstTansactions)
            //{
            //    sb.Append("<tr>");
            //    sb.Append("<td style= \"text-align:left; width:100px\" >");
            //    sb.Append(sn);
            //    sb.Append("</td >");
            //    sb.Append("<td style= \"text-align:left; width:200px\" >");
            //    sb.Append(item.TransactionDate.ToString("MM/dd/yyyy").Replace("-", "/"));
            //    sb.Append("</td>");
            //    sb.Append("<td style= \"text-align:left; width:100px\" >");
            //    sb.Append(item.FundedAmount > 0 ? "$ "+ String.Format("{0:#,#}", item.FundedAmount) : string.Empty);
            //    sb.Append("</td>");
            //    sb.Append("<td style= \"text-align:left; width:200px\" >");
            //    sb.Append(item.TransactionStatusID == 2 ? "Disbursed" : "Pending Disbursement");
            //    sb.Append("</td>");
            //    sb.Append("</tr>");
            //    sn = sn + 1;
            //}


            
            //sb.Append("</tbody>");

            //sb.Append("</table>");

            
            //sb.Append("<tr>");
            //sb.Append("<td>");
            //sb.Append("<div style= \"margin-top: 35px; \"> </div>");
            //sb.Append("<p>");
            //sb.Append(paymentScheduleSummary.AdditionalNotesAgreement != null ? paymentScheduleSummary.AdditionalNotesAgreement : string.Empty);
            //sb.Append("</p>");

            //sb.Append("</td>");
            //sb.Append("</tr>");

            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");


            return sb.ToString();
        }

        public ThresholdAmountResponse GetThresholdRequestAmount()
        {
            try
            {
                var requestAmount= new ThresholdAmountResponse();                
                var result = _genaralOptionRepository.GetMasterOption(CommonConstants.THRESHOLD_REQUEST_FLAG);

                if(result!=null && result.Count > 0)
                {
                    requestAmount.ThresholdAmount= Convert.ToInt64(result.FirstOrDefault().OptionValue);
                }
                return requestAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetProgressReportId()
        {
            try
            {

                return this._LoanApplicationRepository.GetProgressReportId(CommonConstants.ProgressReportFLag);



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetAgreementTemplate(string AgreementText)
        {
            try
            {
                StringBuilder template = new StringBuilder();
                template.Append("<html lang='en'>");
                template.Append("<head><link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.1.1/css/bootstrap.min.css' /></head>");
                template.Append("<body><div class='container'><div>@@AgreementText</div></div></body></html>");
                template.Replace("@@AgreementText", AgreementText);
                return template.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetEmailSPAHTMLText(long applicationId)
        {
            var lstTansactions = _fundingSourceRepository.GetPaymentScheduleTransactionByLoanID(applicationId).Where(x => x.IsActive == true).OrderBy(x => x.TransactionDate).ToList();
            var loanApplication = this._LoanApplicationRepository.GetLoanApplicationByApplicationID(applicationId);
            var paymentScheduleSummary = this._fundingSourceRepository.GetPaymentScheduleSummaryByLoanID(applicationId);
            var sb = new StringBuilder();

            // sb.Append("<div><a href = \"/\"><img src = \"" + _appSettings.BaseUrl + "/img/NUL_Logo_Standalone.93b02716.png\" alt=\"logo\" style=\"height: 50px;margin-right:20px;\"><img src = \"" + _appSettings.BaseUrl + "/img/Urban-Empowerment-Fund-2020-cmyk@2x.13c4f952.png\" alt=\"logo\" style=\"height: 50px;margin-right:10px;\" ></a></div>");
            sb.Append("<div style= \"margin-top: 30px; border-top-width: 1px;border-top-style: solid;border-top-color: rgb(193, 20, 57);\"> </div>");
            sb.Append("<div style= \"margin-top: 20px; \"> </div>");
            sb.Append("<table border = \"0\" cellpadding = \"3\" cellspacing = \"0\" class=\"left\" style = \"width:100%\">");
            sb.Append("<tbody>");

            sb.Append("<tr>");
            sb.Append("<td style = \"background-color:#D3D3D3; width:400px\">");
            sb.Append("<strong >Payment Schedule");
            sb.Append("</strong>");
            sb.Append("</td>");

            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("<div style= \"margin-top: 20px; \"> </div>");
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("<table align = \"left\" border = \"0\" cellpadding = \"0\" cellspacing = \"0\" style = \"width:500px\" >");
            sb.Append("<tbody >");
            sb.Append("<tr>");
            sb.Append("<td style = \"width: 255px\" > Grant Number </td>");
            sb.Append("<td style = \"width: 17px\" >:</td>");
            sb.Append("<td style = \"width: 216px\" >");
            sb.Append(loanApplication?.LoanNumber);
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style = \"width: 255px\" >Business Name");
            sb.Append("</td>");
            sb.Append("<td style = \"width: 17px\" >");
            sb.Append(":");
            sb.Append("</td>");
            sb.Append("<td style = \"width: 216px\" >");
            sb.Append(loanApplication?.LoanBusinessDetail?.BusinessName);
            sb.Append("</td >");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style = \"width: 255px\" >Grant Program");
            sb.Append("</td >");
            sb.Append("<td style = \"width: 17px\" >");
            sb.Append(":");
            sb.Append("</td >");
            sb.Append("<td style = \"width: 216px\" >");
            sb.Append(loanApplication?.ProgramInvitation?.FundingSource?.ProgramName);

            sb.Append("</td >");
            sb.Append("</tr >");            
            sb.Append("<tr >");
            sb.Append("<td style = \"width: 255px\" >Funds Allocated");
            sb.Append("</td >");
            sb.Append("<td style = \"width: 17px\" >");
            sb.Append(":");
            sb.Append("</td >");
            sb.Append("<td style = \"width: 216px\" >");
            sb.Append(paymentScheduleSummary.FundAllocatedAmount > 0 ? "$ " + String.Format("{0:#,#}", paymentScheduleSummary.FundAllocatedAmount) : string.Empty);
            sb.Append("</td >");
            sb.Append("</tr >");
            sb.Append("</tbody >");
            sb.Append("</table >");


            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("<hr/>");
            sb.Append("<table border = \"0\" cellpadding = \"0\" cellspacing = \"0\" class=\"left\" width=\"100%\">");
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.Append("<td style = \"text-align:left; width:10%\" >Sl.No.</td>");
            sb.Append("<td style=\"text-align:left; width:30%\">Transaction Date</td>");
            sb.Append("<td style = \"text-align:left; width:20%\" > Amount </td >");
            sb.Append("<td style = \"text-align:left; width:40%\" > Status </td >");
            sb.Append("</tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("<hr/>");

            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("<table border= \"0\" cellpadding= \"0\" cellspacing= \"0\" class=\"left\" width=\"100%\">");
            sb.Append("<tbody>");
            int sn = 1;
            foreach (var item in lstTansactions)
            {
                sb.Append("<tr>");
                sb.Append("<td style= \"text-align:left; width:10%\" >");
                sb.Append(sn);
                sb.Append("</td >");
                sb.Append("<td style= \"text-align:left; width:30%\" >");
                sb.Append(item.TransactionDate.ToString("MM/dd/yyyy").Replace("-", "/"));
                sb.Append("</td>");
                sb.Append("<td style= \"text-align:left; width:20%\" >");
                sb.Append(item.FundedAmount > 0 ? "$ " + String.Format("{0:#,#}", item.FundedAmount) : string.Empty);
                sb.Append("</td>");
                sb.Append("<td style= \"text-align:left; width:40%\" >");
                sb.Append(item.TransactionStatusID == 2 ? "Disbursed" : "Pending Disbursement");
                sb.Append("</td>");
                sb.Append("</tr>");
                sn = sn + 1;
            }



            sb.Append("</tbody>");

            sb.Append("</table>");


            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("<div style= \"margin-top: 35px; \"> </div>");
            sb.Append("<p>");
            sb.Append(paymentScheduleSummary.AdditionalNotesAgreement != null ? paymentScheduleSummary.AdditionalNotesAgreement : string.Empty);
            sb.Append("</p>");

            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");


            return sb.ToString();
        }
        #endregion  Methods




    }
}
