using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ThoughtFocus.DataAccess.Models.Admin;

namespace ThoughtFocus.Domain.Response
{
    public class BusinessEntityListByUserResponse : BaseResponse
    {
        public BusinessEntityListByUserResponse()
        {
            BusinessEntity = new List<BusinesEntityByUser>();
        }
        public List<BusinesEntityByUser> BusinessEntity;
    }

}