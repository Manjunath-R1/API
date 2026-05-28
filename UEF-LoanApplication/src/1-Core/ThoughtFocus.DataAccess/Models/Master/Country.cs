using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Contact;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("Country", Schema = "Master")]
    public partial class Country
    {
        
        [Key]
        public long CountryID { get; set; }
        public string CountryName { get; set; }
        public long CountryCallingCode { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
        
    }
}