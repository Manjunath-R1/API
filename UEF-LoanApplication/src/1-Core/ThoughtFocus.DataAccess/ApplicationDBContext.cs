using System;
using ThoughtFocus.DataAccess.Models.Master;
using Microsoft.EntityFrameworkCore;
using ThoughtFocus.DataAccess.Models.User;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.DataAccess.Models.Admin;
using ThoughtFocus.DataAccess.Models.Notification;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.DataAccess.Models.FundingSource;
using ThoughtFocus.DataAccess.Models.Application;


namespace ThoughtFocus.DataAccess
{
    public class ApplicationDBContext : ApplicationAuditDBContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<ThoughtFocus.DataAccess.Models.Master.Action> Actions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RMRoleMenuSubMenu> RMRoleMenuSubMenus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Salutation> Salutations { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<SubMenu> SubMenus { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<UserRole> RUserRoles { get; set; }
        public DbSet<UserPasswordResetInfo> UserPasswordResetInfoes { get; set; }
        public DbSet<UserProfileLoginInfo> UserProfileLoginInfoes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ActivityNotification> ActivityNotifications { get; set; }
        public DbSet<EmailTemplateHeaderFooter> EmailNotificationHeaderFooter { get; set; }
        public DbSet<EmailTemplatePlaceholders> EmailTemplatePlaceholders { get; set; }
        public DbSet<NotificationRecipientEmailAddress> NotificationRecipientEmailAddress { get; set; }
        public DbSet<EmailNotificationLog> EmailNotificationLogs { get; set; }
        public DbSet<BusinessRole> BusinessRoles { get; set; }
        public DbSet<UrbanLeagueAffiliate> UrbanLeagueAffiliates { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Ethnicity> Ethnicities { get; set; }
        public DbSet<BusinessOwner> BusinessOwners { get; set; }
        public DbSet<BusinessOwnerMaster> BusinessOwnerMasters { get; set; }
        public DbSet<Veteran> Veterans { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<LoanBusinessDetail> LoanBusinessDetails { get; set; }

        public DbSet<LoanBusinessDetailMaster> LoanBusinessDetailMaster { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<IndustryType> IndustryTypes { get; set; }
        public DbSet<LoanApplicantDetails> LoanApplicantDetails { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<LoanApplicationComment> LoanApplicationComments { get; set; }
        public DbSet<QuestionResponse> QuestionResponses { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<FundingApplication> FundingApplications { get; set; }
        public DbSet<ResponseType> ResponseTypes { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public DbSet<DocumentCategory> DocumentCategorys { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<ApplicationDocument> ApplicationDocuments { get; set; }
        public DbSet<ApplicationDocumentRequestLog> ApplicationDocumentRequestLogs { get; set; }
        public DbSet<FundingEntity> FundingEntities { get; set; }
        public DbSet<FundingSource> FundingSources { get; set; }
        public DbSet<FundingSourceBusinessTypes> FundingSourceBusinessTypes { get; set; }
        public DbSet<FundingSourceStates> FundingSourceStates { get; set; }
        public DbSet<FundTransactionDocument> TransactionDocuments { get; set; }
        public DbSet<FundUtilization> FundUtilizations { get; set; }
        public DbSet<FundTransaction> FundTransactions { get; set; }
        public DbSet<FundingType> FundingTypes { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        public DbSet<EmailTemplatePlaceholderType> EmailTemplatePlaceholderTypes { get; set; }
        public DbSet<NotificationStatus> NotificationStatus { get; set; }
        public DbSet<WorkflowNotificationType> WorkflowNotificationTypes { get; set; }
        public DbSet<NotificationRecipients> NotificationRecipients { get; set; }
        public DbSet<ContactInvitationInfo> ContactInvitationInfos { get; set; }
        public DbSet<BusinessUser> BusinessUsers { get; set; }
        public DbSet<ProgramInvitation> ProgramInvitations { get; set; }
        public DbSet<BusinessEntity> BusinessEntity { get; set; }

        public DbSet<ProgramInvitee> ProgramInvitees { get; set; }

        public DbSet<ProgramStatus> ProgramStatuses { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<LoanApplicationAgreementDetail> LoanApplicationAgreementDetails { get; set; }
        public DbSet<UserConsent> UserConsents { get; set; }

        public DbSet<UrbanLeagueAffiliateContact> UrbanLeagueAffiliateContacts { get; set; }
        public DbSet<LogoType> LogoTypes { get; set; }
        public DbSet<Logo> Logos { get; set; }
        public DbSet<ProgramQuestion> ProgramQuestions { get; set; }
        public DbSet<HelpfulGuideTemplate> HelpfulGuideTemplates { get; set; }
        public DbSet<TemplateType> TemplateType { get; set; }
        public DbSet<ProgramHelpfulGuide> ProgramHelpfulGuides { get; set; }
        public DbSet<ProgramDocument> ProgramDocuments { get; set; }
        public DbSet<CiviCRMDataExportLog> CiviCRMDataExportLogs { get; set; }
        public DbSet<CiviCRMOrganizationExportDetail> CiviCRMOrganizationExportDetails { get; set; }
        public DbSet<CiviCRMContactExportDetail> CiviCRMContactExportDetails { get; set; }
        
        public DbSet<NotificationMode> NotificationMode { get; set; }
        public DbSet<NotificationModesType> NotificationModesTypes { get; set; }


        public DbSet<SMSNotificationTemplate> SMSNotificationTemplate { get; set; }
        //public DbSet<NotificationMode> NotificationMode { get; set; }
        public DbSet<SMSNotificationLog> SMSNotificationLog { get; set; }
        public DbSet<ProgramInvitationEmailPlaceHolder> ProgramInvitationEmailPlaceHolder { get; set; }
        public DbSet<ProgramInvitationContactRole> ProgramInvitationContactRole { get; set; }
       
        public DbSet<PaymentScheduleTransaction> PaymentScheduleTransaction { get; set; }
        public DbSet<GenaralOption> GenaralOption { get; set; }
        public DbSet<PaymentScheduleSummary> PaymentScheduleSummary { get; set; }
        public DbSet<FundAgreementDocuments> FundAgreementDocuments { get; set; }
        public DbSet<PaymentScheduleStatus> PaymentScheduleStatus { get; set; }
        public DbSet<LoanApplicationSchedulePaymentAreementDetail> LoanApplicationSchedulePaymentAreementDetails { get; set; }
        public DbSet<PaymentSummaryTransactionNotify> PaymentSummaryTransactionNotifys { get; set; }
    }
}
