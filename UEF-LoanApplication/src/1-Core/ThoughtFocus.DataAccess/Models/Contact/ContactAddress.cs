using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.Contact
{
    [Table("ContactAddress", Schema = "Contact")]
    public partial class ContactAddress
    {
        [Key]
        public long ContactAddressID { get; set; }
        public long ContactID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Nullable<long> StateID { get; set; }
        public Nullable<long> CountryID { get; set; }
        public bool IsPrimary { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
    }
}