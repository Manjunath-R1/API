using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.Params
{
    public class ExportProgramInvitationsRequest: ProgramInvitationsRequest
    {  
        public List<FilterParameter> FilterParameters { get; set; }
    }
    public class ProgramInvitationsRequest
    {
        public List<long?> ProgramID { get; set; }
        
    }
}
