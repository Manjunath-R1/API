using System.Collections.Generic;

namespace ThoughtFocus.Domain.CustomView
{
    public class EmailTemplatePlaceholdersViewModel
    {
        public string Role { get; set; }

         
        #region User Management

        public string RecipientFullName { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string RecipientEmailAddress { get; set; }
        public string RecipientPosition { get; set; }
        public string RecipientSalutation { get; set; }

        #endregion User Management
 
        #region Email Recipient

        public string Administrator
        {
            get;
            set;
        }
 
        public string Contact
        {
            get;
            set;  
        }

        public string Borrower
        {
            get;
            set;
        }

        public string NULTreasury
        {
            get;
            set;
        }

        public string Controller
        {
            get;
            set;
        }

        public string UnderWriter
        {
            get;
            set;  
        }
        public string LoanProcessor
        {
            get;
            set;  
        }
        public string Affiliate { get; set; }

         public string LoanNumber { get; set; }

          public string memberList { get; set; }

          public List<string> memberInfoList { get; set; }
 
       
        #endregion Email Recipient

        #region Email Signature

        public string AdminFullName { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public string EmailAddress { get; set; }
        public string BusinessName { get; set; }
        public string Disbursement { get; set; }
        #endregion Email Signature

        #region Email Footer

        public string CurrentYear { get; set; }

        #endregion Email Footer

        #region URL

        public string LoginLink { get; set; }
        public string CallBackURL { get; set; }
        public string AdditionalMessage { get; set; }

        public string TimeFrame { get; set; }

        public string NextStatus { get; set; }
        public string CurrentStatus { get; set; }

        public decimal RequestedFundAmount { get; set; }

        #endregion URL
      
        
    }
}
