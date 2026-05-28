namespace ThoughtFocus.Domain.CustomView
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Contact;

    //[Serializable]
    public class ContactListingViewEntity
    {
        #region Properties

        public long ContactID
        {
            get;
            set;
        }       

        public string AccountStatus
        {
            get;
            set;
        }
        public int InvitationSentCount
        {
            get;
            set;
        }
        public DateTime? LastInvitaionSentDate
        {
            get;
            set;
        }

        public string MemberStatus
        {
            get;
            set;
        }

        public string CountryCallingCode
        {
            get;
            set;
        }

        public string EmailAddress
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }


        public string PhoneNumber
        {
            get;
            set;
        }

        public string OrganizationName
        {
            get;
            set;
        }

        public long OrganizationTypeID
        {
            get;
            set;
        }

        public long UserID
        {
            get;
            set;
        }
        public DateTime? MostRecentSiteVisitDate
        {
            get;
            set;
        }
        public string MostRecentSiteVisitYear
        {
            get;
            set;
        }

        public string MemberRole
        {
            get;
            set;
        }
        public string Ethnicity
        {
            get;
            set;
        }
        public string Role
        {
            get;
            set;
        }

        public string Active
        {
            get;
            set;
        }
        public string BusinessName
        {
            get;
            set;
        }

        #endregion Properties
    }
}