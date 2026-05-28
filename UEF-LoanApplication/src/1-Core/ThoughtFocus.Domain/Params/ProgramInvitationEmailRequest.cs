using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.Params
{
    [Serializable]
    public class ProgramInvitationEmailRequest
    {
        public long ProgramID { get; set; }
        public string MessageBody { get; set; }
    }
}
