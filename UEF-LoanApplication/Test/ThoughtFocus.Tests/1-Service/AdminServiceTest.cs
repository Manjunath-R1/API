
using AutoMapper;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ThoughtFocus.Business.Impl.EmailTemplate;
using ThoughtFocus.Common.Utilities;
using ThoughtFocus.Constants;
using ThoughtFocus.DataAccess;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Repository.Impl.Admin;
using ThoughtFocus.Repository.Impl.Contact;
using ThoughtFocus.Repository.Impl.ContactManagement;
using ThoughtFocus.Repository.Impl.Master;
using ThoughtFocus.Repository.Impl.Notification;
using ThoughtFocus.Repository.Interfaces.Contact;
using ThoughtFocus.Service.Impl;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.Service.ModelsMapping;
using ThoughtFocus.Services.impl;
using ThoughtFocus.UnitTests.DataProvider;
using Xunit;

namespace ThoughtFocus.UnitTests.Services
{
    public class AdminServiceTest
    {
        #region Fields
        ApplicationDBContext context;


        public readonly IAdminService adminService;

        #endregion Fields

        public AdminServiceTest()
        {
            var db = new FakeAppDBContext();
            context = db.CreateContextForInMemory();
            var mockAdminServiceLogger = new Mock<ILogger<AdminService>>();
            var mapperInfo = new ModelMappingConfiguration();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapperInfo));
            IMapper mapper = new Mapper(configuration);

            var businessRepo = new BusinessEntityRepository(context);

            var ProgramInvitation = new ProgramInvitationRepository(context);

            var businessUserRepo = new BusinessContactRepository(context);

            //invitation email
            var linkGenerator = new Mock<LinkGenerator>();

            var mockActivityLogger = new Mock<ILogger<ActivityNotificationRepository>>();
            var mockRecipientsLogger = new Mock<ILogger<NotificationRecipientsRepository>>();
            var notificationRepository = new NotificationRepository(context);
            var activityNotificationRepository = new ActivityNotificationRepository(context, mockActivityLogger.Object);
            var emailNotificationHeaderFooterRepository = new EmailNotificationHeaderFooterRepository(context);
            var emailNotificationLogRepository = new EmailNotificationLogRepository(context);
            var emailPlaceholderRepository = new EmailPlaceholderRepository(context);
            var notificationRecipientEmailAddressRepository = new NotificationRecipientEmailAddressRepository(context);
            var notificationRecipientsRepository = new NotificationRecipientsRepository(context, mockRecipientsLogger.Object);
            var rActivityNotificationRepository = new ActivityNotificationRepository(context, mockActivityLogger.Object);
            var contactInvitationInfoRepository = new ContactInvitationInfoRepository(context);
            var programInvitationRepository = new ProgramInvitationRepository(context);
            var  contactRepository = new ContactRepository(context);
            var preEmailConditionManager = new PreEmailConditionManager();
            var mockPostEmailActionManager = new PostEmailActionManager(preEmailConditionManager, emailNotificationLogRepository);
            var mockLogger = new Mock<ILogger<EmailManager>>();

            var emailSettings = new Mock<EmailSettings>();
            emailSettings.Object.SmtpServerAddress = "smtp.gmail.com";
            emailSettings.Object.SmtpServerPort = 587;
            emailSettings.Object.SmtpServerEnableSSL = true;
            emailSettings.Object.EmailUserName = "TFemailtesting123@gmail.com";
            emailSettings.Object.EmailPassWord = "^@bAuat$3r#";
            emailSettings.Object.EmailFromUserName = "TFemailtesting123@gmail.com";
            emailSettings.Object.IsSendMail = false;
            IOptions<EmailSettings> emailSettingsOptions = Options.Create(emailSettings.Object);

            var mockSendEmailLogger = new Mock<ILogger<SendEmail>>();
            var mockSendEmail = new SendEmail(mockSendEmailLogger.Object);
            var mockEmailManager = new EmailManager(preEmailConditionManager, mockPostEmailActionManager, mockLogger.Object, emailSettingsOptions, mockSendEmail);

