using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("State", Schema = "Master")]
    public partial class State
    {
        [Key]
        public long StateID { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> DisplayOrder { get; set; }
    }
}