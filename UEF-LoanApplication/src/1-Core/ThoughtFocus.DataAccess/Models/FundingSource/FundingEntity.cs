using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.FundingSource
{
    [Table("FundingEntities", Schema = "FundingSource")]
    public partial class FundingEntity : AuditBase
    {
        public FundingEntity()
        {
            FundingSources = new List<FundingSource>();
        }
        [Key]
        public long FundingEntityID { get; set; }
        public string FundingEntityName { get; set; }
        public string Address { get; set; }
        public string EIN { get; set; }
        public string TIN { get; set; }

        [ForeignKey("State")]
        public Nullable<long> StateID { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        [ForeignKey("Logo")]
        public Nullable<long> LogoID { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<FundingSource> FundingSources { get; set; }
        public virtual Logo Logo { get; set; }

    }
}