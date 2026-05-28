using System;
using Xunit;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Business.Interfaces.Contact;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.Contact;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Service.Interfaces;
using Moq;
using ThoughtFocus.Service.Impl;
using ThoughtFocus.Constants;
using ThoughtFocus.Repository.Interfaces.Contact;
using ThoughtFocus.Repository.Interfaces.User;
using AutoMapper;
using System.Collections.Generic;
using ThoughtFocus.Service.ModelsMapping;
using ThoughtFocus.UnitTests.DataProvider;
using ThoughtFocus.DataAccess;
using ThoughtFocus.Repository.Impl.User;
using ThoughtFocus.Repository.Impl.Contact;
using Microsoft.AspNetCore.Routing;
using ThoughtFocus.Repository.Interfaces.ContactManagement;
using ThoughtFocus.Services.Interfaces;
using Microsoft.Extensions.Options;
using ThoughtFocus.Services.impl;
using ThoughtFocus.Business.Impl.EmailTemplate;
using ThoughtFocus.Repository.Impl.Notification;
using ThoughtFocus.Repository.Interfaces.Notification;
using ThoughtFocus.Repository.Impl.Master;
using ThoughtFocus.Common.Utilities;
using ThoughtFocus.Repository.Impl.Admin;

namespace ThoughtFocus.UnitTests.Services
{
    public class ContactServiceTests
    {
        #region Fields
        ApplicationDBContext context;
        
        public readonly IUserRepository userRepo;
        public readonly IContactRepository contactRepo;
        public readonly IBusinessContactRepository businessContactRepo;
        public readonly IContactService contactService;
       public readonly IUserPasswordResetInfoRepository userPasswordResetInfoRepository;

        #endregion Fields

        public ContactServiceTests()
        {
            var db = new FakeAppDBContext();
            context = db.CreateContextForInMemory();

            var mockUserRepoLogger = new Mock<ILogger<UserRepositoryImpl>>();
            userRepo = new UserRepositoryImpl(context, mockUserRepoLogger.Object);

            contactRepo = new ContactRepository(context);
            businessContactRepo = new BusinessContactRepository(context);

            var contactInfo = new ModelMappingConfiguration();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(contactInfo));
            IMapper mapper = new Mapper(configuration);

            var modelMapping = new Mock<ModelMappingConfiguration>();
            var mockContactServiceLogger = new Mock<ILogger<ContactService>>();
            //contactService = new ContactService(mockContactServiceLogger.Object, contactRepo, userRepo, mapper);


            var linkGenerator = new Mock<LinkGenerator>();
            var contactInvitationRepository = new Mock<IContactInvitationInfoRepository>();
            

            var mockActivityLogger = new Mock<ILogger<ActivityNotificationRepository>>();
            var mockRecipientsLogger = new Mock<ILogger<NotificationRecipientsRepository>>();
            var notificationRepository = new NotificationRepository(context);
            var activityNotificationRepository = new ActivityNotificationRepository(context, mockActivityLogger.Object);
            var emailNotificationHeaderFooterRepository = new EmailNotificationHeaderFooterRepository(context);
            var emailNotificationLogRepository = new EmailNotificationLogRepository(context);
            var emailPlaceholderRepository = new EmailPlaceholderRepository(context);
            var notificationRecipientEmailAddressRepository = new NotificationRecipientEmailAddressRepository(context);
            var rActivityNotificationRepository = new ActivityNotificationRepository(context,mockActivityLogger.Object);
            var notificationRecipientsRepository = new NotificationRecipientsRepository(context, mockRecipientsLogger.Object);
            var programInvitationRepository = new ProgramInvitationRepository(context);
            

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
            var mockEmailManager = new EmailManager(preEmailConditionManager, mockPostEmailActionManager, mockLogger.Object,emailSettingsOptions,mockSendEmail);

           
            
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
            appSettings.Object.PasswordSalt = "A**r$d!tat!0nA^A@h0u%h}{0c+$2o2L";
            IOptions<ThoughtFocus.DataAccess.Models.AppSettings> appSettingsOptions = Options.Create(appSettings.Object);

