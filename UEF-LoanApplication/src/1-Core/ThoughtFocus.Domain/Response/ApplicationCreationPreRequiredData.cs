using System;
using System.Collections.Generic;
using ThoughtFocus.Domain.Master;
using ThoughtFocus.Domain.Params;

namespace ThoughtFocus.Domain.Response
{
    public class ApplicationCreationPreRequiredData
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string BusinessName { get; set; }
        public string Affiliate { get; set; }
        public long AffiliateID { get; set; }
        public string EIN { get; set; }
        public string BusinessStartdate { get; set; }
        public string ProgramName { get; set; }
        public string FundingSource { get; set; }
        public string ProgramLogoSource { get; set; }
        public bool showProgramLogo { get; set; }
        public string FundingEntityLogoSource { get; set; }
        public bool showFundingEntityLogo { get; set; }
        public bool HasDefaultFundAmount { get; set; }
        public string BussinessProfileHelpfulGuideTemplate { get; set; }
        public string FundingApplicationHelpfulGuideTemplate { get; set; }
        public string DocumentTabHelpfulGuideTemplate { get; set; }
        public string ReviewTabHelpfulGuideTemplate { get; set; }
        public Nullable<decimal> RequestedFundAmount { get; set; }
        public Nullable<decimal> MinimumFundAmount { get; set; }
        public Nullable<decimal> MaximumFundAmount { get; set; }
        public List<StateEntity> StateList { get; set; }
        public List<BusinessTypeEntity> BusinessTypeList { get; set; }
        public List<IndustryTypeEntity> IndustryTypeList { get; set; }
        public List<GenderEntity> GenderList { get; set; }
        public List<RaceEntity> RaceList { get; set; }
        public List<EthnicityEntity> EthnicityList { get; set; }
        public List<VeteranEntity> VeteranList { get; set; }
        public List<ProgramQuestion> ProgramQuestions { get; set; }
        public List<ProgramDocuments> ProgramDocuments { get; set; }

        public List<BusinessOwnerMasterParam> BusinessOwnerMasterParam { get; set; }
        
        public LoanBusinessDetailMasterPreParam LoanBusinessDetailMasterPreParam { get; set; }
        public int ProgressReportId { get; set; }
        public bool? IsPaymentSchedule { get; set; }
    }

    public class ProgramQuestion
    {
        public Nullable<long> QuestionID { get; set; }
        public string QuestionText { get; set; }
        public bool IsRequired { get; set; }
        public int ResponseType { get; set; }
        public string Response { get; set; }
    }

    public class ProgramDocuments
    {
        public long ApplicationDocumentID { get; set; }
        public Guid DocumentGUID { get; set; }
        public long ProgramDocumentID { get; set; }
        public long DocumentID { get; set; }
        public int? DocumentCategoryID { get; set; }
        public long? ProgramID { get; set; }
        public bool IsRequired { get; set; }
        public string Response { get; set; }
        public long? DisplayOrder { get; set; }
        public string DocumentName { get; set; }
        public bool IsActive { get; set; }
        public int DocumentTypeID { get; set; }
        public string FileName { get; set; }
        public string PhysicalFileStorageKey { get; set; }
        public long FileSize { get; set; }
        public long LoanApplicationID​​​​​​​​ { get; set; }
        public string FileSource { get; set; }
    }
}
