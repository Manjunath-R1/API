using System;
using System.Collections.Generic;
using System.Text;
using ThoughtFocus.DataAccess.Models.Application;

namespace ThoughtFocus.Domain.Params
{
    public class BusinessProfileRequest
    {
        public  List<BusinessOwnerMasterParam> BusinessOwnerMasterParam { get; set; }
        public LoanBusinessDetailMasterParam LoanBusinessDetailMasterParam { get; set; }
    }
}
