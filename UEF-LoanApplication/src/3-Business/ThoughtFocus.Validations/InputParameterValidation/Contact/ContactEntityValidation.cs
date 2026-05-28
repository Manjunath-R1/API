using FluentValidation;
using ThoughtFocus.Domain.Contact;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Validations.ValidationModels;
using ThoughtFocus.Domain.Params;
using System.Linq;
using System;

namespace ThoughtFocus.Validations.InputParameterValidation.Contact
{
    public class ContactEntityValidation:AbstractValidator<ContactRequest>
    {
        #region Constructors
        public ContactEntityValidation(ValidationRuleModel validationRule)
        {
            RuleSet(RuleSetEnumeration.ContactEntityValidation.ToString(), () =>
               {
                   
                    RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter first name"); 
                    RuleFor(x => x.LastName).NotEmpty().WithMessage("Please enter last name");  
                    RuleFor(x => x.PhoneNo).NotEmpty().WithMessage("Please enter Phone number");
                    RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Please enter Email address");   
                    RuleFor(x => x.SalutationID).NotNull().WithMessage("Please select Title");
                                 
               });

        }
        #endregion Constructors

        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }
    }
}
