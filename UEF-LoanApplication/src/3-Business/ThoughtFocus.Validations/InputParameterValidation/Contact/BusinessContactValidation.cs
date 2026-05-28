using FluentValidation;
using ThoughtFocus.Domain.Contact;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Validations.ValidationModels;
using ThoughtFocus.Domain.Params;
using System.Linq;
using System;

namespace ThoughtFocus.Validations.InputParameterValidation.Contact
{
    public class BusinessContactValidation:AbstractValidator<BusinessContactRequest>
    {
        #region Constructors
        public BusinessContactValidation()
        {
            RuleSet("mandatoryFields", () =>
               {
                    
                    RuleFor(x => x.ContactID).GreaterThan(0).WithMessage("Please select the Contact");
                    RuleFor(x => x.BusinessRoleID).GreaterThan(0).WithMessage("Please select Business Role");
                    RuleFor(x => x.BusinessID).GreaterThan(0).WithMessage("Please select Business");  
                       
               });

            

        }
        #endregion Constructors

        
    }
}
