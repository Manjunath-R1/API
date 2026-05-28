using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ThoughtFocus.DataAccess.Models.Admin
{
    [Table("CiviCRMContactExportDetails", Schema = "Admin")]
    public partial class CiviCRMContactExportDetail
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("CiviCRMDataExportLog")]
        public Nullable<long> LogID { get; set; }

        [ForeignKey("Contact")]
        public Nullable<long> ContactID { get; set; }

         public virtual CiviCRMDataExportLog CiviCRMDataExportLog { get; set; }

         public virtual ThoughtFocus.DataAccess.Models.Contact.Contact Contact { get; set; }
    }
}