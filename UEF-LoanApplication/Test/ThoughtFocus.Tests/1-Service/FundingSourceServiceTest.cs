using System;
using Xunit;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Service.Interfaces;
using Moq;
using ThoughtFocus.Service.Impl;
using ThoughtFocus.Constants;
using ThoughtFocus.Repository.Interfaces.User;
using AutoMapper;
using System.Collections.Generic;
using ThoughtFocus.Service.ModelsMapping;
using ThoughtFocus.UnitTests.DataProvider;
using ThoughtFocus.DataAccess;
using ThoughtFocus.Repository.Impl.User;
using ThoughtFocus.Repository.Interfaces.FundingSource;
using ThoughtFocus.Repository.Impl.FundingSource;
using System.Linq;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Business.Interfaces.FundSource;
using ThoughtFocus.Business.Impl.FundSource;
using ThoughtFocus.Repository.Interfaces.Master;
using ThoughtFocus.Repository.Impl.Master;
using ThoughtFocus.Repository.Interfaces.Admin;
using ThoughtFocus.Repository.Impl.Admin;
using ThoughtFocusRepository.Interfaces.Master;
using ThoughtFocus.Repository.Interfaces.Application;

namespace ThoughtFocus.UnitTests.Services
{

    public class FundingSourceServiceTest
    {
        #region Fields
        ApplicationDBContext context;
        public readonly IFundingSourceService mockFundingSourceService;
        public readonly IUserRepository userRepo;
        public readonly IFundingSourceRepository fundingSourceRepo;
        public readonly IFundingSourceService fundingSourceService;
        public readonly IFundUtilizationRepository fundUtilizationRepository;
        public readonly IFundingEntityRepository fundingEntityRepository;
        public readonly IDocumentTypeRepository documentTypeRepository;
        public readonly IHelpfulGuideTemplateRepository helpfulGuideTemplateRepository;
        private IQuestionsRepository questionsRepository;
        public IProgramInvitationRepository programInvitationRepository;
        public INotificationRepository notificationRepository;
        public readonly IFundingSource mockFundingSourceBusiness;
        public readonly IGenaralOptionRepository genaralOptionRepository;
        public readonly IFundingApplicationRepository fundingApplicationRepository;
        public readonly IFundingTypeRepository fundingTypeRepository;
        #endregion Fields

        public FundingSourceServiceTest()
        {
            var db = new FakeAppDBContext();
            context = db.CreateContextForInMemory();

            var mockUserRepoLogger = new Mock<ILogger<UserRepositoryImpl>>();
            userRepo = new UserRepositoryImpl(context, mockUserRepoLogger.Object);
            var mockFundingSourceBusinessLogger = new Mock<ILogger<FundingSource>>();

            fundingSourceRepo = new FundingSourceRepository(context);

            fundUtilizationRepository = new FundUtilizationRepository(context);

            fundingEntityRepository = new FundingEntityRepository(context);

            documentTypeRepository = new DocumentTypeRepositoryImpl(context,null);

            helpfulGuideTemplateRepository = new HelpfulGuideTemplateRepositoryImpl(context);

            questionsRepository = new QuestionsRepository(context);

            notificationRepository = new NotificationRepository(context);

            programInvitationRepository = new ProgramInvitationRepository(context);

            mockFundingSourceBusiness = new FundingSource(mockFundingSourceBusinessLogger.Object, fundingEntityRepository, null,null);


            var fundingSourceInfo = new ModelMappingConfiguration();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(fundingSourceInfo));
            IMapper mapper = new Mapper(configuration);

            var modelMapping = new Mock<ModelMappingConfiguration>();
            var mockFundingSourceServiceLogger = new Mock<ILogger<FundingSourceService>>();
            fundingSourceService = new FundingSourceService(mockFundingSourceServiceLogger.Object, fundingSourceRepo, userRepo, mockFundingSourceBusiness, mapper, fundUtilizationRepository, fundingEntityRepository, documentTypeRepository, helpfulGuideTemplateRepository, questionsRepository, notificationRepository, programInvitationRepository,null, fundingTypeRepository, fundingApplicationRepository, genaralOptionRepository,null,null,null,null,null,null);
            //fundingSourceService = new FundingSourceService(mockFundingSourceServiceLogger.Object, fundingSourceRepo, userRepo, mockFundingSourceBusiness, mapper, fundUtilizationRepository, fundingEntityRepository, documentTypeRepository, helpfulGuideTemplateRepository, questionsRepository, notificationRepository, programInvitationRepository);


        }

        [Fact]

        public void CreatingFoundingSourceWithoutErrors()
        {
            var fundingSource = new Mock<FundingSourceParam>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            var CommonResponse = new Mock<CommonResponse>(ResponseStatus.Success, MessageConstants.Success, null).Object;
            fundingSource.ProgramName = "BillGatesWellFare";
            fundingSource.FundingEntityName = "Microsoft";
            fundingSource.FundingEntityID = 1;
            fundingSource.Address = "Test";
            fundingSource.FundingTypeID = 1;
            fundingSource.IsActive = true;
            fundingSource.States = new List<long> { 1, 2 };
            fundingSource.BusinessTypes = new List<long> { 1 };

            int fsCountBeforeSave = context.FundingSources.Count();

            var result = fundingSourceService.CreateFundingSource(fundingSource, userSessionEntity);
            Assert.Equal("Success", result.ResponseStatus.ToString());
            int fsCountAterSave = context.FundingSources.Count();

            Assert.Equal((fsCountBeforeSave + 1), fsCountAterSave);

        }


