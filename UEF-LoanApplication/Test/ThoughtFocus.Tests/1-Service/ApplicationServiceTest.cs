using Xunit;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Service.Interfaces;
using Moq;  
using ThoughtFocus.Service.Impl;
using ThoughtFocus.Repository.Interfaces.Application;
using AutoMapper;
using ThoughtFocus.Repository.Impl.Application;
using ThoughtFocus.Business.Interfaces.Application;
using ThoughtFocus.Service.ModelsMapping;
using ThoughtFocus.DataAccess;
using ThoughtFocus.UnitTests.DataProvider;
using ThoughtFocus.Business.Impl.Application;
using ThoughtFocus.Workflow;
using System.Collections.Generic;
using System;
using ThoughtFocus.RoleProvider.Interfaces;
using ThoughtFocus.Repository.Interfaces.User;
using ThoughtFocus.RoleProvider.Impl;
using ThoughtFocus.Repository.Impl.Master;
using Microsoft.EntityFrameworkCore;
using ThoughtFocus.Repository.Impl.User;
using ThoughtFocus.Repository.Interfaces.Admin;
using ThoughtFocus.Repository.Impl.Contact;
using ThoughtFocus.Repository.Impl.Admin;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.Core.Impl;
using ThoughtFocus.Repository.Interfaces.Master;

namespace ThoughtFocus.UnitTests.Services
{
    public class ApplicationServiceTest
    {
        #region Fields
        public readonly IApplicationService mockApplicationService;
        public readonly ILoanApplicationRepository mockLoanApplicationRepo;
        public readonly IBusinessOwnerRepository mockBusinessOwnerRepo;
        public readonly ILoanApplication mockLoanApplicationBusiness;

        public readonly IUserRepository mockUserRepository;
        ApplicationDBContext context;
        public readonly IProgramInvitationRepository mockProgramInvitationRepo;
        private readonly IApplicationDocumentRepository mockapplicationDocumentRepository;
        //private readonly IDocumentInformationProvider mockdocumentInformationProvider;
        private readonly IFundingApplicationRepository mockfundingApplicationRepository;

        private readonly IQuestionResponseRepository mockquestionResponseRepository;

        private readonly ILoanBusinessDetailRepository mockloanBusinessDetailRepository;

        public readonly IUrbanLeagueAffiliateRepository mockIUrbanLeagueAffiliateRepository;


        #endregion Fields

