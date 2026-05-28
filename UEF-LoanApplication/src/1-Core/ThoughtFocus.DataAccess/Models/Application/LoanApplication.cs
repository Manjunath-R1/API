using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Master;
using System.ComponentModel.DataAnnotations;
using ThoughtFocus.DataAccess.Models.Admin;

namespace ThoughtFocus.DataAccess.Models.Application
{
    [Table("LoanApplication", Schema = "Application")]
    public partial class LoanApplication : AuditBase
    {
        public LoanApplication()
        {
            QuestionResponse=new List<QuestionResponse>();
            LoanApplicantDetails=new List<LoanApplicantDetails>();
            BusinessOwners=new List<BusinessOwner>();
            ApplicationDocuments = new List<ApplicationDocument>();
        }
       
        [Key]
        public long LoanApplicationID { get; set; }
        public string LoanNumber { get; set; }
        public bool IsConcentAccepted { get; set; }
        public DateTime ConcentAcceptedDate { get; set; }

        [ForeignKey("ApplicationStatus")]
        public int ApplicationStatusID { get; set; }
        public virtual ApplicationStatus ApplicationStatus { get; set; }
        
        [ForeignKey("ApplicationType")]
        public int ApplicationTypeID { get; set; }   

        [ForeignKey("ProgramInvitation")]
        public long ProgramInvitationID { get; set; }    
        public virtual ApplicationType ApplicationType{ get; set; }
        public System.DateTime DateApplied { get; set; }
         
        public virtual ICollection<LoanApplicantDetails> LoanApplicantDetails{ get; set; }
        
        public virtual ICollection<BusinessOwner> BusinessOwners{ get; set; }
        public virtual LoanBusinessDetail LoanBusinessDetail{ get; set; }
        public virtual FundingApplication FundingApplication{ get; set; }
        public virtual ProgramInvitation ProgramInvitation{ get; set; }
        public virtual List<QuestionResponse> QuestionResponse{ get; set; }
        public virtual ICollection<ApplicationDocument> ApplicationDocuments { get; set; }        
        public virtual PaymentScheduleStatus PaymentScheduleStatus { get; set; }
    }
}