namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThoughtFocus.Domain.Contact;

    [Serializable]
    public class BusinessContactRequest
    {
        #region Properties       
        public long ContactID {get; set;}
        public long BusinessID { get; set; }
        public long BusinessRoleID { get; set; }
        
 

        
       
        #endregion Properties
    }
}