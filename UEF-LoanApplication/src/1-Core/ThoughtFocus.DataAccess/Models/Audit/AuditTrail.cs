using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace ThoughtFocus.DataAccess.Models.Audit
{
    [Table("AuditTrail", Schema = "Audit")]
    public partial class AuditTrail
    {
        [Key]
        public int Id { get; set; }
        public long? UserId { get; set; }
        public string Type { get; set; }
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public string PrimaryKey { get; set; }
    }
}