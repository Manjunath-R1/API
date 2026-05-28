using System;
using System.Collections.Generic;
using System.Text;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.Domain.Master;
using ThoughtFocus.Domain.Params;

namespace ThoughtFocus.Domain.Response
{
    public class BusinessProfileMasterDataResponse : BaseResponse
    {

        public BusinessProfileMasterDataResponse()
        {
            BusinessOwnerMasterParam = new List<BusinessOwnerMasterParam>();
            LoanBusinessDetailMasterPreParam = new LoanBusinessDetailMasterPreParam();
        }
        public List<BusinessOwnerMasterParam> BusinessOwnerMasterParam { get; set; }
        public LoanBusinessDetailMasterPreParam LoanBusinessDetailMasterPreParam { get; set; }
        public BusinessProfileMasterDataEntity BusinessProfileMasterDataEntity { get; set; }

    }


}
