using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ThoughtFocus.DataAccess;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.DataAccess.Models.FundingSource;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.UnitTests.DataProvider
{
    public class MockFundingSourceData 
    {
        public FundUtilization GetMockedFundUtilizationData()
        {

            FundingEntity fundingEntity = new FundingEntity();
            fundingEntity.FundingEntityID = 10;
            fundingEntity.FundingEntityName = "PepsiCo";
            fundingEntity.Address = "New York";
            fundingEntity.EIN ="CMh2877833";
            fundingEntity.TIN = "CMh28778333434";

            FundingType fundingType = new FundingType();
            fundingType.FundingTypeID = 1;
            fundingType.Type = "Loan";
            fundingType.DisplayOrder = 1;
            fundingType.Description = "Loan";
            fundingType.IsActive = true;

            //Mock data for base funding source entity
            FundingSource fundingSource = new FundingSource();
            fundingSource.FundingSourceID = 10;
            fundingSource.ProgramName = "test";
            fundingSource.FundingEntityID = 10;
            fundingSource.FundingTypeID = 1;
            fundingSource.IsActive = true;
            fundingSource.FundingEntity = new FundingEntity();
            fundingSource.FundingEntity = fundingEntity;
            fundingSource.FundingType = new FundingType();
            fundingSource.FundingType = fundingType;
            

            TransactionType transactionType = new TransactionType();
            transactionType.TransactionTypeID = 3;
            transactionType.Type = "Add";
            transactionType.DisplayOrder = 1;
            transactionType.Description = "Add amount to fund";
            transactionType.IsActive = true;
            
            

            //Mock fund transaction
            FundTransaction fundTransaction = new FundTransaction();
            fundTransaction.ID = 100;
            fundTransaction.Comment = "Added";
            fundTransaction.FundingSourceID= 10;
            fundTransaction.OriginatingBankAccount = "5675846578436";
            fundTransaction.TransactionTypeID = 3;
            fundTransaction.TransactionAmount = 80000;
            fundTransaction.IsActive = true;
            fundTransaction.CreatedDateTime = DateTime.Now;
            fundTransaction.TransactionDate = DateTime.Now;
            fundTransaction.TransactionType = new TransactionType();
            fundTransaction.TransactionType = transactionType;
            fundingSource.FundTransactions.Add(fundTransaction);

            
            

            FundUtilization fundUtilization = new FundUtilization();
            fundUtilization.ID = 10;
            fundUtilization.DateofDisbursement = DateTime.Now;
            fundUtilization.LoanApplication = new LoanApplication();
            fundUtilization.LoanApplication.LoanBusinessDetail = new LoanBusinessDetail();
            fundUtilization.LoanApplication.LoanBusinessDetail.BusinessName = "Textile Industry";
            fundUtilization.LoanApplication.LoanBusinessDetail.BusinessType = new BusinessType();
            fundUtilization.LoanApplication.LoanBusinessDetail.BusinessType.Type = "Textile";
            fundUtilization.ApplicationID = 1;
            //fundUtilization.FundingSourceID = 10;
            fundTransaction.ID = 101;
            fundUtilization.Comment = "Added";
            fundUtilization.FundingSourceID= 10;
            fundUtilization.OriginatingBankAccount = "5675846578436";
            fundUtilization.TransactionTypeID = 3;
            fundUtilization.TransactionAmount = 8000;
            fundUtilization.IsActive = true;
            fundUtilization.CreatedDateTime = DateTime.Now;
            fundUtilization.TransactionDate = DateTime.Now;
            fundUtilization.TransactionType = new TransactionType();
            fundUtilization.TransactionType = transactionType;
            // fundUtilization.FundingSource.FundingType.Type = "Loan";
            // fundUtilization.LoanApplication.LoanBusinessDetail.BusinessName = "Restaurant";
            // fundUtilization.LoanApplication.LoanBusinessDetail.BusinessType.Type = "Partnership";
            fundingSource.FundTransactions.Add(fundUtilization);


            fundUtilization.FundingSource = fundingSource;

            return fundUtilization;

        }
    }
}