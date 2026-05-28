namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThoughtFocus.Domain.Contact;

    [Serializable]
    public class ProgramInvitationRequest
    {
        #region Properties

       
        public long BusinessID { get; set; }
        public long ProgramID { get; set; }
        public long ProgramStatusID { get; set; }
        public long ContactID { get; set; }
 

        
       
        #endregion Properties
    }
}