using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.Response
{
    public class AllProgramInvitationResponse : BaseResponse
    {
        public List<ProgramResponse> Programs { get; set; }
    }
}
