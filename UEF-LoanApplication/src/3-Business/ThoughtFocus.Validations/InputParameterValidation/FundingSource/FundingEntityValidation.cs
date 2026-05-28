using FluentValidation;
using ThoughtFocus.Domain.Params;
using System.Linq;
using System;

namespace ThoughtFocus.Validations.InputParameterValidation.FundingSource
{
    public class FundingEntityValidation:AbstractValidator<FundingEntityRequest>
    {
        #region Constructors
        public FundingEntityValidation()
        {
            RuleSet("mandatoryFields", () =>
               {
                    RuleFor(x => x.FundingEntityName).NotEmpty().WithMessage("Please enter Name"); 
                    RuleFor(x => x.TIN).NotEmpty().WithMessage("Please enter TIN");  
                    RuleFor(x => x.EIN).NotEmpty().WithMessage("Please enter EIN");  
                    RuleFor(x => x.Address).NotEmpty().WithMessage("Please enter Address");     
               });

            RuleSet("invalidInput", () =>
               {
                    // RuleFor(x => x.ProgramName).Must(IsValidName).WithMessage("Please enter only alphabets for Program Name"); 
                    // RuleFor(x=> x.FundingEntityName).Must(IsValidName).WithMessage("Please enter only alphabets for Funding Entity Name");
               });

        }
        #endregion Constructors

        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }
    }
}
