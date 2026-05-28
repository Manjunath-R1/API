using System.Collections.Generic;

namespace ThoughtFocus.Domain.Params
{
    public class SystemTaskParam
    {
        public long ProcessID { get; set; }

        public long ProcessTypeID { get; set; }

        public long LoggedInUserID { get; set; }

        public long MemberInformationID { get; set; }

        public List<long> EventTypes { get; set; }

    }
}
