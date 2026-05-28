using FluentValidation;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Validations.ValidationModels;

namespace ThoughtFocus.Validations.InputParameterValidation.Common
{
    public class PageFilterModelValidation : AbstractValidator<PagingFilterModel>
    {

        #region Constructors
        public PageFilterModelValidation(ValidationRuleModel validationRule)
        {
            RuleSet(RuleSetEnumeration.PageFilterModelProperties.ToString(), () =>
               {
                   RuleFor(x => x.Start).NotNull().WithMessage("Start Property is null in PagingFilterModel.");
                   RuleFor(x => x.Length).NotNull().WithMessage("Length Property is null in PagingFilterModel.");
                   RuleFor(x => x.Search).NotNull().WithMessage("Search Property is null in PagingFilterModel.");
                   RuleFor(x => x.Order).NotNull().WithMessage("Order Property is null in PagingFilterModel.");
               });
        }
        #endregion Constructors
    }
}
