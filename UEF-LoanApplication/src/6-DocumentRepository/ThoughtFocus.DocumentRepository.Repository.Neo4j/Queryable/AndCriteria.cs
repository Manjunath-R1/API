using Neo4jClient.Cypher;
using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j.Queryable
{
    public class AndCriteria : ICriteria
    {
        private ICriteria _criteria;
        private ICriteria _otherCriteria;

        public int OrderIndex { get { return 0; } }

        public AndCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            _criteria = criteria;
            _otherCriteria = otherCriteria;
        }

        public ICypherFluentQuery MeetCriteria(ICypherFluentQuery criteria, IEnumerable<Domain.FilterItem> searchFilters, ref bool whereAdded)
        {
            var firstCriteria = _criteria.MeetCriteria(criteria, searchFilters, ref whereAdded);
            var secondCriteria = _otherCriteria.MeetCriteria(firstCriteria, searchFilters, ref whereAdded);

            return secondCriteria;
        }
    }
}
