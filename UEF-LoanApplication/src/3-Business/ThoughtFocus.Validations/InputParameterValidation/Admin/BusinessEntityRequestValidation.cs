using FluentValidation;
using ThoughtFocus.Domain.Params;
using System.Linq;
using System;

namespace ThoughtFocus.Validations.InputParameterValidation.Admin
{
    public class BusinessEntityRequestValidation:AbstractValidator<BusinessEntityRequest>
    {
        #region Constructors
        public BusinessEntityRequestValidation()
        {
            RuleSet("mandatoryFields", () =>
               {
                   
                    RuleFor(x => x.BusinessName).NotEmpty().WithMessage("Please enter Business Name"); 
                    RuleFor(x => x.AffiliateID).GreaterThan(0).WithMessage("Please select Affiliate");                         
                    RuleFor(x => x.BusinessTypeID).GreaterThanOrEqualTo(0).WithMessage("Please select Business Type");
                    RuleFor(x => x.EIN).NotEmpty().WithMessage("Please enter EIN");
               });
        }
        #endregion Constructors
    }
}
