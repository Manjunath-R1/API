using System;
using System.Collections.Generic;
using System.Text;
using ThoughtFocus.DataAccess.Models.Contact;

namespace ThoughtFocus.Domain.Response
{
    public class ProgramInvitationContactRoleResponse : BaseResponse
    {
        public List<ProgramInvitationContactRole> ProgramInvitationContactRoles { get; set; }
    }
}
