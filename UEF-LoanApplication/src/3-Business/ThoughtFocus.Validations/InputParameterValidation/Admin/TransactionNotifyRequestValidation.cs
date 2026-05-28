using FluentValidation;
using ThoughtFocus.Domain.Contact;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Validations.ValidationModels;
using ThoughtFocus.Domain.Params;
using System.Linq;
using System;

namespace ThoughtFocus.Validations.InputParameterValidation.Admin
{
    public class TransactionNotifyRequestValidation : AbstractValidator<TransactionNotifyRequest>
    {
        #region Constructors
        public TransactionNotifyRequestValidation()
        {
            RuleSet("mandatoryFields", () =>
               {
                   RuleFor(x => x.ApplicationID).NotEmpty().WithMessage("Please check Application id!");
                   RuleFor(x => x.TransactionDate).NotEmpty().WithMessage("Please check transaction date!");
                
                       
               });

            

        }
        #endregion Constructors

        
    }
}