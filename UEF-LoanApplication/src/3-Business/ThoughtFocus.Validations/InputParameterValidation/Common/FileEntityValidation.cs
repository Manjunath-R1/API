using FluentValidation;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Validations.ValidationModels;

namespace ThoughtFocus.Validations.InputParameterValidation.Common
{
    public class FileEntityValidation : AbstractValidator<FileEntity>
    {

        #region Constructors
        public FileEntityValidation(ValidationRuleModel validationRule)
        {
            RuleSet(RuleSetEnumeration.FileEntityProperties.ToString(), () =>
               {
                   RuleFor(x => x.FileName).NotNull().NotEmpty().WithMessage("FileName is null or empty in FileEntity");
                   RuleFor(x => x.InputStream).NotNull().WithMessage("File Input Stream is null in FileEntity");
                  
               });
        }
        #endregion Constructors

    }
}
