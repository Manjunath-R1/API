using FluentValidation;
using ThoughtFocus.Domain.Params;
using System.Linq;
using System;

namespace ThoughtFocus.Validations.InputParameterValidation.FundingSource
{
    public class PaymentScheduleTransactionValidation : AbstractValidator<PaymentScheduleTransParam>
    {
        #region Constructors
        public PaymentScheduleTransactionValidation()
        {
            RuleSet("mandatoryFields", () =>
               {
                   
                    RuleFor(x => x.FundedAmount).NotEmpty().WithMessage("Please enter Amount"); 
                    RuleFor(x => x.TransactionStatusID).NotEmpty().WithMessage("Please select transaction status");
                   RuleFor(x => x.FundingTypeID).NotEmpty().WithMessage("Please select fund type");
                   RuleFor(x => x.TransactionDate).NotEmpty().WithMessage("Please enter transaction date");

                    
               });
        }
        #endregion Constructors
    }
}