            var mockEmailTemplateManager = new EmailTemplateManager(mockPlaceHoldersManager, mockEmailManager, mockEmailTemplateLogger.Object,rActivityNotificationRepository,appSettingsOptions,null,null,null,null,null,null,null,null,null,null,null);

            var mockLoggerService = new Mock<ILogger<NotificationService>>();
            var notificationService  = new NotificationService(mockEmailTemplateManager,mockLoggerService.Object, null);

     
            contactService = new ContactService(mockContactServiceLogger.Object, contactRepo, userRepo, mapper, notificationService, contactInvitationRepository.Object,appSettingsOptions, businessContactRepo, null,userPasswordResetInfoRepository,null,null);
        }

        [Fact]
        public void CheckEmailExists()
        {
            var result = contactService.CheckEmailExists("Archanyogeesh@yopmail.com");
            Assert.Equal("Success", result.ResponseStatus.ToString());
        }

        [Fact]
        public void CheckEmailDoesnotExists()
        {
            var result = contactService.CheckEmailExists("xyz@yopmail.com");
            Assert.Equal("Fail", result.ResponseStatus.ToString());

        }


        [Fact]
        public void InviteContact()
        {   
            var contact = new Mock<ContactRequest>().Object;
        
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            var CommonResponse = new Mock<CommonResponse>(ResponseStatus.Success, MessageConstants.Success, null).Object;

            contact.FirstName = "Namratha";
            contact.MiddleName = "";
            contact.LastName = "Dixith";
            contact.PhoneNo = "9809890909";
            contact.EmailAddress = "dixith@yopmail.com";
            contact.IsActive = true;
            contact.SalutationID = 1;
            contact.AccountStatusID = 2;
            contact.UserRoles = new List<int> {2,4};

            var result = contactService.AddContact(contact,userSessionEntity);
            Assert.Equal("Success",result.ResponseStatus.ToString());
        }

        [Fact]
        public void CannotInviteContact()
        {   
            var contact = new Mock<ContactRequest>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            var CommonResponse = new Mock<CommonResponse>(ResponseStatus.Success, MessageConstants.Success, null).Object;


            contact.FirstName = "123";
            contact.MiddleName = "";
            contact.LastName = "";
            contact.PhoneNo = "9809890909";
            contact.EmailAddress = "namratha@yopmail.com";
            contact.IsActive = true;
            contact.SalutationID = 1;
            contact.AccountStatusID = 2;
            contact.UserRoles = new List<int> {2,4};

            var result = contactService.AddContact(contact,userSessionEntity);
            Assert.Equal("Fail",result.ResponseStatus.ToString());
        }

        [Fact]
        public void GetContact()
        {
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            ContactViewEntityResponse contactViewEntity = contactService.GetContactInfoById(1, userSessionEntity);
            Assert.True(contactViewEntity.IsSuccess);

            contactViewEntity = contactService.GetContactInfoById(3, userSessionEntity);
            Assert.False(contactViewEntity.IsSuccess);
        }




        [Fact]
        public void DeleteContact()
        {
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            var result = contactService.DeleteContact(1, userSessionEntity);
            Assert.Equal("Success", result.ResponseStatus.ToString());

            ContactViewEntityResponse contactViewEntity = contactService.GetContactInfoById(1, userSessionEntity);
            Assert.False(contactViewEntity.IsSuccess);
        }

        [Fact]
        public void DeleteteNonExistingContact()
        {
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            var result = contactService.DeleteContact(3, userSessionEntity);
            Assert.Equal("Fail", result.ResponseStatus.ToString());
        }

        [Fact]
        public void GetBusinessUserList()
        {
            ContactListResponse result = contactService.GetBusinessContacts(1);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void CannotGetBusinessUserList()
        {
            ContactListResponse result = contactService.GetBusinessContacts(3);
            Assert.False(result.IsSuccess);
        }

        
        

    }
}