            var mockPlaceHoldersManagerLogger = new Mock<ILogger<PlaceHoldersManager>>();
            var mockPlaceHoldersManager = new PlaceHoldersManager(
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

            var mockEmailTemplateManager = new EmailTemplateManager(mockPlaceHoldersManager, mockEmailManager, mockEmailTemplateLogger.Object, rActivityNotificationRepository, appSettingsOptions, null, null, null, null, null,null,null,null, null,null,null);

            var mockLoggerService = new Mock<ILogger<NotificationService>>();
            var notificationService = new NotificationService(mockEmailTemplateManager, mockLoggerService.Object, null);

            var UrbanLeagueAffiliateContacts = new UrbanLeagueAffiliateRepositoryImpl(context);

            adminService = new AdminService(mockAdminServiceLogger.Object,
                                            mapper,
                                            businessRepo,
                                            notificationService,
                                            ProgramInvitation,
                                            appSettingsOptions,
                                            null,
                                            null,
                                            businessUserRepo,
                                            contactInvitationInfoRepository,
                                            null,
                                            null,
                                            null,
                                            null,
                                            null, null, null,
                                            null, null, null
                                            , null, null, null, null, null,null);


        }

        [Fact]
        public void AddBusinessEntity()
        {
            var businessEntity = new Mock<BusinessEntityRequest>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;
            var CommonResponse = new Mock<CommonResponse>(ResponseStatus.Success, MessageConstants.Success, null).Object;
            businessEntity.AffiliateID = 1;
            businessEntity.BusinessName = "Test";
            businessEntity.EIN = "12-8764435";
            businessEntity.BusinessTypeID = 1;
            var result = adminService.AddBusinessEntity(businessEntity, userSessionEntity);
            Assert.Equal("Success", result.ValidationErrors[0].ToString());
        }


        [Fact]
        public void AddNonExistingBusinessEntityAffiliate()
        {
            var businessEntity = new Mock<BusinessEntityRequest>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;
            var CommonResponse = new Mock<CommonResponse>(ResponseStatus.Fail, MessageConstants.Failure, null).Object;
            businessEntity.AffiliateID = 0;
            businessEntity.BusinessName = "Test";
            businessEntity.EIN = "12-8764435";
            businessEntity.BusinessTypeID = 1;
            var result = adminService.AddBusinessEntity(businessEntity, userSessionEntity);
            Assert.Equal("Please select Affiliate", result.ValidationErrors[0].ToString());
        }


        [Fact]
        public void AddBusinessEntityWithoutBusinessName()
        {
            var businessEntity = new Mock<BusinessEntityRequest>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;
            var CommonResponse = new Mock<CommonResponse>(ResponseStatus.Success, MessageConstants.Success, null).Object;
            businessEntity.AffiliateID = 1;
            businessEntity.EIN = "12-8764435";
            businessEntity.BusinessTypeID = 1;
            var result = adminService.AddBusinessEntity(businessEntity, userSessionEntity);
            Assert.Equal("Please enter Business Name", result.ValidationErrors[0].ToString());
        }
        [Fact]
        public void AddBusinessEntityWithoutEIN()
        {
            var businessEntity = new Mock<BusinessEntityRequest>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;
            var CommonResponse = new Mock<CommonResponse>(ResponseStatus.Success, MessageConstants.Success, null).Object;
            businessEntity.BusinessName = "Test";
            businessEntity.AffiliateID = 1;
            businessEntity.BusinessTypeID = 1;
            var result = adminService.AddBusinessEntity(businessEntity, userSessionEntity);
            Assert.Equal("Please enter EIN", result.ValidationErrors[0].ToString());
        }

        [Fact]
        public void SaveProgramInvitation()
        {
            var programInvitation = new Mock<ProgramInvitationRequest>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            programInvitation.BusinessID = 1;
            programInvitation.ProgramID = 1;
            programInvitation.ProgramStatusID = 1;
            programInvitation.ContactID = 1;

            var result = adminService.SaveProgramInvitation(programInvitation, userSessionEntity);
            Assert.Equal("Success", result.ResponseStatus.ToString());
        }

        [Fact]
        public void SaveProgramInvitationWithoutBusiness()
        {
            var programInvitation = new Mock<ProgramInvitationRequest>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            programInvitation.BusinessID = 0;
            programInvitation.ProgramID = 1;
            programInvitation.ProgramStatusID = 1;

            var result = adminService.SaveProgramInvitation(programInvitation, userSessionEntity);
            Assert.Equal("Fail", result.ResponseStatus.ToString());
        }

        [Fact]
        public void SaveProgramInvitationWithoutProgram()
        {
            var programInvitation = new Mock<ProgramInvitationRequest>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            programInvitation.BusinessID = 2;
            programInvitation.ProgramID = 0;
            programInvitation.ProgramStatusID = 1;

            var result = adminService.SaveProgramInvitation(programInvitation, userSessionEntity);
            Assert.Equal("Fail", result.ResponseStatus.ToString());
        }
    }
}