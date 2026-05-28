using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Enumeration;
using ThoughtFocus.DocumentRepository.Domain.Response;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.Common.Exceptions.BusinessException;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class DocumentTagValueValidator : IDocumentTagValueValidator
    {
        //private static readonly ILogger<DocumentTagValueValidator> Logger;
        private ITagValueRepository _tagValueRepository;
        public DocumentTagValueValidator(ITagValueRepository tagValueRepository)
        {
            this._tagValueRepository = tagValueRepository;
        }

        public ValidationResponse ValidateTagValue(DocumentTagViewModel documentTag)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            validationResponse.IsSuccess = true;
            try
            {
                if (documentTag != null)
                {
                    var type = typeof(IValueValidators);
                    var validators = Assembly.GetExecutingAssembly()
                                            .GetTypes()
                                            .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);
                    foreach (var validator in validators.Select(
                            provider => (IValueValidators)Activator.CreateInstance(provider, this._tagValueRepository)))
                    {
                        if (validators != null && validator.CanValidate(documentTag.TagTypeID))
                        {
                            validationResponse = validator.ValidateValue(documentTag);
                        }
                    }
                }
                else
                {
                    validationResponse.IsSuccess = false;

                }
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (TargetInvocationException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);

            }
            return validationResponse;
        }
    }

    public class NumericTypeValidator : IValueValidators
    {
        //private static readonly ILogger<NumericTypeValidator> Logger;
        private ITagValueRepository _tagValueRepository;

        public NumericTypeValidator(ITagValueRepository tagValueRepository)
        {
            this._tagValueRepository = tagValueRepository;
        }

        public bool CanValidate(long tagType)
        {
            bool canValidate = false;
            if (tagType == (long)TagTypeEnumeration.Numeric)
            {
                canValidate = true;
            }
            return canValidate;
        }

        public ValidationResponse ValidateValue(DocumentTagViewModel documentTag)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            double number;
            validationResponse.IsSuccess = true;
            try
            {
                if (Double.TryParse(documentTag.Value, out number))
                {
                    validationResponse.IsValid = true;
                }
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            return validationResponse;
        }
    }

    public class AlphaNumericTypeValidator : IValueValidators
    {
        //private static readonly ILogger<AlphaNumericTypeValidator> Logger;
        private ITagValueRepository _tagValueRepository;

        public AlphaNumericTypeValidator(ITagValueRepository tagValueRepository)
        {
            this._tagValueRepository = tagValueRepository;
        }
        public bool CanValidate(long tagType)
        {
            bool canValidate = false;
            if (tagType == (long)TagTypeEnumeration.AlphaNumeric)
            {
                canValidate = true;
            }
            return canValidate;
        }

        public ValidationResponse ValidateValue(DocumentTagViewModel documentTag)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            validationResponse.IsSuccess = true;
            try
            {
                if (documentTag.Value.All(char.IsLetterOrDigit) || documentTag.Value.Contains(' '))
                {
                    validationResponse.IsValid = true;
                }
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            return validationResponse;
        }
    }

    public class DateTimeTypeValidator : IValueValidators
    {
        //private static readonly ILogger<DateTimeTypeValidator> Logger;
        private ITagValueRepository _tagValueRepository;

        public DateTimeTypeValidator(ITagValueRepository tagValueRepository)
        {
            this._tagValueRepository = tagValueRepository;
        }

        public bool CanValidate(long tagType)
        {
            bool canValidate = false;
            if (tagType == (long)TagTypeEnumeration.DateTime)
            {
                canValidate = true;
            }
            return canValidate;
        }

        public ValidationResponse ValidateValue(DocumentTagViewModel documentTag)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            DateTime dateValue;
            validationResponse.IsSuccess = true;
            try
            {
                if (DateTime.TryParse(documentTag.Value, out dateValue))
                {
                    validationResponse.IsValid = true;
                }
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            return validationResponse;
        }
    }

    public class ListTypeValidator : IValueValidators
    {
        //private static readonly ILogger<ListTypeValidator> Logger;
        private ITagValueRepository _tagValueRepository;

        public ListTypeValidator(ITagValueRepository tagValueRepository)
        {
            this._tagValueRepository = tagValueRepository;
        }

        public bool CanValidate(long tagType)
        {
            bool canValidate = false;
            if (tagType == (long)TagTypeEnumeration.List)
            {
                canValidate = true;
            }
            return canValidate;
        }

        public ValidationResponse ValidateValue(DocumentTagViewModel documentTag)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            validationResponse.IsSuccess = true;
            Guid number;
            try
            {
                if (!(Guid.TryParse(documentTag.Value, out number)))
                {
                    validationResponse.IsValid = false;
                    return validationResponse;
                }
                //TagValue tagValue = this._tagValueRepository.FirstOrDefault(a => a.TagID == documentTag.TagID && a.TagValueID == number && a.IsActive == true);
                TagValue tagValue = this._tagValueRepository.GetTagValueById(documentTag.TagID, number); 
                if (tagValue != null)
                {
                    validationResponse.IsValid = true;
                }
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            return validationResponse;
        }
    }

    public class BooleanValidator : IValueValidators
    {
        //private static readonly ILogger<BooleanValidator> Logger;
        private ITagValueRepository _tagValueRepository;

        public BooleanValidator(ITagValueRepository tagValueRepository)
        {
            this._tagValueRepository = tagValueRepository;
        }

        public bool CanValidate(long tagType)
        {
            bool canValidate = false;
            if (tagType == (long)TagTypeEnumeration.Boolean)
            {
                canValidate = true;
            }
            return canValidate;
        }

        public ValidationResponse ValidateValue(DocumentTagViewModel documentTag)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            validationResponse.IsSuccess = true;
            try
            {
                if (documentTag.Value.ToLower() == "no" || documentTag.Value.ToLower() == "yes")
                {
                    validationResponse.IsValid = true;
                }
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            return validationResponse;
        }
    }
}
