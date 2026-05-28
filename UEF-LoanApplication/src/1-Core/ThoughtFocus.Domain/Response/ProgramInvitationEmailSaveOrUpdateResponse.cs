using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.Response
{
    public class ProgramInvitationEmailSaveOrUpdateResponse : BaseResponse
    {
        public ProgramInvitationEmailSaveOrUpdate ProgramInvitationEmailSaveOrUpdate { get; set; }
    }

    public class ProgramInvitationEmailSaveOrUpdate
    {
        public long ProgramID { get; set; }
        public string MessageBody { get; set; }
    }
}
