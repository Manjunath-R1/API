using FluentValidation;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Domain.Params;
using System.Linq;
using System;
using System.Collections.Generic;

namespace ThoughtFocus.Validations.InputParameterValidation.Application
{
    public class LoanApplicationParamValidation : AbstractValidator<LoanApplicationRequest>
    {
        #region Constructors
        public LoanApplicationParamValidation()
        {
            RuleSet("requiredInputValidation", () =>
              {
                  RuleFor(x => x.ProgramInvitationID).NotEmpty().WithMessage("Please enter program invitation id");

                  //Loan Business Details

                  RuleFor(x => x.LoanBusinessDetails.DBA).NotEmpty().WithMessage("Please enter Doing Business as");
                  RuleFor(x => x.LoanBusinessDetails.Address).NotEmpty().WithMessage("Please enter Business Address");
                  RuleFor(x => x.LoanBusinessDetails.Zip).NotEmpty().WithMessage("Please enter Zip code");
                  RuleFor(x => x.LoanBusinessDetails.City).NotEmpty().WithMessage("Please enter city");
                  RuleFor(x => x.LoanBusinessDetails.StateID).NotEmpty().WithMessage("Please Select State");
                  RuleFor(x => x.LoanBusinessDetails.EmailAddress).NotEmpty().WithMessage("Please enter Email Address");
                  RuleFor(x => x.LoanBusinessDetails.PhoneNumber).NotEmpty().WithMessage("Please enter phone number");
                  RuleFor(x => x.LoanBusinessDetails.BusinessTypeID).NotEmpty().WithMessage("Please Select Business Type");
                  RuleFor(x => x.LoanBusinessDetails.IndustryTypeID).NotEmpty().WithMessage("Please Select Industry Type");
                  RuleFor(x => x.LoanBusinessDetails.StartDate).NotEmpty().WithMessage("Please enter Business StartDate");

                  RuleFor(x => x.LoanBusinessDetails.NumberOfYearsInBusiness).NotEmpty()
                  .WithMessage("Please enter Number Of Years In Business");

                  RuleFor(x => x.LoanBusinessDetails.EmployeeStrength).NotEmpty().WithMessage("Please enter Number Of Employees");


                  RuleFor(x => x.LoanBusinessDetails.BankAccountNumber).NotEmpty().WithMessage("Please enter Bank Account Number");


                  RuleFor(x => x.LoanBusinessDetails.BankRoutingNumber).NotEmpty().WithMessage("Please enter Bank Routing Number");

                  RuleFor(x => x.LoanBusinessDetails.SIC_ID).NotEmpty().WithMessage("Please Select SIC Code");



                  //   RuleFor(x => x.LoanBusinessDetails.BusinessName).NotEmpty().WithMessage("Please enter Name of the Business applying for loan");
                  //   RuleFor(x => x.LoanBusinessDetails.Url).NotEmpty().WithMessage("Please enter URL");
                  //   RuleFor(x => x.LoanBusinessDetails.EIN).NotEmpty().WithMessage("Please enter EIN");
                  //   RuleFor(x => x.LoanBusinessDetails.DUNS).NotEmpty().WithMessage("Please enter DUNS");
                  //   RuleFor(x => x.LoanBusinessDetails.AffiliateID).NotEmpty().WithMessage("Please Select Affiliate");
                  //   RuleFor(x => x.LoanBusinessDetails.AverageMonthlyPayroll).NotEmpty().WithMessage("Please enter Average Monthly Payroll");

                  // Required field validation for funding Application
                  //RuleFor(x => x.FundingApplication.RequestedFundAmount).NotEmpty().WithMessage("Please enter funding amount requested");

                  //RuleFor(x => x.FundingApplication.RequestedFundAmount).NotEmpty().WithMessage("Please enter fund amount requested")
                  //                                   .GreaterThanOrEqualTo(1).WithMessage("Please enter valid requested fund amount");

                  RuleFor(x => x.FundingApplication.Purpose).NotEmpty().WithMessage("Please enter purpose of funds");


                  //Loan Business Owner Details
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.BusinessOwnerName).NotEmpty().WithMessage("Please enter Business Owner Name");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Please enter Email Address");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.OwnedPercentage).NotEmpty().WithMessage("Please enter Ownership %");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.GenderID).GreaterThanOrEqualTo(0).WithMessage("Please select Gender");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.RaceID).GreaterThanOrEqualTo(0).WithMessage("Please select Race");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.EthnicityID).GreaterThanOrEqualTo(0).WithMessage("Please select Ethnicity");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.VeteranID).GreaterThanOrEqualTo(0).WithMessage("Please select Veteran");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.Demographic).NotEmpty().WithMessage("Please enter Demographic");
                  //   });



                  //   RuleForEach(x => x.FundingApplication.QuestionResponse).ChildRules(FundingApplicationQuestions =>
                  //   {
                  //       FundingApplicationQuestions.RuleFor(x => x.Response).NotEmpty().WithMessage("Please provide answer");
                  //   });



              });

