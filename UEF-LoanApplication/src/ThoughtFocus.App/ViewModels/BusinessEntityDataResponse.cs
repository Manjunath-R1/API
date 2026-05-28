using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.App.ViewModels
{
    public class BusinessEntityDataResponse : BaseResponse
    {
        public BusinessViewEntity businessViewEntity { get; set; }
        public bool CanBeDeleted { get; set; }
        public bool IsPaymentSchedule { get; set; }
    }
     
    public class BusinessEntityResponse : BaseResponse
    {
        public List<BusinessEntityListingView> data { get; set; }
        public BusinessViewEntity info {get; set;}
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }       
    }

     public class ProgramInvitationViewModel : BaseResponse
    {
        public List<ProgramResponse> Programs { get; set; }
        public List<ProgramInvitationListingView> data { get; set; }

        public List<BusinessProgramInvitationListingView> programInvitations { get; set; }
         
    }
   
     
}