namespace ThoughtFocus.Business
{
    using System;
    using System.Collections.Generic;
    using DataAccess.Models.Master;
    using ThoughtFocus.Domain.Audit;
    using ThoughtFocus.Domain.Contact;
    using ThoughtFocus.Domain.CustomView; 

    public interface IBaseBusiness
    {
        #region Methods

        // List<ContactStatusEntity> GetAfterResponseContactStatusEntity();

        // List<ContactEntity> GetAllContactEntityWhoAreNotAgency();

        // List<ContactPersonTypeEntity> GetAllContactPersonTypeEntity();

        // List<ContactStatusEntity> GetAllContactStatusEntity();

        // List<CountryEntity> GetAllCountryEntity();

        // List<EducationalFieldTypeEntity> GetAllEducationalFieldTypeEntity();

        // List<EmployerEntity> GetAllEmployerEntity();

        // List<EthnicityEntity> GetAllEthnicityEntity();

        // List<GenderEntity> GetAllGenderEntity();

        // List<OrganizationEntity> GetAllOrganizationEntity();

        // List<OrganizationTypeEntity> GetAllOrganizationTypeEntity();

        // List<PositionEntity> GetAllPositionEntity();

        // List<RoleEntity> GetAllRoleEntity();

        // List<SalutationEntity> GetAllSalutationEntity();

        // List<SchoolEntity> GetAllSchoolEntity();

        // List<InstituionEntity> GetAllInstituionEntity(UserSessionEntity userSessionEntity);

        // List<SchoolPersonTypeEntity> GetAllSchoolPersonTypeEntity();

        // List<SchoolTypeEntity> GetAllSchoolTypeEntity();

        // List<SecurityQuestionEntity> GetAllSecurityQuestionEntity();

        // List<SiteVisitAcceptanceStatusEntity> GetAllSiteVisitAcceptanceStatusEntity();

        // List<SiteVisitMemberRoleEntity> GetAllSiteVisitMemberRoleEntity();

        // List<SiteVisitSeasonEntity> GetAllSiteVisitSeasonEntity();

        // List<MemberType> GetAllContactMemberTypes();

        // List<SiteVisitStateEntity> GetAllSiteVisitStateEntity();

        // List<SiteVisitTypeEntity> GetAllSiteVisitTypeEntity();

        // List<FeedbackTypeEntity> GetAllFeedbackTypeEntity();

        // List<SiteVisitConflictOfInterestTypeEntity> GetAllSiteVisitConflictOfInterestTypeEntity();

        // List<StateEntity> GetAllStateEntity();

        // string GetMinorityStatusByEthnicityID(long ethnicityID);

        // string GetMinorityStatus(long? ethnicityID);

        // AuditTrailUIExceptionLogEntity GetAuditTrailUIExceptionLogEntity(
        //    string actionName, string persistanceLayerName, string errorCode, string errorMessage,
        //    Exception ex, UserSessionEntity userSessionEntity);

        // SiteVisitTypeEntity GetSiteVisitTypeByID(long siteVisitTypeID);

        // long GetUpcomingSiteVisitTypeBySchoolId(long SchoolID);

        // SchoolEntity GetShoolBySchoolId(long SchoolID);

        // List<ProgramApplicationTypesEntity> GetAllProgramApplicationTypeEntity();

        #endregion Methods
    }
}