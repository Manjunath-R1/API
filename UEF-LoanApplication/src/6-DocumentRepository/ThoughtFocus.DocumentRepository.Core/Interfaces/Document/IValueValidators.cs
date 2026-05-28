using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Response;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IValueValidators
    {
        bool CanValidate(long tagType);

        ValidationResponse ValidateValue(DocumentTagViewModel documentTag);
    }
}
