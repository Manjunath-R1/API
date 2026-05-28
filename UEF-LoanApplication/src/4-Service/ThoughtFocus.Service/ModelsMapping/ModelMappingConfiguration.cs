using System;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.DataAccess.Models.Admin;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.CustomView;
using AutoMapper;
using System.Linq;
using ThoughtFocus.DataAccess.Models.User;
using ThoughtFocus.Domain.User;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.DataAccess.Models.FundingSource;
using System.Collections.Generic;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.Domain.Master;
using ThoughtFocus.DataAccess.Models;

namespace ThoughtFocus.Service.ModelsMapping
{
    public class ModelMappingConfiguration : Profile
    {
        public ModelMappingConfiguration()
        {
            
		    base.CreateMap<ContactRequest, Contact>();
			// .ForMember(dest => dest.ContactID, opt => opt.Ignore())
			// .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            // .ForMember(dest => dest.CreatedByUserID, opt => opt.Ignore())
            // .ForMember(dest => dest.LastModifiedByUserID, opt => opt.Ignore())
            // .ForMember(dest => dest.AccountStatus, opt => opt.Ignore())
            // .ForMember(dest => dest.Salutation, opt => opt.Ignore())
            // .ForMember(dest => dest.Users, opt => opt.Ignore())
            // .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            // .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore());

                 

            base.CreateMap<ContactViewEntity, Contact>()
            .ForMember(dest => dest.CreatedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.Users, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore())
            .ReverseMap();

            base.CreateMap<BusinessContactViewEntity, BusinessUser>()
            .ForMember(dest => dest.CreatedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore())
            .ReverseMap();



            base.CreateMap<User, UserSessionEntity>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Contact.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Contact.LastName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.Contact.MiddleName));


            base.CreateMap<ContactInvitationParam, Contact>()
            .ForMember(dest => dest.CreatedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.AccountStatus, opt => opt.Ignore())
            .ForMember(dest => dest.Salutation, opt => opt.Ignore())
            .ReverseMap();

            //Loan Application Tables Mapping
            base.CreateMap<LoanApplicationRequest, LoanApplication>()
                .ForMember(dest => dest.ConcentAcceptedDate, opt => opt.Ignore())
                .ForMember(dest => dest.LoanNumber, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByUserID, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedByUserID, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.LoanApplicantDetails, opt => opt.Ignore())
                .ForMember(dest => dest.FundingApplication, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationDocuments, opt => opt.Ignore())

                .ReverseMap();
            
            //Loan Application Tables Mapping
            base.CreateMap<FundingApplicationParam, FundingApplication>()                
                .ReverseMap();
            base.CreateMap<QuestionResponseForFunding, QuestionResponse>()
                .ForMember(dest => dest.Question, opt => opt.Ignore())
                .ForMember(dest => dest.LoanApplicationID, opt => opt.Ignore())
                .ReverseMap();

            base.CreateMap<LoanBusinessDetailParam, LoanBusinessDetail>().ReverseMap();

            base.CreateMap<BusinessOwnerParam, BusinessOwner>()
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ReverseMap();
            //Mapping for master business profile
            base.CreateMap<LoanBusinessDetailMasterParam, LoanBusinessDetailMaster>().ReverseMap();
            base.CreateMap<BusinessOwnerMasterParam, BusinessOwnerMaster>()
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ReverseMap();


            base.CreateMap<BusinessOwner, BusinessOwnerMasterParam>()
           .ForMember(dest => dest.BusinessID,opt=>opt.Ignore())
           .ReverseMap();

            base.CreateMap<LoanBusinessDetail, LoanBusinessDetailMasterParam>()
           .ForMember(dest => dest.BusinessID, opt => opt.Ignore())
           .ReverseMap();


