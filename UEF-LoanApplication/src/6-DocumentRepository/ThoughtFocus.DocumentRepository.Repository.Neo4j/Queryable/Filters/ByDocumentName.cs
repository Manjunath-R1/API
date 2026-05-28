using Neo4jClient.Cypher;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ThoughtFocus.DocumentRepository.Domain;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class ByDocumentName : ICriteria
    {
        private const int ORDER_INDEX = 1;

        public int OrderIndex { get { return ORDER_INDEX; } }

        public ICypherFluentQuery MeetCriteria(ICypherFluentQuery criteria, IEnumerable<FilterItem> filters, ref bool whereAdded)
        {
            var nameFilter = filters.FirstOrDefault(f => f.Property == "SearchText" && f.Value != null && !string.IsNullOrEmpty(f.Value.ToString()));

            if (nameFilter == null)
                return criteria;

            //Cypher: WHERE document.Name CONTAINS 'searchText' OR document.DocumentID IN {0}
            //Cypher: AND document.Name CONTAINS 'searchText' OR document.DocumentID IN {0}
            var contentSearchFilter = filters.FirstOrDefault(f => f.Property == "ContentSearchResult" && f.Value != null);

            if (contentSearchFilter == null)
                return criteria;

            var contentSearchResult = contentSearchFilter.Value as ContentSearchResult;

            string[] filterNames = { };
            string searchfilterName = null;
            if (whereAdded)

                searchfilterName = Regex.Replace(nameFilter.Value.ToString(), @"\s+", " ");
            filterNames = searchfilterName.Trim().Contains(' ') ? searchfilterName.Trim().Split(' ') : null;

            if (filterNames != null)
            {
                criteria = criteria.AndWhere("((document.Key IN {documentList})")
                                 .WithParam("documentList", contentSearchResult.CloudSearchedDocuments);

                int j = 1;
                foreach (string name in filterNames.Where(i => !string.IsNullOrEmpty(i)))
                {
                    if (j == 1)
                    { 
                        criteria = criteria.OrWhere("((toLower(document.Name) CONTAINS {nameFilter" + j + "})")
                          .WithParam("nameFilter" + j + "", name.ToLower().Trim());
                    }
                    else if (j == filterNames.Length)
                    { 
                    criteria = criteria.AndWhere("(toLower(document.Name) CONTAINS {nameFilter" + j + "})))")
                                      .WithParam("nameFilter" + j + "", name.ToLower().Trim());
                    }
                    else
                    { 
                    criteria = criteria.AndWhere("(toLower(document.Name) CONTAINS {nameFilter" + j + "})")
                                      .WithParam("nameFilter" + j + "", name.ToLower().Trim());
                    }
                    j++;
                }

                return criteria;

            }
            else
            {
                return criteria.AndWhere("(document.Key IN {documentList} OR toLower(document.Name) CONTAINS {nameFilter})")
                           .WithParam("documentList", contentSearchResult.CloudSearchedDocuments)
                           .WithParam("nameFilter", nameFilter.Value.ToString().ToLower().Trim());

            }
        }
    }
}
