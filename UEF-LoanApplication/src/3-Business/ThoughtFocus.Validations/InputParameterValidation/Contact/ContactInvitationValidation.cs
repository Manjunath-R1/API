using FluentValidation;
using ThoughtFocus.Domain.Contact;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Validations.ValidationModels;
using ThoughtFocus.Domain.Params;
using System.Linq;
using System;

namespace ThoughtFocus.Validations.InputParameterValidation.Contact
{
    public class ContactInvitationValidation:AbstractValidator<ContactRequest>
    {
        #region Constructors
        public ContactInvitationValidation()
        {
            RuleSet("mandatoryFields", () =>
               {
                   
                    RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter First name"); 
                    RuleFor(x => x.LastName).NotEmpty().WithMessage("Please enter Last name");  
                    RuleFor(x => x.PhoneNo).NotEmpty().WithMessage("Please enter Phone number");
                    RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Please enter Email address");        
                    RuleFor(x => x.SalutationID).GreaterThan(0).WithMessage("Please select Title");   
                    RuleFor(x => x.UserRoles).NotNull().WithMessage("Please assign User roles");       
               });

            RuleSet("invalidInput", () =>
               {
                   
                   // RuleFor(x => x.FirstName).Must(IsValidName).WithMessage("Please enter only alphabets for First name"); 
                   // RuleFor(x=> x.MiddleName).Must(IsValidName).WithMessage("Please enter only alphabets for Middle name");
                   // RuleFor(x => x.LastName).Must(IsValidName).WithMessage("Please enter only alphabets for Last name");  
                    //RuleFor(x => x.PhoneNo).Length(10).WithMessage("Please enter 10 digits Phone number for Phone number field");
                    RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Please enter valid Email address");
              
               });

        }
        #endregion Constructors

        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }
    }
}
