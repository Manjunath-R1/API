using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ThoughtFocus.DataAccess.Models.Admin
{
    [Table("CiviCRMOrganizationExportDetails", Schema = "Admin")]
    public partial class CiviCRMOrganizationExportDetail
    {
        [Key]
        public long ID { get; set; }
        
        [ForeignKey("CiviCRMDataExportLog")]
        public Nullable<long> LogID { get; set; }
        
        [ForeignKey("BusinessEntity")]
        public Nullable<long> BusinessEntityID { get; set; }
        
        public virtual CiviCRMDataExportLog CiviCRMDataExportLog {get; set;}
        
        public virtual BusinessEntity BusinessEntity {get; set;}
    }
}