using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Response
{
    public class ContactViewEntityResponse:BaseResponse 
    {
        public ContactViewEntity contactViewEntity { get; set; }
        public BusinessContactViewEntity businessContactViewEntity { get; set; }
        public List<ProgramInvitationContactRole> ProgramInvitations { get; set; }
    }
}
