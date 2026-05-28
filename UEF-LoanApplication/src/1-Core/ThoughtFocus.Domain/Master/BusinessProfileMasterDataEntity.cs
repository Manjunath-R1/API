using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.Master
{
    public class BusinessProfileMasterDataEntity
    {
            #region Properties
            public List<StateEntity> StateList { get; set; }
            public List<GenderEntity> GenderList { get; set; }
            public List<RaceEntity> RaceList { get; set; }
            public List<EthnicityEntity> EthnicityList { get; set; }
            public List<IndustryTypeEntity> IndustryTypeList { get; set; }
            public List<BusinessTypeEntity> BusinessTypeList { get; set; }
            public List<VeteranEntity> VeteranList { get; set; }
           
            #endregion Properties
        
    }
}
