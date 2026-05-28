using System.Collections.Generic;

namespace ThoughtFocus.Domain
{
    public class DocumentSearchRequest
    {
        public int Take { get; set; }

        public int Skip { get; set; }

        public List<KeyValueModel> DocumentFilters { get; set; }

        public List<KeyValueModel> TagFilters { get; set; }

    }
}
