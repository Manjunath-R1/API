using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DocumentRepository.Domain;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class OnDate : ICriteria
    {
        private const int ORDER_INDEX = 4;

        public int OrderIndex { get { return ORDER_INDEX; } }

        public ICypherFluentQuery MeetCriteria(ICypherFluentQuery criteria, IEnumerable<FilterItem> filters, ref bool whereAdded)
        {
            var dateFilter = filters.FirstOrDefault(f => f.Property == "OnDate" && f.Value != null && !string.IsNullOrEmpty(f.Value.ToString()));

            if (dateFilter == null)
                return criteria;

            DateTime onDate = DateTime.ParseExact(dateFilter.Value.ToString(), "MM-dd-yyyy", null);
            var nextDate = onDate.AddDays(1);

            if (whereAdded)
                return criteria.AndWhere<Document>(document => document.LastUploadedDate >= onDate)
                               .AndWhere<Document>(document => document.LastUploadedDate < nextDate);

            whereAdded = true;
            return criteria.Where<Document>(document => document.LastUploadedDate == onDate)
                           .AndWhere<Document>(document => document.LastUploadedDate < nextDate);
        }
    }
}
