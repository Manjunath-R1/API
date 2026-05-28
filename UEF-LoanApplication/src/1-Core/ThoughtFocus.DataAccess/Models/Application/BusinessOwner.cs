using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.Application
{
    [Table("BusinessOwner", Schema = "Application")]
    public partial class BusinessOwner	
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("LoanApplication")]
        public long LoanApplicationID { get; set; }
        public string BusinessOwnerName { get; set; }
        public string EmailAddress { get; set; }

         public string PhoneNumber { get; set; }
        public decimal? OwnedPercentage { get; set; }

        [ForeignKey("Veteran")]
        public long? VeteranID { get; set; }

        [ForeignKey("Gender")]
        public long? GenderID { get; set; }

        [ForeignKey("Race")]
        public long? RaceID { get; set; }

        [ForeignKey("Ethnicity")]
        public long? EthnicityID { get; set; }
        public string Demographic  { get; set; }
        public bool IsActive { get; set; }

        public virtual LoanApplication LoanApplication { get; set; }
        public virtual Veteran Veteran { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Race Race { get; set; }
        public virtual Ethnicity Ethnicity { get; set; }
        
    }
}