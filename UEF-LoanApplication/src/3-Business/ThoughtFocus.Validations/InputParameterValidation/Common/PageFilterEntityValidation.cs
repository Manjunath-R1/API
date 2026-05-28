using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Validations.ValidationModels;

namespace ThoughtFocus.Validations.InputParameterValidation.Common
{
    public class PageFilterEntityValidation : AbstractValidator<PageFilterEntity>
    {
        #region Constructors
        public PageFilterEntityValidation(ValidationRuleModel validationRule)
        {
            RuleSet(RuleSetEnumeration.PageFilterModelProperties.ToString(), () =>                    
               {
                   RuleFor(x => x.SortDirection).NotNull().WithMessage("SortDirection Property is null in PageFilterEntity");
                   RuleFor(x => x.SearchByValue).NotNull().WithMessage("SearchByValue Property is null in PageFilterEntity");
                   RuleFor(x => x.SortBy).NotNull().WithMessage("SortBy Property is null in PageFilterEntity");
                   RuleFor(x => x.PageNumber).NotNull().WithMessage("PageNumber Property is null in PageFilterEntity");
                   RuleFor(x => x.TakeRecordCount).NotNull().WithMessage("TakeRecordCount Property is null in PageFilterEntity");
               });

           

        }
        #endregion Constructors
    }
}
