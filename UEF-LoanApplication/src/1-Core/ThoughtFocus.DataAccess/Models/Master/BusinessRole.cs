using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.DataAccess.Models.User;

namespace ThoughtFocus.DataAccess.Models.Master
{
    
    [Table("BusinessRole", Schema = "Master")]
    public partial class BusinessRole
    {

        [Key]
        public long BusinessRoleID { get; set; }
        public string BusinessRoleName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
    
        
    }
}