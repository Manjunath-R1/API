using Neo4jClient.Cypher;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DocumentRepository.Domain;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class ByDocumentExtension : ICriteria
    {
        private const int ORDER_INDEX = 3;

        public int OrderIndex { get { return ORDER_INDEX; } }

        public ICypherFluentQuery MeetCriteria(ICypherFluentQuery criteria, IEnumerable<FilterItem> filters, ref bool whereAdded)
        {
            var extensionFilter = filters.FirstOrDefault(f => f.Property == "Extension" && f.Value != null && !string.IsNullOrEmpty(f.Value.ToString()));

            if (extensionFilter == null)
                return criteria;

            var extensions = extensionFilter.Value.ToString().Split(',').Select(str => long.Parse(str)).ToArray();

            return criteria
                    .With<Document>(document => document.As<Document>())
                    .Match("(document)-[:HAS]->(extension:FileExtensionType)-[:BELONGS_TO]->(category:FileExtensionCategory)")
                    .Where("category.FileExtensionCategoryID IN {extensionList}")
                    .WithParam("extensionList", extensions);
        }
    }
}
