using System.Collections.Generic;

namespace ThoughtFocus.Domain.CustomView
{
    public class RoleRequestParam
    {
        public List<RoleListingViewEntity> RoleListingViewEntities { get; set; }

        public long UserID { get; set; }
    }
}