        #region Constructors
        public ApplicationServiceTest()
        {
            var db = new FakeAppDBContext();
            context = db.CreateContextForInMemory();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelMappingConfiguration());
            });
            var mapper = mockMapper.CreateMapper();

            var mockLogger_LoanApplicationBusiness = new Mock<ILogger<LoanApplicationImpl>>(); 
            mockLoanApplicationRepo = new LoanApplicationRepository(context);
            
            var mockLogger_UserRepo = new Mock<ILogger<UserRepositoryImpl>>(); 
            mockUserRepository = new UserRepositoryImpl(context, mockLogger_UserRepo.Object);

            mockProgramInvitationRepo = new ProgramInvitationRepository(context);
            mockBusinessOwnerRepo = new BusinessOwnerRepository(context);

            mockLoanApplicationBusiness = new LoanApplicationImpl(mockLogger_LoanApplicationBusiness.Object, mockLoanApplicationRepo, null,null);
          
            var mockLogger_ApplicationService = new Mock<ILogger<ApplicationServiceImpl>>();

            mockapplicationDocumentRepository = new ApplicationDocumentRepository(context,null);
            mockfundingApplicationRepository = new FundingApplicationRepository(context);
            mockloanBusinessDetailRepository = new LoanBusinessDetailRepository(context);
            mockquestionResponseRepository = new QuestionResponseRepository(context);

            mockIUrbanLeagueAffiliateRepository = new UrbanLeagueAffiliateRepositoryImpl(context);

            //mockdocumentInformationProvider = new DocumentInformationProvider(context);

            mockApplicationService = new ApplicationServiceImpl(mockLogger_ApplicationService.Object, 
                                                mapper, 
                                                mockLoanApplicationRepo, 
                                                mockLoanApplicationBusiness, 
                                                mockUserRepository,
                                                mockProgramInvitationRepo, 
                                                mockapplicationDocumentRepository,
                                                null,null,mockBusinessOwnerRepo, 
                                                mockloanBusinessDetailRepository,
                                                null,
                                                mockfundingApplicationRepository,
                                                mockquestionResponseRepository,
                                                
                                                mockIUrbanLeagueAffiliateRepository
                                                ,null,null,null,null,null,null,null,null,null,null,null,null,null); 

        }
        #endregion Constructors

        #region Methods
        // [Fact]
        // public void CanCreateLoanApplication()
        // {             
        //     var loanApplicationRequest = new Mock<LoanApplicationRequest>().Object;
        //     var userSessionEntity = new Mock<UserSessionEntity>().Object;

        //     //mock user session
        //     userSessionEntity.UserID = 3;

        //     FakeLoanApplicationData fakeLoanApplicationData = new FakeLoanApplicationData();
            
        //     loanApplicationRequest = fakeLoanApplicationData.GetFakeLoanApplicationParam();
            

        //     var resultWithValidInput = mockApplicationService.SaveLoanApplication(loanApplicationRequest,userSessionEntity);
        //     Assert.Equal("Success",resultWithValidInput.ResponseStatus.ToString());
        // }

    //    [Fact]
    //     public void Should_Fail_Loan_Application_Creation_When_Entered_Invalid_SSN()
    //     {             
    //           var loanApplicationRequest = new Mock<LoanApplicationRequest>().Object;
    //         var userSessionEntity = new Mock<UserSessionEntity>().Object;

    //         //mock user session
    //         userSessionEntity.UserID = 3;

    //         FakeLoanApplicationData fakeLoanApplicationData = new FakeLoanApplicationData();
            
    //         loanApplicationRequest = fakeLoanApplicationData.GetFakeLoanApplicationParam();

    //         //replace SSN with wrong one
    //         loanApplicationRequest.LoanApplicantDetails.SSN = "85678";

    //         var resultWithInvalidSSNNumber = mockApplicationService.SaveLoanApplication(loanApplicationParam,userSessionEntity);
    //         Assert.Equal("Fail",resultWithInvalidSSNNumber.ResponseStatus.ToString());
    //         Assert.Equal("please enter valid ssn number",resultWithInvalidSSNNumber.ValidationErrors[0].ToLower());
    //     }
        
        // [Fact]
        // public void Should_Fail_Loan_Application_Creation_When_Entered_Invalid_Email()
        // {             
        //     var loanApplicationParam = new Mock<LoanApplicationParam>().Object;
        //     var userSessionEntity = new Mock<UserSessionEntity>().Object;

        //     //mock user session
        //     userSessionEntity.UserID = 3;

        //     FakeLoanApplicationData fakeLoanApplicationData = new FakeLoanApplicationData();
            
        //     loanApplicationParam = fakeLoanApplicationData.GetFakeLoanApplicationParam();

        //     //replace Email with wrong one
        //     loanApplicationParam.LoanApplicantDetails[1].EmailAddress = "wrongemailaddress";

        //     var resultWithInvalidEmailAddress = mockApplicationService.SaveLoanApplication(loanApplicationParam,userSessionEntity);
        //     Assert.Equal("Fail",resultWithInvalidEmailAddress.ResponseStatus.ToString());
        //     Assert.Equal("please enter a valid email address",resultWithInvalidEmailAddress.ValidationErrors[0].ToLower());
        // }

        [Fact]
        public void Should_Fail_Loan_Application_Creation_When_Required_Field_Is_Missing()
        {             
            var loanApplicationParam = new Mock<LoanApplicationRequest>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            //mock user session
            userSessionEntity.UserID = 3;

            FakeLoanApplicationData fakeLoanApplicationData = new FakeLoanApplicationData();
            
            loanApplicationParam = fakeLoanApplicationData.GetFakeLoanApplicationParam();

            //make required field as empty
             
            loanApplicationParam.LoanBusinessDetails.BankAccountNumber = "0";

            var resultWithMissingFields = mockApplicationService.SaveLoanApplication(loanApplicationParam,userSessionEntity);
            Assert.Equal("Fail",resultWithMissingFields.ResponseStatus.ToString());
            Assert.Equal("please enter bank account number",resultWithMissingFields.ValidationErrors[0].ToLower());
           
        }

        //To get the initial commands 
         [Fact]
        public void GetInitialWorkFlowCommands()
        {
            var mockLogger = new Mock<ILogger<ApplicationServiceImpl>>();
            
            long applicationID = 0;
            
             
            var userSessionEntity = new Mock<UserSessionEntity>().Object;
            userSessionEntity.UserID = 6;
            userSessionEntity.IdentityID = new Guid("0e9c6abc-cbb5-459d-86b1-76bc9f3f66b8");
            var result = mockApplicationService.GetWorkFlowCommands(applicationID,userSessionEntity);
            Assert.Equal("True", result.IsSuccess.ToString());
        }
         //To get the initial commands 
         [Fact]
        public void GetNextWorkFlowCommands()
        {
            var mockLogger = new Mock<ILogger<ApplicationServiceImpl>>();
             
            long applicationID = 4;
            
             
            var userSessionEntity = new Mock<UserSessionEntity>().Object;
            userSessionEntity.IdentityID = new Guid("28645462-c613-467e-af94-93cbf1e284df");
            var result = mockApplicationService.GetWorkFlowCommands(applicationID,userSessionEntity);
            Assert.Equal("True", result.IsSuccess.ToString());
        }

        [Fact]
        public void ApplicationCommandHandlerForNewApplicationWithValidData()
        {
            var mockLogger = new Mock<ILogger<ApplicationServiceImpl>>();
            LoanApplicationRequest loanApplicationParam = new LoanApplicationRequest();
           
            loanApplicationParam.CommandName = "SaveAsDraft";
             
            var rolePermissionRepository = new RolePermissionRepositoryImpl(context);
            var rUserRoleRepository = new RUserRoleRepositoryImpl(context, null);
            var roleRepository = new RoleRepositoryImpl(context);
            var userRepository = new UserRepositoryImpl(context, null);
            var listRole = new ListRoleImpl(rolePermissionRepository,rUserRoleRepository,roleRepository,null);

            DependencyHelper.WorkflowActions = new WorkflowActions();

            DependencyHelper.WorkflowRole = new WorkflowRole(listRole, userRepository);
            var userSessionEntity = new Mock<UserSessionEntity>().Object;
            userSessionEntity.IdentityID = new Guid("28645462-c613-467e-af94-93cbf1e284df");
            var result = mockApplicationService.ApplicationCommandHandler(loanApplicationParam, userSessionEntity);
            Assert.Equal("True", result.IsSuccess.ToString());
        }

        [Fact]
        public void ApplicationCommandHandlerForSubmittedApplicationWithValidData()
        {
            var mockLogger = new Mock<ILogger<ApplicationServiceImpl>>();
            LoanApplicationRequest loanApplicationParam = new LoanApplicationRequest();
            loanApplicationParam.LoanApplicationID= 7;
            
            loanApplicationParam.CommandName = "RequestConditionalApproval"; 
        
            var rolePermissionRepository = new RolePermissionRepositoryImpl(context);
            var rUserRoleRepository = new RUserRoleRepositoryImpl(context, null);
            var roleRepository = new RoleRepositoryImpl(context);
            var userRepository = new UserRepositoryImpl(context, null);
            var listRole = new ListRoleImpl(rolePermissionRepository,rUserRoleRepository,roleRepository,null);

            DependencyHelper.WorkflowActions = new WorkflowActions();

            DependencyHelper.WorkflowRole = new WorkflowRole(listRole, userRepository);
            var userSessionEntity = new Mock<UserSessionEntity>().Object;
            userSessionEntity.IdentityID = new Guid("28645462-c613-467e-af94-93cbf1e284df");
            var result = mockApplicationService.ApplicationCommandHandler(loanApplicationParam, userSessionEntity);
            Assert.Equal("True", result.IsSuccess.ToString());
        }
        #endregion Methods 
    }
}