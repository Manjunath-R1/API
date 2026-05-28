using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.DataAccess.Models.Admin
{
    public class BusinesEntityByUser
    {
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }

        public List<long> ProgramInvitationId { get; set; }
    }
}

