using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Contact;
using System.Collections.Generic;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("Salutation", Schema = "Master")]
    public partial class Salutation
    {
        // public Salutation()
        // {
        //     this.Contacts = new List<ThoughtFocus.DataAccess.Models.Contact.Contact>();
        // }
        [Key]
        public long SalutationID { get; set; }
        public string SalutationName { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> DisplayOrder { get; set; }
        //public virtual ICollection<ThoughtFocus.DataAccess.Models.Contact.Contact> Contacts { get; set; }
    }
}