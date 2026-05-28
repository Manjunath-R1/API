using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.App.ViewModels
{
    public class ContactDataResponse : BaseResponse
    {
        public ContactViewEntity Contact { get; set; }
        public BusinessContactViewEntity BusinessContact { get; set; }
        public List<ProgramInvitationContactRole> ProgramInvitations { get; set; }
    }
     
    public class ContactResponse : BaseResponse
    {
        public List<ContactListingViewEntity> data { get; set; }
        public ContactViewEntity info {get; set;}
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }       
    }
    public class ActivationResponse : BaseResponse
    {
       public  long UserId { get; set; }
       public  long ContactId { get; set; }
       public  string TokenID { get; set; }
    }

    public class BusinessContactResponse : BaseResponse
    {
        public List<BusinessContactListingEntity> data { get; set; }
        public BusinessContactViewEntity info {get; set;}
        public int recordsTotal { get; set; }       
    }

    public class InternlContactResponse : BaseResponse
    {
        public List<ContactListingViewEntity> data { get; set; }
        public ContactListingViewEntity info {get; set;}
        public int recordsTotal { get; set; }       
    }
      public class PasswordChangeAndForgotResponse : BaseResponse
    {
       public  long UserId { get; set; }
       public  long ContactId { get; set; }
       public  string TokenID { get; set; }   
    }
 
     
}