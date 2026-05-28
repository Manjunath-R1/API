using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DocumentRepository.Domain;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class BetweenDates : ICriteria
    {
        private const int ORDER_INDEX = 6;

        public int OrderIndex { get { return ORDER_INDEX; } }

        public ICypherFluentQuery MeetCriteria(ICypherFluentQuery criteria, IEnumerable<FilterItem> filters, ref bool whereAdded)
        {
            //Filter by Start Date
            var dateFilter = filters.FirstOrDefault(f => f.Property == "StartDate" && f.Value != null && !string.IsNullOrEmpty(f.Value.ToString()));

            if (dateFilter != null)
            {
                var afterDate = DateTime.ParseExact(dateFilter.Value.ToString(), "MM-dd-yyyy", null);

                if (whereAdded)
                    criteria = criteria.AndWhere<Document>(document => document.LastUploadedDate >= afterDate);

                else
                {
                    whereAdded = true;
                    criteria = criteria.Where<Document>(document => document.LastUploadedDate >= afterDate);

                }
            
            }


            //Filter by End Date
            dateFilter = filters.FirstOrDefault(f => f.Property == "EndDate" && f.Value != null && !string.IsNullOrEmpty(f.Value.ToString()));

            if (dateFilter == null)
                return criteria;

            var beforeDate = DateTime.ParseExact(dateFilter.Value.ToString(), "MM-dd-yyyy", null).AddDays(1);

            if (whereAdded)
                return criteria.AndWhere<Document>(document => document.LastUploadedDate < beforeDate);

            whereAdded = true;
            return criteria.Where<Document>(document => document.LastUploadedDate < beforeDate);
        }
    }
}
