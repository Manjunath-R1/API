using Neo4jClient.Cypher;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DocumentRepository.Domain;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class ByDocumentNumber : ICriteria
    {
        private const int ORDER_INDEX = 2;

        public int OrderIndex { get { return ORDER_INDEX; } }

        public ICypherFluentQuery MeetCriteria(ICypherFluentQuery criteria, IEnumerable<FilterItem> filters, ref bool whereAdded)
        {
            var numberFilter = filters.FirstOrDefault(f => f.Property == "DocumentNumber" && f.Value != null && !string.IsNullOrEmpty(f.Value.ToString()));
            
            if(numberFilter == null)
                return criteria;

            if (whereAdded)
                return criteria.AndWhere<Document>(document => document.Number.ToLower() == numberFilter.Value.ToString().ToLower());

            whereAdded = true;
            return criteria.Where<Document>(document => document.Number.ToLower() == numberFilter.Value.ToString().ToLower());
        }
    }
}
