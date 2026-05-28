using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DocumentRepository.Domain;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class ByTimeLine : ICriteria
    {
        private const int ORDER_INDEX = 5;

        public int OrderIndex { get { return ORDER_INDEX; } }

        public ICypherFluentQuery MeetCriteria(ICypherFluentQuery criteria, IEnumerable<FilterItem> filters, ref bool whereAdded)
        {
            var dateFilter = filters.FirstOrDefault(f => f.Property == "DocumentTimeLine" && f.Value != null && !string.IsNullOrEmpty(f.Value.ToString()));

            if (dateFilter == null || dateFilter.Value.ToString().Equals("0"))
                return criteria;

            int timeLineFilterValue = int.Parse(dateFilter.Value.ToString());
            DateTime timeLine = DateTime.Now.AddDays((-timeLineFilterValue));

            if (whereAdded)
                return criteria.AndWhere<Document>(document => document.LastUploadedDate >= timeLine);

            whereAdded = true;
            return criteria.Where<Document>(document => document.LastUploadedDate >= timeLine);
        }
    }
}
