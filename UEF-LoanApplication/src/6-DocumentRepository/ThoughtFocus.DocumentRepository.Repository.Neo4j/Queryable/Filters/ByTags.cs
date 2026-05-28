using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DocumentRepository.Domain;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class ByTags : ICriteria
    {
        private const int ORDER_INDEX = 7;

        public int OrderIndex { get { return ORDER_INDEX; } }

        public ICypherFluentQuery MeetCriteria(ICypherFluentQuery criteria, IEnumerable<FilterItem> filters, ref bool whereAdded)
        {
            var tagFilter = filters.FirstOrDefault(f => f.Property == "TagFilters" && f.ListValues.Any());

            if (tagFilter == null)
                return criteria;

            criteria = criteria
                        .With<Document>(document => document.As<Document>())
                        .Match("(document)-[:HAS]->(documentTag:DocumentTag)");

            var withMatchWhereAdded = false;
            int j = 1;

            foreach (var tag in tagFilter.ListValues)
            {
                var tagKey = Guid.Parse(tag.Key);
                if (withMatchWhereAdded)
                {

                    criteria = criteria
                       .With<Document>(document => document.As<Document>())
                       .Match("(document)-[:HAS]->(documentTag:DocumentTag)");

                    criteria = criteria.Where("((documentTag.TagID = {documentTagKey" + j + "}) and (toLower(documentTag.Value) = {documentTagValue" + j + "}) and (documentTag.IsActive = true))")
                         .WithParam("documentTagKey" + j + "", tagKey)
                         .WithParam("documentTagValue" + j + "", tag.Value.ToLower().Trim());

                    j++;

                }
                else
                {
                    withMatchWhereAdded = true;

                    criteria = criteria.Where("((documentTag.TagID = {tagKey}) and (toLower(documentTag.Value) = {tagValue}) and (documentTag.IsActive = true))")
                               .WithParam("tagKey", tagKey)
                               .WithParam("tagValue", tag.Value.ToLower().Trim());
                }
                whereAdded = true;
            }


            return criteria;
        }

        //public ICypherFluentQuery MeetCriteria(ICypherFluentQuery criteria, IEnumerable<FilterItem> searchFilters, ref bool whereAdded)
        //{
        //    return criteria;
        //}
    }
}
