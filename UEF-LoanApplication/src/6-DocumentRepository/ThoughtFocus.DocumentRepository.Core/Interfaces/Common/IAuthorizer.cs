using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain.Response;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IAuthorizer
    {
        AuthorizationResponse AuthorizeUser(AuthorizeRequest AuthorizeRequest);
    }
}
