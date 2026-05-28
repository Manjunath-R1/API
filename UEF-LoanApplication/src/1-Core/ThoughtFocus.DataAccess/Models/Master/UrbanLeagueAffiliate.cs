using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.User;
using ThoughtFocus.DataAccess.Models.Admin;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("UrbanLeagueAffiliate", Schema = "Master")]
    public partial class UrbanLeagueAffiliate
    {
        
        [Key]
        public long AffiliateID { get; set; }
        public string AffiliateName { get; set; }
        public string AffiliateAddress { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public virtual ICollection<UrbanLeagueAffiliateContact> UrbanLeagueAffiliateContacts{ get; set; }
        
    }
}