using FluentValidation;
using ThoughtFocus.Domain.Params;
using System.Linq;
using System;

namespace ThoughtFocus.Validations.InputParameterValidation.FundingSource
{
    public class FundingSourceValidation:AbstractValidator<FundingSourceParam>
    {
        #region Constructors
        public FundingSourceValidation()
        {
            RuleSet("mandatoryFields", () =>
               {
                   
                    RuleFor(x => x.ProgramName).NotEmpty().WithMessage("Please enter Program Name."); 
                    RuleFor(x => x.FundingEntityName).NotEmpty().WithMessage("Please enter Funding Entity Name.");  
                    RuleFor(x => x.FundingTypeID).GreaterThan(0).WithMessage("Please select Funding Type.");     
               });

            RuleSet("invalidInput", () =>
               {
                    RuleFor(x => x.ProgramName).Must(IsValidName).WithMessage("Please enter only alphabets for Program Name."); 
                    RuleFor(x=> x.FundingEntityName).Must(IsValidName).WithMessage("Please enter only alphabets for Funding Entity Name.");
               });

        }
        #endregion Constructors

        private bool IsValidName(string name)
        {
            return !name.All(Char.IsNumber);
        }
    }
}
