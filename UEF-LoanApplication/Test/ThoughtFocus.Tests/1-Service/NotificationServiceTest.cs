
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ThoughtFocus.Business.Impl.EmailTemplate;
using ThoughtFocus.Business.Interfaces.EmailTemplate;
using ThoughtFocus.Common.Utilities;
using ThoughtFocus.DataAccess;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Repository.Impl.Admin;
using ThoughtFocus.Repository.Impl.Contact;
using ThoughtFocus.Repository.Impl.FundingSource;
using ThoughtFocus.Repository.Impl.Master;
using ThoughtFocus.Repository.Impl.Notification;
using ThoughtFocus.Repository.Interfaces.Admin;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.Repository.Interfaces.Contact;
using ThoughtFocus.Repository.Interfaces.Notification;
using ThoughtFocus.Services.impl;
using ThoughtFocus.Services.Interfaces;
using ThoughtFocus.UnitTests.DataProvider;
using ThoughtFocusRepository.Interfaces.Master;
using Xunit;


namespace ThoughtFocus.UnitTests.Services
{
    public class NotificationServiceTest
    {
        #region Fields

        ApplicationDBContext context;
        public readonly INotificationService mockNotificationService;
        public readonly IEmailManager mockEmailManager;

        public readonly IEmailTemplateManager mockEmailTemplateManager;

        public readonly INotificationRepository notificationRepository;
        public readonly IActivityNotificationRepository activityNotificationRepository;

        public readonly IEmailNotificationHeaderFooterRepository emailNotificationHeaderFooterRepository;
        public readonly IEmailNotificationLogRepository emailNotificationLogRepository;

        public readonly IEmailPlaceholderRepository emailPlaceholderRepository;

        public readonly INotificationRecipientEmailAddressRepository notificationRecipientEmailAddressRepository;

        public readonly INotificationRecipientsRepository notificationRecipientsRepository;
        public readonly IProgramInvitationRepository programInvitationRepository;

        public readonly IPlaceHoldersManager mockPlaceHoldersManager;
        public readonly IPreEmailConditionManager preEmailConditionManager;
        public IFundingApplicationRepository fundingSourceRepository;
        private IContactRepository contactRepository;

        #endregion Fields

        public NotificationServiceTest()
        {


            var db = new FakeAppDBContext();
            context = db.CreateContextForInMemory();


            var mockActivityLogger = new Mock<ILogger<ActivityNotificationRepository>>();
            var mockRecipientsLogger = new Mock<ILogger<NotificationRecipientsRepository>>();
            notificationRepository = new NotificationRepository(context);
            activityNotificationRepository = new ActivityNotificationRepository(context, mockActivityLogger.Object);
            emailNotificationHeaderFooterRepository = new EmailNotificationHeaderFooterRepository(context);
            emailNotificationLogRepository = new EmailNotificationLogRepository(context);
            emailPlaceholderRepository = new EmailPlaceholderRepository(context);
            notificationRecipientEmailAddressRepository = new NotificationRecipientEmailAddressRepository(context);
            notificationRecipientsRepository = new NotificationRecipientsRepository(context, mockRecipientsLogger.Object);
            var rActivityNotificationRepository = new ActivityNotificationRepository(context,mockActivityLogger.Object);
            programInvitationRepository = new ProgramInvitationRepository(context);
            contactRepository = new ContactRepository(context);

            //var mockEmailSettings = new Mock<IOptions<EmailSettings>>();

            preEmailConditionManager = new PreEmailConditionManager();

            var mockPostEmailActionManager = new PostEmailActionManager(preEmailConditionManager, emailNotificationLogRepository);
            var mockLogger = new Mock<ILogger<EmailManager>>();

            var emailSettings = new Mock<EmailSettings>();
            emailSettings.Object.SmtpServerAddress = "smtp.gmail.com";
            emailSettings.Object.SmtpServerPort = 587;
            emailSettings.Object.SmtpServerEnableSSL = true;
            emailSettings.Object.EmailUserName = "TFemailtesting123@gmail.com";
            emailSettings.Object.EmailPassWord = "^@bAuat$3r#";
            emailSettings.Object.EmailFromUserName = "TFemailtesting123@gmail.com";
            emailSettings.Object.IsSendMail = true;
            IOptions<EmailSettings> emailSettingsOptions = Options.Create(emailSettings.Object);
            var mockSendEmailLogger = new Mock<ILogger<SendEmail>>();
            var mockSendEmail = new SendEmail(mockSendEmailLogger.Object);
            mockEmailManager = new EmailManager(preEmailConditionManager, mockPostEmailActionManager, mockLogger.Object, emailSettingsOptions, mockSendEmail);


            var mockPlaceHoldersManagerLogger = new Mock<ILogger<PlaceHoldersManager>>();
            mockPlaceHoldersManager = new PlaceHoldersManager(
              notificationRecipientEmailAddressRepository,
              preEmailConditionManager,
              emailPlaceholderRepository,
              notificationRecipientsRepository,
              activityNotificationRepository,
              emailNotificationHeaderFooterRepository,
              notificationRepository,
             mockPlaceHoldersManagerLogger.Object,
             emailSettingsOptions,
             programInvitationRepository,null
             );

            var mockEmailTemplateLogger = new Mock<ILogger<EmailTemplateManager>>();
             var appSettings = new Mock<ThoughtFocus.DataAccess.Models.AppSettings>();            
            IOptions<ThoughtFocus.DataAccess.Models.AppSettings> appSettingsOptions = Options.Create(appSettings.Object);
            var mockEmailTemplateManager = new EmailTemplateManager(mockPlaceHoldersManager, mockEmailManager, mockEmailTemplateLogger.Object,rActivityNotificationRepository,appSettingsOptions,null,null,null,null,null,null,null,null,null,null,null);

            var mockLoggerService = new Mock<ILogger<NotificationService>>();
            mockNotificationService = new NotificationService(mockEmailTemplateManager, mockLoggerService.Object, null);
        }

        [Fact]
        public void CheckSendInvitation()
        {
            var userSessionEntity = new Mock<UserSessionEntity>();
            userSessionEntity.Object.UserID = 2;
            var result = mockNotificationService.SendContactEmail("Activation email", "https://uef-api-dev.azurewebsites.net/swagger/index.html", 1, 2, "Test Additional message", userSessionEntity.Object);
            Assert.True(result);
        }

        [Fact]
        public void CheckSendProgramInvitation()
        {
            var userSessionEntity = new Mock<UserSessionEntity>();
            userSessionEntity.Object.UserID = 2;
            var result = mockNotificationService.SendProgramInvitationEmail("Activation email", 1, 0,"https://uef-api-dev.azurewebsites.net/swagger/index.html", 2, userSessionEntity.Object, "ContactUs@nul.org");
            Assert.True(result);
        }



    }
}