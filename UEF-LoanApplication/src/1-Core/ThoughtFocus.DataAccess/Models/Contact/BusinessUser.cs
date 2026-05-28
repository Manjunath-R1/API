using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.DataAccess.Models.Admin;

namespace ThoughtFocus.DataAccess.Models.Contact
{
    [Table("BusinessUser", Schema = "Admin")]
    public partial class BusinessUser: AuditBase
    {
        [Key]
        public long BusinessUserID { get; set; }
        [ForeignKey("BusinessEntity")]
        public long BusinessID { get; set; }
        [ForeignKey("Contacts")]
        public long ContactID { get; set; }
        [ForeignKey("BusinessRole")]
        public long BusinessRoleID { get; set; }
       // public bool CiviCRMExportFlag {get; set;}
        public virtual BusinessRole BusinessRole{ get; set; }
        public virtual Contact Contact{ get; set; }
        public virtual BusinessEntity BusinessEntity{ get; set; }
    }
}