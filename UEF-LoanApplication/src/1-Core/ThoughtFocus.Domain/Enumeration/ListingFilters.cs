namespace ThoughtFocus.Domain.Enumeration
{
    public enum SiteVisitListFilters
    {
        /// <summary>
        /// For SchoolName
        /// </summary>
        SchoolName = 0,
        /// <summary>
        /// For SiteVisitType
        /// </summary>
        SiteVisitType = 1,

        /// <summary>
        /// For SiteVisitDateTimeFrom
        /// </summary>
        SiteVisitDateTimeFrom = 2,

        /// <summary>
        /// For SiteVisitDateTimeTo
        /// </summary>
        SiteVisitDateTimeTo = 3,


        /// <summary>
        /// For SiteVisitStatus
        /// </summary>
        SiteVisitStatus = 4

    }

    public enum ContactListFilters
    {
        /// <summary>
        /// For FirstName
        /// </summary>
        FirstName = 0,
        /// <summary>
        /// For LastName
        /// </summary>
        LastName = 1,

        /// <summary>
        /// For SchoolName
        /// </summary>
        SchoolName = 2,

        /// <summary>
        /// For EmailAddress
        /// </summary>
        EmailAddress = 3,


        /// <summary>
        /// For PhoneNumber
        /// </summary>
        PhoneNumber = 4,



        ///// <summary>
        ///// For MemberStatus
        ///// </summary>
        //MemberStatus = 5,

        /// <summary>
        /// For AccountStatus
        /// </summary>
        AccountStatus = 5,

        ///// <summary>
        ///// For SiteVisitMemberRole
        ///// </summary>
        //SiteVisitMemberRole = 7,

        ///// <summary>
        ///// For MostRecentSiteVisitDate
        ///// </summary>
        ////MostRecentSiteVisitDate = 8

    }

    public enum OrganizationListingFilters
    {
        /// <summary>
        /// For OrganizationName
        /// </summary>
        OrganizationName = 0,
        /// <summary>
        /// For OrganizationTypeName
        /// </summary>
        OrganizationTypeName = 1,

        /// <summary>
        /// For PhoneNumber
        /// </summary>
        OrganizationNumber = 2,



    }
 
    public enum SchoolListFilters
    {
        /// <summary>
        /// For SchoolName
        /// </summary>
        SchoolName = 0,

        /// <summary>
        /// For DeanName
        /// </summary>
        DeanName = 1,

        /// <summary>
        /// For PresidentName
        /// </summary>
        PresidentName = 2,

        /// <summary>
        /// For Number
        /// </summary>
        Number = 3,


        /// <summary>
        /// For City
        /// </summary>
        City = 4,

    }
    public enum SurveyListFilters
    {
        /// <summary>
        /// For SurveyName
        /// </summary>
        SurveyName = 0,

        /// <summary>
        /// For Participant
        /// </summary>
        Participant = 1,

        /// <summary>
        /// For CreatedDateTime
        /// </summary>
        CreatedDateTime = 2,



        /// <summary>
        /// For LastModifiedDateTime
        /// </summary>
        LastModifiedDateTime = 3,

    }

    public enum ApplicationListFilters
    {
        /// <summary>
        /// For LoanNumber
        /// </summary>
        LoanNumber = 0,
        /// <summary>
        /// For DateApplied
        /// </summary>
        DateApplied = 1,

        /// <summary>
        /// For BusinessName
        /// </summary>
        BusinessName = 2,

        /// <summary>
        /// For LoanProgramName
        /// </summary>
        LoanProgramName = 3,


        /// <summary>
        /// For LoanType
        /// </summary>
        LoanType = 4,

        /// <summary>
        /// For LoanAmount
        /// </summary>
        LoanAmount = 5,

        /// <summary>
        /// For ApplicationStatus
        /// </summary>
        ApplicationStatus = 6,

    }

    public enum FundingEntityListFilters
    {
        /// <summary>
        /// For FundingEntityID
        /// </summary>
        FundingEntityID = 0,

        /// <summary>
        /// For FundingEntityName
        /// </summary>
        FundingEntityName = 1,

        /// <summary>
        /// For City
        /// </summary>
        City = 2,

        /// <summary>
        /// For State
        /// </summary>
        State = 3

    }
}