        [Fact]
        public void AddingFundTransactionWithValidData()
        {
            var fundTransaction = new Mock<FundTransactionParam>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            fundTransaction.FundingSourceID = 1;
            fundTransaction.TransactionTypeID = 1;
            fundTransaction.TransactionAmount = 87488448;
            fundTransaction.Comment = "Adding the amount";
            fundTransaction.TransactionDocument.DocumentGUID = new Guid("28645462-C613-467E-AF94-93CBF1E284DF");
            fundTransaction.TransactionDocument.DocumentName = "AddFund";
            fundTransaction.OriginatingBankAccount = "786765575757773";
            fundTransaction.TransactionDocument.DocumentTypeID = 1;
            var result = fundingSourceService.AddFundTransaction(fundTransaction, userSessionEntity);
            Assert.Equal("Success", result.ResponseStatus.ToString());
        }

        [Fact]
        public void AddingFundTransactionWithNoAmount()
        {
            var fundTransaction = new Mock<FundTransactionParam>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            var CommonResponse = new Mock<CommonResponse>(ResponseStatus.Success, MessageConstants.Success, null).Object;

            fundTransaction.FundingSourceID = 1;
            fundTransaction.TransactionAmount = 0;
            fundTransaction.Comment = "Adding the amount";
            userSessionEntity.UserID = 1;
            var result = fundingSourceService.AddFundTransaction(fundTransaction, userSessionEntity);
            Assert.Equal("Fail", result.ResponseStatus.ToString());

        }

        [Fact]
        public void AddingFundTransactionWithoutBankAccountNumer()
        {
            var fundTransaction = new Mock<FundTransactionParam>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            var CommonResponse = new Mock<CommonResponse>(ResponseStatus.Success, MessageConstants.Success, null).Object;



            fundTransaction.FundingSourceID = 1;
            fundTransaction.TransactionTypeID = 1;
            fundTransaction.TransactionAmount = 87488448;
            fundTransaction.Comment = "Adding the amount";
            fundTransaction.TransactionDocument.DocumentGUID = new Guid("28645462-C613-467E-AF94-93CBF1E284DF");
            fundTransaction.TransactionDocument.DocumentName = "AddFund";
            fundTransaction.OriginatingBankAccount = "";
            userSessionEntity.UserID = 1;

            var result = fundingSourceService.AddFundTransaction(fundTransaction, userSessionEntity);
            Assert.Equal("Fail", result.ResponseStatus.ToString());

        }