            RuleSet("invalidInputValidation", () =>
           {
               RuleFor(x => x.ProgramInvitationID).GreaterThanOrEqualTo(1).WithMessage("Please provide valid program invitation id");

               //Loan Business Details
               //    RuleFor(x => x.LoanBusinessDetails.BankAccountNumber.ToString()).Matches(@"^[0-9]+$").WithMessage("Please enter valid bank account number");
               //    RuleFor(x => x.LoanBusinessDetails.BankRoutingNumber.ToString()).Matches(@"^[0-9]+$").WithMessage("Please enter valid bank Routing number");
               //RuleFor(x => x.LoanBusinessDetails.SIC).Matches(@"^[0-9]+$").WithMessage("Please enter valid SIC code");
               //    RuleFor(x => x.LoanBusinessDetails.EmailAddress).EmailAddress().WithMessage("Please enter valid email address");
               //    RuleFor(x => x.LoanBusinessDetails.PhoneNumber).Matches(@"^[0-9]{10}$").WithMessage("Please enter a  10 digits phone number");
               //RuleFor(x => x.LoanBusinessDetails.NAICS).Matches(@"^[0-9]+$").WithMessage("Please enter valid NAICS code");
               //RuleFor(x => x.LoanBusinessDetails.Url).Must(LinkMustBeAUri).WithMessage("Please enter valid URL followed by http:// or https://");
               //    RuleFor(x => x.LoanBusinessDetails.FacebookUrl).Must(LinkMustBeAUri).WithMessage("Please enter valid URL followed by http:// or https://");

               //Loan Business Owner Details
               //    RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
               //    {
               //        BusinessOwner.RuleFor(x => x.BusinessOwnerName).Matches(@"^[a-zA-Z ]+$").WithMessage("Please enter only alphabets");
               //    });
               //    RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
               //    {
               //        BusinessOwner.RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Please enter valid email address");
               //    });



               //    RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
               //    {
               //        BusinessOwner.RuleFor(x => x.Demographic).Matches(@"^[a-zA-Z ]+$").WithMessage("Please enter only alphabets");
               //    });
               //    RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
               //    {
               //        BusinessOwner.RuleFor(x => x.OwnedPercentage).InclusiveBetween(0, 100).WithMessage("Please enter valid owned percentage");
               //    });

               // Invalid input validation for funding Application
               // RuleFor(x => x.FundingApplication.RequestedFundAmount.ToString()).Must(IsDecimalFormat).WithMessage("Please valid funding amount requested");




           });
        }


