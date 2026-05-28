using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DataAccess.Models;

namespace ThoughtFocus.Repository.Interfaces.Application
{
    public interface IApplicationDocumentRepository : IEFApplicationBaseRepository<ApplicationDocument>
    {
        List<ApplicationDocument> GetApplicationDocuments(long applicationID);
        List<ApplicationDocument> GetApplicationDocumentsByApplicationIDAndDocumentCatergoryID(long applicationID, int documentCategoryID);
        ApplicationDocument GetApplicationDocumentByApplicationIDAndDocumentTypeID(long applicationID, int documentTypeID);
        void SaveApplicationDocument(DataAccess.Models.ApplicationDocument applicationDocument);
        ApplicationDocument GetApplicationDocumentByApplicationIDAndDocumentTypeIDAndName(long applicationID, int documentTypeID, string name);
        void SaveApplicationDocumentRequest(ApplicationDocumentRequestLog documentRequestLog);
    }
}
