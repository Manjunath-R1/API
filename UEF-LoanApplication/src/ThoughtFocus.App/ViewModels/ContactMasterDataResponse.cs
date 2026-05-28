using System.Collections.Generic;
using ThoughtFocus.Domain.Master;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.App.ViewModels
{
    public class ContactMasterDataResponse : BaseResponse
    {
        public List<SalutationEntity> Salutations { get; set; }

        public List<GenderEntity> Genders { get; set; }

        public List<StateEntity> States { get; set; }

        public List<CountryEntity> Countries { get; set; }

        public List<EthnicityEntity> Ethnicity { get; set; }

        public List<ContactStatusEntity> ContactStatuses { get; set; }

       
      
        //public List<MemberType> MemberTypes { get; set; }
    }
}