        [Fact]
        public void RemoveFundTransaction()
        {

            var fundTransaction = new Mock<FundTransactionParam>().Object;
            var userSessionEntity = new Mock<UserSessionEntity>().Object;

            var CommonResponse = new Mock<CommonResponse>(ResponseStatus.Success, MessageConstants.Success, null).Object;


            //without amount-Validation error
            fundTransaction.FundingSourceID = 1;
            fundTransaction.TransactionAmount = 0;
            fundTransaction.Comment = "Removing amount";
            fundTransaction.OriginatingBankAccount = "2345643456545676";
            userSessionEntity.UserID = 1;
            var result = fundingSourceService.RemoveFundTransaction(fundTransaction, userSessionEntity);
            Assert.Equal("Please enter Amount", result.ValidationErrors[0].ToString());

            //without FundingSourceID-Validation error
            fundTransaction.FundingSourceID = 0;
            fundTransaction.TransactionAmount = 1000;
            fundTransaction.Comment = "removing the amount";
            fundTransaction.OriginatingBankAccount = "2345643456545676";
            userSessionEntity.UserID = 1;
            result = fundingSourceService.RemoveFundTransaction(fundTransaction, userSessionEntity);
            Assert.Equal("Invalid funding source id", result.ValidationErrors[0].ToString());



            //without Comment-Validation error
            fundTransaction.FundingSourceID = 1;
            fundTransaction.TransactionAmount = 10;
            fundTransaction.OriginatingBankAccount = "2345643456545676";
            fundTransaction.Comment = "";
            userSessionEntity.UserID = 1;
            result = fundingSourceService.RemoveFundTransaction(fundTransaction, userSessionEntity);
            Assert.Equal("Please enter Comments", result.ValidationErrors[0].ToString());


            //without OriginatingBankAccount-Validation error
            fundTransaction.FundingSourceID = 1;
            fundTransaction.TransactionAmount = 10;
            fundTransaction.OriginatingBankAccount = "";
            fundTransaction.Comment = "comments";
            userSessionEntity.UserID = 1;
            result = fundingSourceService.RemoveFundTransaction(fundTransaction, userSessionEntity);
            Assert.Equal("Please enter originating account number", result.ValidationErrors[0].ToString());

            //add fund
            fundTransaction.FundingSourceID = 1;
            fundTransaction.TransactionTypeID = 1;
            fundTransaction.TransactionAmount = 10000;
            fundTransaction.Comment = "Adding the amount";
            fundTransaction.TransactionDocument.DocumentGUID = new Guid("28645462-C613-467E-AF94-93CBF1E284DF");
            fundTransaction.TransactionDocument.DocumentName = "AddFund";
            fundTransaction.OriginatingBankAccount = "786765575757773";
            fundTransaction.TransactionDocument.DocumentTypeID = 1;
            result = fundingSourceService.AddFundTransaction(fundTransaction, userSessionEntity);
            Assert.Equal("Success", result.ResponseStatus.ToString());

            //without error  remove ft
            fundTransaction.FundingSourceID = 1;
            fundTransaction.TransactionTypeID = 2;
            fundTransaction.TransactionAmount = 1000;
            fundTransaction.Comment = "removing the amount";
            fundTransaction.TransactionDocument.DocumentGUID = new Guid("28645462-C613-467E-AF94-93CBF1E284DF");
            fundTransaction.OriginatingBankAccount = "2345643456545676";
            fundTransaction.TransactionDocument.DocumentName = "RemoveFund";
            fundTransaction.TransactionDocument.DocumentTypeID = 1;
            //userSessionEntity.UserID = 1;
            result = fundingSourceService.RemoveFundTransaction(fundTransaction, userSessionEntity);
            Assert.Equal("Success", result.ResponseStatus.ToString());

            //Error --Amount greater than available limit
            fundTransaction.FundingSourceID = 1;
            fundTransaction.TransactionTypeID = 2;
            fundTransaction.TransactionAmount = 1000000;
            fundTransaction.Comment = "removing the amount";
            fundTransaction.TransactionDocument.DocumentGUID = new Guid("28645462-C613-467E-AF94-93CBF1E284DF");
            fundTransaction.OriginatingBankAccount = "2345643456545676";
            fundTransaction.TransactionDocument.DocumentName = "RemoveFund";
            fundTransaction.TransactionDocument.DocumentTypeID = 1;

            result = fundingSourceService.RemoveFundTransaction(fundTransaction, userSessionEntity);
            Assert.Equal("Removal amount is greater than available limit", result.ValidationErrors[0].ToString());
        }

        [Fact]
        public void ViewFundutilizationWithoutErrors()
        {
            var db = new FakeAppDBContext();
            context = db.CreateContextForInMemory();
            var fundingSourceResponse = new Mock<FundingSourceResponse>().Object;

            var result = fundingSourceService.GetFundUtilization(10);
            Assert.True(result.IsSuccess);

        }

        [Fact]
        public void ViewFundutilizationWithInvalidInput()
        {
            var db = new FakeAppDBContext();
            context = db.CreateContextForInMemory();
            var fundingSourceID = 0;

            var fundingSourceResponse = new Mock<FundingSourceResponse>().Object;

            var result = fundingSourceService.GetFundUtilization(fundingSourceID);
            Assert.False(result.IsSuccess);

        }

        [Fact]
        public void ViewFundutilizationWithNorecords()
        {
            var db = new FakeAppDBContext();
            context = db.CreateContextForInMemory();
            var fundingSourceID = 50000;

            var fundingSourceResponse = new Mock<FundingSourceResponse>().Object;

            var result = fundingSourceService.GetFundUtilization(fundingSourceID);
            Assert.False(result.IsSuccess);

        }

        [Fact]
        public void Should_Fetch_Fund_Transaction()
        {
            long FundingSourceId = 10;
            var result = fundingSourceService.GetFundTransaction(FundingSourceId);
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.FundTransactionList.Count());
        }

        [Fact]
        public void Should_Fail_To_Fetch_Fund_Transaction_On_InValid_FundingSourceId()
        {
            long FundingSourceId = 0;
            var result = fundingSourceService.GetFundTransaction(FundingSourceId);
            Assert.False(result.IsSuccess);
            Assert.True(result.IsValidationError);
            Assert.Equal("FundingSourceID", result.ValidationError.ValidationError.FirstOrDefault()?.FieldName);

            FundingSourceId = -1;
            result = fundingSourceService.GetFundTransaction(FundingSourceId);
            Assert.False(result.IsSuccess);
            Assert.True(result.IsValidationError);
            Assert.Equal("FundingSourceID", result.ValidationError.ValidationError.FirstOrDefault()?.FieldName);

        }

        [Fact]
        public void Should_Fetch_No_Fund_Transaction_When_FundingSourceId_Not_Found()
        {
            long FundingSourceId = 4;
            var result = fundingSourceService.GetFundTransaction(FundingSourceId);
            Assert.True(result.IsSuccess);
            Assert.False(result.IsValidationError);
            Assert.Empty(result.FundTransactionList);
            Assert.Equal(0, result.TotalFundedAmount);
        }

    }
}
