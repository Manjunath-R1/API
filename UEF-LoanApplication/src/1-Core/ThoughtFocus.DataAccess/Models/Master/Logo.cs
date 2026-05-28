using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ThoughtFocus.DataAccess.Models.Master

{
    [Table("Logos", Schema = "Master")]
    public partial class Logo : AuditBase

    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }

        [ForeignKey("LogoTypes")]
        public long LogoTypeID { get; set; }
        public string PhysicalFileStorageKey { get; set; }
        public Guid DocumentGUID { get; set; }
        public virtual LogoType LogoTypes { get; set; }

    }
}