        #endregion Constructors

        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }
        private bool IsDecimalFormat(string input)
        {
            Decimal dummy;
            return Decimal.TryParse(input, out dummy);
        }

        private static bool LinkMustBeAUri(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            //check if url is valid or not. 
            //Note - www will not be taken as valid uri
            return Uri.IsWellFormedUriString(link, UriKind.Absolute);

        }
    }

    public class LoanBusinessprofileParamValidation : AbstractValidator<LoanBusinessDetailParam>
    {
        #region Constructors
        public LoanBusinessprofileParamValidation()
        {
            RuleSet(RuleSetEnumeration.LoanBusinessProfileParamValidation.ToString(), () =>
              {
                  //Loan Business Details
                  RuleFor(x => x.BusinessName).NotEmpty().WithMessage("Please enter Name of the Business applying for loan");
                  RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Please enter Email Address");
                  RuleFor(x => x.DBA).NotEmpty().WithMessage("Please enter Doing Business as");
                  RuleFor(x => x.Address).NotEmpty().WithMessage("Please enter Business Address");
                  RuleFor(x => x.Zip).NotEmpty().WithMessage("Please enter Zip code");
                  RuleFor(x => x.StateID).NotEmpty().WithMessage("Please Select State");
                  RuleFor(x => x.EIN).NotEmpty().WithMessage("Please enter EIN");
                  RuleFor(x => x.DUNS).NotEmpty().WithMessage("Please enter DUNS");
                  RuleFor(x => x.AffiliateID).NotEmpty().WithMessage("Please Select Affiliate");
                  RuleFor(x => x.Url).NotEmpty().WithMessage("Please enter URL");
                  //   RuleFor(x => x.FacebookUrl).NotEmpty().WithMessage("Please enter Facebook URL");
                  //  RuleFor(x => x.SIC).NotEmpty().WithMessage("Please enter SIC code");
                  //   RuleFor(x => x.NAICS).NotEmpty().WithMessage("Please enter NAICS code");
                  RuleFor(x => x.BusinessTypeID).NotEmpty().WithMessage("Please Select Business Type");
                  RuleFor(x => x.IndustryTypeID).NotEmpty().WithMessage("Please Select Industry Type");
                  RuleFor(x => x.StartDate).NotEmpty().WithMessage("Please enter Business StartDate");
                  RuleFor(x => x.NumberOfYearsInBusiness).NotEmpty().WithMessage("Please enter Number Of Years In Business");
                  RuleFor(x => x.EmployeeStrength).NotEmpty().WithMessage("Please enter Number Of Employees");
                  RuleFor(x => x.AverageMonthlyPayroll).NotEmpty().WithMessage("Please enter Average Monthly Payroll");
                  RuleFor(x => x.BankAccountNumber).NotEmpty().WithMessage("Please enter Bank Account Number");
                  RuleFor(x => x.BankRoutingNumber).NotEmpty().WithMessage("Please enter Bank Routing Number");

                  // RuleFor(x => x.SIC).Matches(@"^[0-9]+$").WithMessage("Please enter valid SIC code");
                  //  RuleFor(x => x.NAICS).Matches(@"^[0-9]+$").WithMessage("Please enter valid NAICS code");
                  RuleFor(x => x.Url).Must(LinkMustBeAUri).WithMessage("Please enter valid URL followed by http:// or https://");
                  //   RuleFor(x => x.FacebookUrl).Must(LinkMustBeAUri).WithMessage("Please enter valid URL followed by http:// or https://");

                  RuleFor(x => x.BankAccountNumber.ToString()).Matches(@"^[0-9]+$").WithMessage("Please enter valid bank account number");
                  RuleFor(x => x.BankRoutingNumber.ToString()).Matches(@"^[0-9]+$").WithMessage("Please enter valid bank Routing number");


                  //Loan Business Owner Details
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.BusinessOwnerName).NotEmpty().WithMessage("Please enter Business Owner Name");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.OwnedPercentage).NotEmpty().WithMessage("Please enter Ownership %");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.GenderID).GreaterThanOrEqualTo(0).WithMessage("Please select Gender");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.RaceID).GreaterThanOrEqualTo(0).WithMessage("Please select Race");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.EthnicityID).GreaterThanOrEqualTo(0).WithMessage("Please select Ethnicity");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.VeteranID).GreaterThanOrEqualTo(0).WithMessage("Please select Veteran");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.Demographic).NotEmpty().WithMessage("Please enter Demographic");
                  //   });

                  //   //Loan Business Owner Details
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.BusinessOwnerName).Matches(@"^[a-zA-Z ]+$").WithMessage("Please enter only alphabets");
                  //   });

                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.Demographic).Matches(@"^[a-zA-Z ]+$").WithMessage("Please enter only alphabets");
                  //   });
                  //   RuleForEach(x => x.BusinessOwners).ChildRules(BusinessOwner =>
                  //   {
                  //       BusinessOwner.RuleFor(x => x.OwnedPercentage).InclusiveBetween(0, 100).WithMessage("Please enter only alphabets");
                  //   });

              });
        }

        private static bool LinkMustBeAUri(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            //check if url is valid or not. 
            //Note - www will not be taken as valid uri
            return Uri.IsWellFormedUriString(link, UriKind.Absolute);

        }

        #endregion Constructors
    }

    public class FundingApplicationParamValidation : AbstractValidator<FundingApplicationParam>
    {
        #region Constructors
        public FundingApplicationParamValidation()
        {
            RuleSet(RuleSetEnumeration.FundingApplicationParamValidation.ToString(), () =>
              {
                  RuleFor(x => x.Purpose).NotEmpty().WithMessage("Please enter purpose for requested loan");
                  RuleFor(x => x.RequestedFundAmount).NotEmpty().WithMessage("Please enter fund amount requested")
                                                    .GreaterThanOrEqualTo(1).WithMessage("Please enter valid requested fund amount");
                  RuleForEach(x => x.QuestionResponse).ChildRules(QuestionResponse =>
                    {
                        QuestionResponse.RuleFor(x => x.Response).NotEmpty().WithMessage("Please enter valid response");
                    });
              });
        }
        #endregion Constructors
    }


    public class LoanApplicantprofileParamValidation : AbstractValidator<List<LoanApplicantDetailsParam>>
    {
        #region Constructors
        public LoanApplicantprofileParamValidation()
        {
            RuleSet(RuleSetEnumeration.LoanApplicantDetailsParamValidation.ToString(), () =>
            {
                //Required Field Validation
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.BusinessRoleID).NotEmpty().WithMessage("Please select Role");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter first name");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.LastName).NotEmpty().WithMessage("Please enter last name");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Please enter email address");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.CurrentAddress).NotEmpty().WithMessage("Please enter address");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.SalutationID).NotEmpty().WithMessage("Please select Title");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.City).NotEmpty().WithMessage("Please enter city");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.StateID).NotEmpty().WithMessage("Please select State");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Please enter zip-code");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.SSN).NotEmpty().WithMessage("Please enter SSN number");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Please enter Phone Number");
                });

                //Invalid Input validation
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.FirstName).Matches(@"^[a-zA-Z]+$").WithMessage("Please enter only alphabets for first name");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.LastName).Matches(@"^[a-zA-Z]+$").WithMessage("Please enter only alphabets for last name");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.MiddleName).Matches(@"^[a-zA-Z]*$").WithMessage("Please enter only alphabets for middle name");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Please enter a valid email address");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.City).Matches(@"^[a-zA-Z ]+$").WithMessage("Please enter alphabets for city");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.ZipCode).Matches(@"^[a-zA-Z0-9-]+$").WithMessage("Please enter valid zip code");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.SSN).Matches(@"^\d{9}|\d{3}-\d{2}-\d{4}$").WithMessage("Please enter valid SSN number");
                });
                RuleForEach(x => x).ChildRules(ApplicantDetail =>
                {
                    ApplicantDetail.RuleFor(x => x.PhoneNumber).Matches(@"^[0-9]{10}$").WithMessage("Please enter a  10 digits phone number");
                });

            });
        }

        #endregion Constructors
    }

}






