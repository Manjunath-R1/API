using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("TransactionTypes", Schema = "Master")]
    public partial class TransactionType
    {
        [Key]
        public int TransactionTypeID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
    }
}