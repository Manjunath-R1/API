using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("Agreement", Schema = "Master")]
    public partial class Agreement
    {
        [Key]
        public long AgreementID { get; set; }
        public bool IsActive { get; set; }
        public string Body { get; set; }
        public string AgreementName { get; set; }

    }
}