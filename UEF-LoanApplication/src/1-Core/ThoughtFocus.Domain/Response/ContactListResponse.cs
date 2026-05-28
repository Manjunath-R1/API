using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Response
{
    public class ContactListResponse:BaseResponse 
    {
        public PageResultEntity<ContactListingViewEntity> ContactPageResultEntity { get; set; }

        public PageResultEntity<BusinessContactListingEntity> BusinessContactResultEntity { get; set; }
    }
}
