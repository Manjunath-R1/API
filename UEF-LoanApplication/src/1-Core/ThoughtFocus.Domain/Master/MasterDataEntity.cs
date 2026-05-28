using System;
using System.Collections.Generic;

namespace ThoughtFocus.Domain.Master
{
    public class MasterDataEntity
    {
        #region Properties
        public List<StateEntity> StateList {get; set;}
        public List<GenderEntity> GenderList {get; set;}
        public List<SalutationEntity> SalutationList {get; set;}
        public List<ApplicationTypeEntity> ApplicationTypeList {get; set;}
        public List<ApplicationStatusEntity> ApplicationStatusList {get; set;}
        public List<RaceEntity> RaceList {get; set;}
        public List<RoleEntity> RoleList {get; set;}
        public List<AffiliateEntity> AffiliateList {get; set;}
        public List<EthnicityEntity> EthnicityList {get; set;}
        public List<IndustryTypeEntity> IndustryTypeList {get; set;}
        public List<BusinessTypeEntity> BusinessTypeList {get; set;}
        public List<VeteranEntity> VeteranList {get; set;}
        public List<BusinessRoleEntity> BusinessRoleList {get; set;}
        public List<FundingTypeEntity> FundingTypeList {get; set;}


        #endregion Properties
    }
}