using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.Admin
{
    [Table("UrbanLeagueAffiliateContacts", Schema = "Admin")]
    public partial class UrbanLeagueAffiliateContact : AuditBase

    {
        [Key]
        public long AffiliateContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        
        [ForeignKey("Affiliate")]
         public long AffiliateID { get; set; }
        public virtual UrbanLeagueAffiliate Affiliate { get; set; }

    }
}