//required field --> will be used in phase 2
//Loan Applicant Details 
//   RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//   {
//       ApplicantDetail.RuleFor(x => x.BusinessRoleID).NotEmpty().WithMessage("Please select Role");
//   });
//   RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//   {
//       ApplicantDetail.RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter first name");
//   });
//   RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//   {
//       ApplicantDetail.RuleFor(x => x.LastName).NotEmpty().WithMessage("Please enter last name");
//   });
//   RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//   {
//       ApplicantDetail.RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Please enter email address");
//   });
//   RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//   {
//       ApplicantDetail.RuleFor(x => x.CurrentAddress).NotEmpty().WithMessage("Please enter address");
//   });
//   RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//   {
//       ApplicantDetail.RuleFor(x => x.SalutationID).NotEmpty().WithMessage("Please select Title");
//   });
//   RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//   {
//       ApplicantDetail.RuleFor(x => x.City).NotEmpty().WithMessage("Please enter city");
//   });
//   RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//   {
//       ApplicantDetail.RuleFor(x => x.StateID).NotEmpty().WithMessage("Please select State");
//   });
//   RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//   {
//       ApplicantDetail.RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Please enter zip-code");
//   });
//   RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//   {
//       ApplicantDetail.RuleFor(x => x.SSN).NotEmpty().WithMessage("Please enter SSN number");
//   });
//   RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//   {
//       ApplicantDetail.RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Please enter Phone Number");
//   });



//Loan Applicant Details 
//    RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//    {
//        ApplicantDetail.RuleFor(x => x.FirstName).Matches(@"^[a-zA-Z]+$").WithMessage("Please enter only alphabets for first name");
//    });
//    RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//    {
//        ApplicantDetail.RuleFor(x => x.LastName).Matches(@"^[a-zA-Z]+$").WithMessage("Please enter only alphabets for last name");
//    });
//    RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//    {
//        ApplicantDetail.RuleFor(x => x.MiddleName).Matches(@"^[a-zA-Z]*$").WithMessage("Please enter only alphabets for middle name");
//    });
//    RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//    {
//        ApplicantDetail.RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Please enter a valid email address");
//    });
//    RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//    {
//        ApplicantDetail.RuleFor(x => x.City).Matches(@"^[a-zA-Z ]+$").WithMessage("Please enter alphabets for city");
//    });
//    RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//    {
//        ApplicantDetail.RuleFor(x => x.ZipCode).Matches(@"^[a-zA-Z0-9-]+$").WithMessage("Please enter valid zip code");
//    });
//    RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//    {
//        ApplicantDetail.RuleFor(x => x.SSN).Matches(@"^\d{9}|\d{3}-\d{2}-\d{4}$").WithMessage("Please enter valid SSN number");
//    });
//    RuleForEach(x => x.LoanApplicantDetails).ChildRules(ApplicantDetail =>
//    {
//        ApplicantDetail.RuleFor(x => x.PhoneNumber).Matches(@"^[0-9]{10}$").WithMessage("Please enter a  10 digits phone number");
//    });
