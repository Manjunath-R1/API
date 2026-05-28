using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Admin
{
    [Table("CiviCRMDataExportLogs", Schema = "Admin")]
    public partial class CiviCRMDataExportLog
    {
        [Key]
        public long ID { get; set; }
        public long ExportedBy { get; set; }
        public DateTime ExportedOn { get; set; }
        public int ExportType {get; set;}
        public int Recordscount {get; set;}
        public bool IsActive { get; set; }
        public virtual ICollection<CiviCRMContactExportDetail> ExportedContactDetails{ get; set; }
        public virtual ICollection<CiviCRMOrganizationExportDetail> ExportedOrganizationDetails{ get; set; }
    }
}