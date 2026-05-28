using System.Linq;

namespace ThoughtFocus.DocumentRepository.Domain.Response
{
    public class DocumentListResponse:DocumentBaseResponse
    {
        public IQueryable<DataAccess.Neo4j.Document> Documents { get; set; }

    }
    
}
