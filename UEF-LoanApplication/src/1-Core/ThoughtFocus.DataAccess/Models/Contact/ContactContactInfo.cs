using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.Contact
{
    [Table("ContactContactInfo", Schema = "Contact")]
    public partial class ContactContactInfo
    {
        [Key]
        public long ContactContactInfoID { get; set; }
        public long ContactID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public string ContactType { get; set; }
        public Nullable<long> CountryID { get; set; }
        public string Number { get; set; }
        public string Extension { get; set; }
        public bool IsPrimary { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Country Country { get; set; }
    }
}