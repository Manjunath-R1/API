using Neo4jClient.Cypher;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.Domain;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public interface ICriteria
    {
        int OrderIndex { get; }
        ICypherFluentQuery MeetCriteria(ICypherFluentQuery criteria, IEnumerable<FilterItem> searchFilters, ref bool whereAdded);
    }
}
