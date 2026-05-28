namespace ThoughtFocus.Domain.CustomView
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Contact;

    //[Serializable]
    public class ProgramInvitationListingView 
    {
        #region Properties

        public long ID
        {
            get;
            set;
        }  
        public long BusinessID
        {
            get;
            set;
        }       

        public string BusinessName
        {
            get;
            set;
        }

        public string FundingEntityName
        {
            get;
            set;
        }
        public long ProgramID
        {
            get;
            set;
        }       

        public string ProgramName
        {
            get;
            set;
        }
        public string ProgramStatus
        {
            get;
            set;
        }

        public string AffiliateName
        {
            get;
            set;
        }

        public string DateInvited
        {
            get;
            set;
        }

        #endregion Properties
    }
}