using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain.Response;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IAuthorizerStrategy
    {
        AuthorizationResponse GetAuthorization(AuthorizeRequest AuthorizeRequest);

        int ExcicutionOrder { get; }
    }
}
