using ThoughtFocus.Domain.AffiliateContact;
using ThoughtFocus.Domain.Common;


namespace ThoughtFocus.Domain.Response
{
    public class AffiliateContactListResponse:BaseResponse 
    {
        public PageResultEntity<AffiliateContactEntity> AffiliateContactPageResultEntity { get; set; }
    }
}
