using System.Collections.Generic;
using ThoughtFocus.Domain.AffiliateContact;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Notification;

namespace ThoughtFocus.Domain.Response
{
    public class BusinessEntityListResponse : BaseResponse
    {
        public List<BusinessEntityListingView> businessEntityListResponse { get; set; }
    }

    public class ProgramInvitationResponse : BaseResponse
    {
        public List<ProgramResponse> Programs { get; set; }
        public List<ProgramInvitationListingView> ProgramInvitations { get; set; }
    }

    public class BusinessProgramInvitationResponse : BaseResponse
    {
        public List<BusinessProgramInvitationListingView> BusinessProgramInvitations { get; set; }
    }

    public class ProgramInvitationPreRequiredDataResponse : BaseResponse
    {
        public List<BusinessEntities> BusinessEntities { get; set; }

        public List<ContactResponse> Contacts { get; set; }

        public List<ProgramResponse> Programs { get; set; }
        public List<FundingEntitiesResponse> FundingEntities { get; set; }
        public bool IsAllSelect { get; set; }
    }

    public class BusinessUserResponse : BaseResponse
    {
        public List<ContactResponse> Contacts { get; set; }

    }
    public class ConsolidatedReportDataResponse : BaseResponse
    {
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public List<long?> ProgramID { get; set; }
        public Dictionary<string, string> CumulativeReportData = new Dictionary<string, string>();
    }

    public class AffiliateContactResponse : BaseResponse
    {
        public List<AffiliateContactEntity> data { get; set; }
        public int recordsTotal { get; set; }
        public long AffiliateID { get; set; }

    }

    public class ContactResponse
    {
        public long ContactID
        {
            get;
            set;
        }
        public string FullName
        {
            get;
            set;
        }

    }

    public class BusinessEntities
    {
        #region Properties

        public long ID
        {
            get;
            set;
        }

        public string BusinessName
        {
            get;
            set;
        }


        #endregion Properties
    }

    public class ProgramResponse
    {
        public long ProgramId
        {
            get;
            set;
        }
        public string ProgramName
        {
            get;
            set;
        }

    }
    public class FundingEntitiesResponse
    {
        public long FundingEntityId
        {
            get;
            set;
        }
        public string FundingEntityName
        {
            get;
            set;
        }

    }
    public class ConsolidatedReportExportDataResponse
    {
        public long BusinessID
        {
            get;
            set;
        }
        public long ProgramInvitationID
        {
            get;
            set;
        }
        public string BusinessName { get; set; }
        public string AffiliateName { get; set; }
        public string ProgramName { get; set; }
        public int ApplicationInvited { get; set; }
        public int ActivatedAccounts { get; set; }
        public int ApplicationStarted { get; set; }
        public int ApplicationSubmitted { get; set; }
        public int ApplicationFunded { get; set; }
        public decimal Fundreleased { get; set; }

    }
    public class QuestionsResponse : BaseResponse
    {
        public List<QuestionsViewEntity> QuestionsRecords { get; set; }
        public PageResultEntity<QuestionsViewEntity> QuestionsPageResultEntity { get; set; }
        public int recordsTotal { get; set; }
        public long QuestionID { get; set; }

    }
    public class DocumentsResponse : BaseResponse
    {
        public List<DocumentViewEntity> DocumentRecords { get; set; }
        public PageResultEntity<DocumentViewEntity> DocumentsPageResultEntity { get; set; }
        //public long DocumentTypeID { get; set; }
        public int recordsTotal { get; set; }

    }
    public class ProgramAgreementResponse : BaseResponse
    {
        public long? AgreementID { get; set; }
        public long ProgramIdID { get; set; }
        public string AgreementName { get; set; }
        public string AgreementBody { get; set; }

    }
    public class NotificationTypeResponse : BaseResponse
    {
        public long NotificationModeTypeID { get; set; }
        public bool IsNotificationModelType { get; set; }
        public string NotificationModeTypeText { get; set; }
        public long NotificationID { get; set; }
        public List<NotificationTypeEntity> NotificationTypeEntity { get; set; }

    }
    public class ProgramsResponse : BaseResponse
    {
        public List<ProgramResponse> Programs { get; set; }

    }
}
