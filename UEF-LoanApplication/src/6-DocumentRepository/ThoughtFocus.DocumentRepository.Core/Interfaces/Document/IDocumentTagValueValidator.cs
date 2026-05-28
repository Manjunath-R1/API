using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Response;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IDocumentTagValueValidator
    {
        ValidationResponse ValidateTagValue(DocumentTagViewModel documentTag);
    }
}
