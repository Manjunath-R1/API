using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;

namespace ThoughtFocus.Validations.Impl.RuleHandler
{
    public static class RuleValidater  
    {
        public static ValidationResult Validate<T>(this IValidator<T> validator, T instance, string[] ruleSet)
        {
            IValidatorSelector selector = null;
            if (ruleSet != null)
            {
                //var ruleSetNames = ruleSet.Split(',', ';').Select(x => x.Trim());
                selector = ValidatorOptions.ValidatorSelectors.RulesetValidatorSelectorFactory(ruleSet);
            }
            var context = new ValidationContext<T>(instance, new PropertyChain(), selector);
            return validator.Validate(context);
        }
    }
}
