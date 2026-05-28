using FluentValidation;
using ThoughtFocus.Domain.Params;
using System.Linq;
using System;

namespace ThoughtFocus.Validations.InputParameterValidation.FundingSource
{
    public class FundTransactionValidation:AbstractValidator<FundTransactionParam>
    {
        #region Constructors
        public FundTransactionValidation()
        {
            RuleSet("mandatoryFields", () =>
               {
                   
                    RuleFor(x => x.TransactionAmount).NotEmpty().WithMessage("Please enter Amount"); 
                    RuleFor(x => x.Comment).NotEmpty().WithMessage("Please enter Comments");                         
                    RuleFor(x => x.dateOfFunding).NotEmpty().WithMessage("Please enter transaction date");

                    
               });
        }
        #endregion Constructors
    }
}
