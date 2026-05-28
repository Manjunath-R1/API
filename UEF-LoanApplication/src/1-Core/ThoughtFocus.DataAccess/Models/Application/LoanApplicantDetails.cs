using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Master;
using System.ComponentModel.DataAnnotations;

namespace ThoughtFocus.DataAccess.Models.Application
{
    [Table("LoanApplicantDetails", Schema = "Application")]
    public partial class LoanApplicantDetails
    {
        [Key]
        public long LoanApplicantID { get; set; }

        [ForeignKey("LoanApplication")]
        public long LoanApplicationID { get; set; }
        public virtual LoanApplication LoanApplication{ get; set; }


        [ForeignKey("BusinessRole")]
        public long BusinessRoleID { get; set; }
        public virtual BusinessRole BusinessRole { get; set; }

        public string SSN { get; set; } 
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }


        [ForeignKey("Salutation")]
        public long SalutationID { get; set; }
        public virtual Salutation Salutation { get; set; }

        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string CurrentAddress { get; set; }

        [ForeignKey("State")]
        public long StateID { get; set; }
        public virtual State State { get; set; }
        
        public string City { get; set; }
        public string ZipCode { get; set; }

        [ForeignKey("Contact")]
        public long ContactID { get; set; }   
        public virtual ThoughtFocus.DataAccess.Models.Contact.Contact Contact { get; set; } 

    }
}