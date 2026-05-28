using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Response
{
    public class ContactListExportResponse:BaseResponse 
    {
        public List<ContactListingViewEntity> contactListResponse { get; set; }

        public List<BusinessContactListingEntity> businessContactListResponse { get; set; }
    }
}
