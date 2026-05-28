using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.DocumentManager.Interfaces
{
    public interface IApplicationDocumentFacade
    {
         ApplicationDocumentResponse GetApplicationDocumentByVersionID(string documentVersionkey, UserSessionEntity userSessionEntity);
         DocumentResponse GetDocumentIDByTypeAndApplication(long documentTypeID, long applicationID);
    }
}
