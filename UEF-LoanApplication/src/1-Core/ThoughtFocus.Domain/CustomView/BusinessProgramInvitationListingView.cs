namespace ThoughtFocus.Domain.CustomView
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Contact;

    //[Serializable]
    public class BusinessProgramInvitationListingView 
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
        public long ContactID
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
        public string NotifiedOn 
        { 
            get; 
            set; 
        }
         

        #endregion Properties
    }
}