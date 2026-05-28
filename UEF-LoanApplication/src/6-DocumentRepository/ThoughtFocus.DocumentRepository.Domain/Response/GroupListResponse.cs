using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Domain.Response
{
    public class GroupListResponse : DocumentBaseResponse
    {
        public List<Group> Groups { get; set; }
    }
}
