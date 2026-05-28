using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ThoughtFocus.DataAccess;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Domain.Params;

namespace ThoughtFocus.UnitTests.DataProvider
{
    public class FakeLoanApplicationData 
    {
        public LoanApplicationRequest GetFakeLoanApplicationParam()
        {
            LoanApplicationRequest loanApplicationParam = new LoanApplicationRequest();
            //Mock LoanApplicationParam with static values
            loanApplicationParam.ProgramInvitationID = 1;

            loanApplicationParam.LoanBusinessDetails = new LoanBusinessDetailParam();
            loanApplicationParam.BusinessOwners = new List<BusinessOwnerParam>();
            // loanApplicationParam.LoanApplicantDetails = new List<LoanApplicantDetailsParam>();

            loanApplicationParam.LoanBusinessDetails.BusinessName = "BusinessNameDemo";
            loanApplicationParam.LoanBusinessDetails.EmailAddress = "business1@demo.com";
            loanApplicationParam.LoanBusinessDetails.PhoneNumber = "3456234567";
            loanApplicationParam.LoanBusinessDetails.BusinessTypeID = 1;
            loanApplicationParam.LoanBusinessDetails.EIN = "EIN Number";
            loanApplicationParam.LoanBusinessDetails.DBA = "DBA Demo";
            loanApplicationParam.LoanBusinessDetails.StartDate = DateTime.Now.ToShortDateString();
            loanApplicationParam.LoanBusinessDetails.IndustryTypeID = 1;
            loanApplicationParam.LoanBusinessDetails.EmployeeStrength = 59851;
            loanApplicationParam.LoanBusinessDetails.NumberOfYearsInBusiness = 60;
            loanApplicationParam.LoanBusinessDetails.AverageMonthlyPayroll = 940000;
            loanApplicationParam.LoanBusinessDetails.DUNS = "DUNS Test";
            loanApplicationParam.LoanBusinessDetails.AffiliateID = 1;
            loanApplicationParam.LoanBusinessDetails.Url = "https://businessxyz.com";
            // loanApplicationParam.LoanBusinessDetails.FacebookUrl = "https://facebook.com/businessxyz";
            loanApplicationParam.LoanBusinessDetails.SIC_ID = 1;
            loanApplicationParam.LoanBusinessDetails.NAICS_ID = 1;
            loanApplicationParam.LoanBusinessDetails.Address = "Times Square USA";
            loanApplicationParam.LoanBusinessDetails.City = "Times Square";
            loanApplicationParam.LoanBusinessDetails.StateID = 3;
            loanApplicationParam.LoanBusinessDetails.Zip = "100023";
            loanApplicationParam.LoanBusinessDetails.BankAccountNumber = "00078341222";
            loanApplicationParam.LoanBusinessDetails.BankRoutingNumber = "99900";

            BusinessOwnerParam businessOwnerParam1 = new BusinessOwnerParam();
            BusinessOwnerParam businessOwnerParam2 = new BusinessOwnerParam();
            businessOwnerParam1.BusinessOwnerName = "Smith";
            businessOwnerParam1.Demographic = "Business owner demographich test";
            businessOwnerParam1.EthnicityID = 1;
            businessOwnerParam1.GenderID = 1;
            businessOwnerParam1.EmailAddress = "smith@demo.com";
            businessOwnerParam1.RaceID = 1;
            businessOwnerParam1.VeteranID = 1;
            businessOwnerParam1.OwnedPercentage = 67;
            businessOwnerParam2.BusinessOwnerName = "John";
            businessOwnerParam2.Demographic = "Business owner demographich test";
            businessOwnerParam2.EthnicityID = 1;
            businessOwnerParam2.EmailAddress = "john@demo.com";
            businessOwnerParam2.GenderID = 1;

            businessOwnerParam2.RaceID = 1;
            businessOwnerParam2.VeteranID = 1;
            businessOwnerParam2.OwnedPercentage = 67;
            loanApplicationParam.BusinessOwners.Add(businessOwnerParam1);
            loanApplicationParam.BusinessOwners.Add(businessOwnerParam2);

            loanApplicationParam.FundingApplication = new FundingApplicationParam();
            // loanApplicationParam.FundingApplication.LoanProgram = "CovidReliefProgram";
            loanApplicationParam.FundingApplication.Purpose = "Need to supply covid vaccine";
            loanApplicationParam.FundingApplication.RequestedFundAmount = 50000;
            loanApplicationParam.FundingApplication.QuestionResponse = new List<QuestionResponseForFunding>();
            QuestionResponseForFunding Question = new QuestionResponseForFunding();
            Question.QuestionID = 1;
            Question.Response = "true";
            loanApplicationParam.FundingApplication.QuestionResponse.Add(Question);

            // //Add for loanapplicatiinsdetails param 
            // LoanApplicantDetailsParam loanApplicantDetailsParam1 = new LoanApplicantDetailsParam();
            // LoanApplicantDetailsParam loanApplicantDetailsParam2 = new LoanApplicantDetailsParam();
            // loanApplicantDetailsParam1.BusinessRoleID = 1;
            // loanApplicantDetailsParam1.City = "California";
            // loanApplicantDetailsParam1.ContactID = 3;
            // loanApplicantDetailsParam1.CurrentAddress = "Street 41 California";
            // loanApplicantDetailsParam1.EmailAddress = "applicant1@uef.com";
            // loanApplicantDetailsParam1.FirstName = "Demo";
            // loanApplicantDetailsParam1.MiddleName = "dm";
            // loanApplicantDetailsParam1.LastName = "Test";
            // loanApplicantDetailsParam1.PhoneNumber = "9162456798";
            // loanApplicantDetailsParam1.SalutationID = 2;
            // loanApplicantDetailsParam1.SSN = "856-45-6789";
            // loanApplicantDetailsParam1.StateID = 6;
            // loanApplicantDetailsParam1.ZipCode = "6776674";

            // loanApplicantDetailsParam2.BusinessRoleID = 2;
            // loanApplicantDetailsParam2.City = "California";
            // loanApplicantDetailsParam2.ContactID = 3;
            // loanApplicantDetailsParam2.CurrentAddress = "Street 41 California";
            // loanApplicantDetailsParam2.EmailAddress = "applicant2@uef.com";
            // loanApplicantDetailsParam2.FirstName = "John";
            // loanApplicantDetailsParam2.MiddleName = "";
            // loanApplicantDetailsParam2.LastName = "Wick";
            // loanApplicantDetailsParam2.PhoneNumber = "9166666798";
            // loanApplicantDetailsParam2.SalutationID = 2;
            // loanApplicantDetailsParam2.SSN = "856-45-6789";
            // loanApplicantDetailsParam2.StateID = 6;
            // loanApplicantDetailsParam2.ZipCode = "6776677";

            // loanApplicationParam.LoanApplicantDetails.Add(loanApplicantDetailsParam1);
            // loanApplicationParam.LoanApplicantDetails.Add(loanApplicantDetailsParam2);


            return loanApplicationParam;
        }
    }
}