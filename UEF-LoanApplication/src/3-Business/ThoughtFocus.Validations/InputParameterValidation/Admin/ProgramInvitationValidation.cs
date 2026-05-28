using FluentValidation;
using ThoughtFocus.Domain.Contact;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Validations.ValidationModels;
using ThoughtFocus.Domain.Params;
using System.Linq;
using System;

namespace ThoughtFocus.Validations.InputParameterValidation.Admin
{
    public class ProgramInvitationValidation:AbstractValidator<ProgramInvitationRequest>
    {
        #region Constructors
        public ProgramInvitationValidation()
        {
            RuleSet("mandatoryFields", () =>
               {
                    RuleFor(x => x.BusinessID).GreaterThan(0).WithMessage("Please select the Business");
                    RuleFor(x => x.ProgramID).GreaterThan(0).WithMessage("Please select the Program");
                    RuleFor(x => x.ProgramStatusID).GreaterThan(0).WithMessage("Please select the status");
                    RuleFor(x => x.ContactID).GreaterThan(0).WithMessage("Please select the Contact");  
                       
               });

            

        }
        #endregion Constructors

        
    }
}