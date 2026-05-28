using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.FundingSource
{
    [Table("FundingSources", Schema = "FundingSource")]
    public partial class FundingSource : AuditBase
    {
        public FundingSource()
        {
            FundTransactions = new List<FundTransaction>();
            FundingSourceBusinessTypes = new List<FundingSourceBusinessTypes>();
            FundingSourceStates = new List<FundingSourceStates>();

        }
        [Key]
        public long FundingSourceID { get; set; }
        public string ProgramName { get; set; }

        [ForeignKey("FundingEntity")]
        public long FundingEntityID { get; set; }

        [ForeignKey("FundingType")]
        public int FundingTypeID { get; set; }

        public decimal MinimumLoanAmount { get; set; }
        public decimal MaximumLoanAmount { get; set; }
        public decimal InitialFundedAmount { get; set; }

        [ForeignKey("Logo")]
        public Nullable<long> LogoID { get; set; }

        [ForeignKey("Agreement")]
        public Nullable<long> AgreementID { get; set; }
        public virtual FundingEntity FundingEntity { get; set; }
        public virtual FundingType FundingType { get; set; }
        public virtual ICollection<FundTransaction> FundTransactions { get; set; }
        public virtual ICollection<FundingSourceBusinessTypes> FundingSourceBusinessTypes { get; set; }
        public virtual ICollection<FundingSourceStates> FundingSourceStates { get; set; }
        public bool HasDefaultFundingAmount { get; set; }
        public virtual Logo Logo { get; set; }
        public virtual Agreement Agreement { get; set; }
        public virtual ICollection<ProgramQuestion> ProgramQuestions { get; set; }

        public virtual ICollection<ProgramHelpfulGuide> ProgramHelpfulGuides { get; set; }

        public virtual ICollection<ProgramDocument> ProgramDocuments { get; set; }



    }
}