            base.CreateMap<BusinessOwnerParam, BusinessOwnerMaster>()
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest=>dest.BusinessID,opt=>opt.Ignore())
            .ReverseMap();

            base.CreateMap<BusinessOwnerMaster, BusinessOwner>()
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ReverseMap();

            base.CreateMap<LoanBusinessDetailMaster, LoanBusinessDetail>()
            .ReverseMap();

            base.CreateMap<LoanBusinessDetailParam, LoanBusinessDetailMaster>()
            .ForMember(dest => dest.BusinessID, opt => opt.Ignore())
            .ReverseMap();

            //end
            base.CreateMap<FundingSourceParam, FundingSource>()
            .ForMember(dest => dest.CreatedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ReverseMap();


            base.CreateMap<DocumentRequest, ApplicationDocument>().ReverseMap();


            base.CreateMap<FundUtilizationParam, FundUtilization>()
            .ForMember(dest => dest.CreatedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.ID, opt => opt.Ignore())
            .ReverseMap();



            base.CreateMap<FundingEntityRequest, FundingEntity>()
            .ReverseMap();

            base.CreateMap<FundTransactionParam, FundTransaction>()
          .ForMember(dest => dest.CreatedByUserID, opt => opt.Ignore())
          .ForMember(dest => dest.CreatedDateTime, member => member.MapFrom(a => System.DateTime.Now.ToString()))
          .ForMember(dest => dest.LastModifiedByUserID, opt => opt.Ignore())
          .ForMember(dest => dest.CreatedDateTime, member => member.MapFrom(a => System.DateTime.Now.ToString()))
          .ForMember(dest => dest.IsActive, opt => opt.Ignore())
          .ForMember(dest => dest.IsActive, opt => opt.MapFrom(v => true))
          .ReverseMap();

            base.CreateMap<FundTransactionParam, FundTransactionDocument>()
          .ForMember(dest => dest.CreatedByUserID, opt => opt.Ignore())
          .ForMember(dest => dest.CreatedDateTime, member => member.MapFrom(a => System.DateTime.Now.ToString()))
          .ForMember(dest => dest.LastModifiedByUserID, opt => opt.Ignore())
          .ForMember(dest => dest.CreatedDateTime, member => member.MapFrom(a => System.DateTime.Now.ToString()))
          .ForMember(dest => dest.IsActive, opt => opt.Ignore())
          .ForMember(dest => dest.IsActive, opt => opt.MapFrom(v => true))
          .ReverseMap();



            base.CreateMap<FundTransaction, FundTransactionDetail>()
             .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.Type))
             .ForMember(dest => dest.FundTransactionDocumentID, opt =>
              opt.MapFrom(src => src.FundTransactionDocuments.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault().ID));

            base.CreateMap<LoanApplicantDetailsParam, LoanApplicantDetails>().ReverseMap();

            // Master data entity model mapping
            base.CreateMap<UrbanLeagueAffiliate, AffiliateEntity>();
            base.CreateMap<Veteran, VeteranEntity>();
            base.CreateMap<Race, RaceEntity>();
            base.CreateMap<BusinessType, BusinessTypeEntity>();
            base.CreateMap<IndustryType, IndustryTypeEntity>();
            base.CreateMap<State, StateEntity>();
            base.CreateMap<Salutation, SalutationEntity>();
            base.CreateMap<Gender, GenderEntity>();
            base.CreateMap<Ethnicity, EthnicityEntity>();
            base.CreateMap<ApplicationStatus, ApplicationStatusEntity>();
            base.CreateMap<ApplicationType, ApplicationTypeEntity>();
            base.CreateMap<Role, RoleEntity>();
            base.CreateMap<BusinessRole, BusinessRoleEntity>();
            base.CreateMap<FundingType, FundingTypeEntity>();

            base.CreateMap<User, UserEntity>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Contact.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Contact.LastName));




            base.CreateMap<BusinessEntityRequest, BusinessEntity>()
            .ForMember(dest => dest.CreatedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.NAICS, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeeStrength, opt => opt.Ignore())
            .ForMember(dest => dest.NumberOfYearsInBusiness, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
            .ForMember(dest => dest.BankRoutingNumber, opt => opt.Ignore())
            .ForMember(dest => dest.BankAccountNumber, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.StartDate, opt => opt.Ignore())
            .ForMember(dest => dest.Url, opt => opt.Ignore())
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ForMember(dest => dest.EmailAddress, opt => opt.Ignore())

            .ForMember(dest => dest.IndustryTypeID, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore())
            .ReverseMap();

            base.CreateMap<BusinessViewEntity, BusinessEntity>()
            .ForMember(dest => dest.CreatedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.NAICS, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeeStrength, opt => opt.Ignore())
            .ForMember(dest => dest.NumberOfYearsInBusiness, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
            .ForMember(dest => dest.BankRoutingNumber, opt => opt.Ignore())
            .ForMember(dest => dest.BankAccountNumber, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.StartDate, opt => opt.Ignore())
            .ForMember(dest => dest.Url, opt => opt.Ignore())
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ForMember(dest => dest.EmailAddress, opt => opt.Ignore())

            .ForMember(dest => dest.IndustryTypeID, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore())
            .ReverseMap();

            base.CreateMap<ProgramInvitationRequest, ProgramInvitation>()
            .ForMember(dest => dest.CreatedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedByUserID, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.InvitationSentDateTime, opt => opt.Ignore())
             .ForMember(dest => dest.BusinessEntity, opt => opt.Ignore())
              .ForMember(dest => dest.FundingSource, opt => opt.Ignore())
            .ReverseMap();

            base.CreateMap<MasterDataEntity, ApplicationCreationPreRequiredData>();

            base.CreateMap<BusinessContactRequest, BusinessUser>();

            base.CreateMap<FundingSourceParam, FundingSourceStates>();

            base.CreateMap<FundingApplicationParam, FundingApplication>().ReverseMap();
            base.CreateMap<BusinessOwnerParam, BusinessOwner>().ReverseMap();
            base.CreateMap<LoanBusinessDetailParam, LoanBusinessDetail>().ReverseMap();
            base.CreateMap<FundingSourceListResponse, FundingSource>().ReverseMap();

            base.CreateMap<BusinessOwnerParam, BusinessOwnerMasterParam>()
            .ForMember(dest=>dest.BusinessID,opt=>opt.Ignore())
            .ReverseMap();

            base.CreateMap<LoanBusinessDetailParam, LoanBusinessDetailMasterParam>()
            .ForMember(dest => dest.BusinessID, opt => opt.Ignore())
            .ReverseMap();

            base.CreateMap<LoanBusinessDetail, LoanBusinessDetailMasterPreParam>()
          .ForMember(dest => dest.EmployeeStrength, opt => opt.Ignore())
          .ForMember(dest => dest.NumberOfYearsInBusiness, opt => opt.Ignore())
          .ForMember(dest => dest.StartDate, opt => opt.Ignore())
          .ReverseMap();

           base.CreateMap<LoanBusinessDetailMaster, LoanBusinessDetailMasterPreParam>()
          .ForMember(dest => dest.EmployeeStrength, opt => opt.Ignore())
          .ForMember(dest => dest.NumberOfYearsInBusiness, opt => opt.Ignore())
          .ForMember(dest => dest.StartDate, opt => opt.Ignore())
          .ReverseMap();
        }

    }
}
