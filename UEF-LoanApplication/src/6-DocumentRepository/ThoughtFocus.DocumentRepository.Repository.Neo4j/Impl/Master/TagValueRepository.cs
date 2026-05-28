using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DocumentRepository.Repository.Neo4j;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using Neo4jClient;
using System.Data.SqlClient;
using ThoughtFocus.Common.Exceptions;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl.Master
{
    public class TagValueRepository : AbstractNeo4jBaseRepository<TagValue>, ITagValueRepository
    {
       // private static readonly ILogger<TagValueRepository> Logger;
        private readonly IGraphClient _graphClient;

        #region Constructors

        public TagValueRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        #endregion Constructors


        public void SaveTagValue(List<TagValue> tagValues)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                _graphClient.Cypher
                    .Unwind(tagValues, "tagValue")
                    .Merge("(T:TagValue {TagValueID : tagValue.TagValueID})")
                    .OnCreate()
                    .Set("T = tagValue")
                    .ExecuteWithoutResultsAsync();

                _graphClient.Cypher.CreateUniqueConstraint("T:TagValue", "T.TagValueID").ExecuteWithoutResultsAsync();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in SaveTagValue repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in SaveTagValue repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in SaveTagValue repository", ex);
            }
        }

        public void SaveTagValue(TagValue tagValue)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient
                    .Cypher
                    .Match("(tag:Tag)")
                    .Where("tag.TagID='" + tagValue.TagID + "'")
                    .Merge("(tagValue:TagValue { TagValueID: {TagValueID} })")
                    .CreateUnique("(tag)-[r:HAS]->(tagValue)")                    
                    .Set("tagValue = {tagValue}")
                    .WithParams(new
                    {
                        TagValueID = tagValue.TagValueID,
                        tagValue
                    });

                query.ExecuteWithoutResultsAsync();
                //_graphClient.Cypher.CreateUniqueConstraint("T:TagValue", "T.TagValueID").ExecuteWithoutResultsAsync();

                //query = _graphClient.Cypher
                //    .Match("(tag:Tag)", "(tagValue:TagValue)")
                //    .Where("tag.TagID='" + tagValue.TagID+"'")
                //     .AndWhere("tagValue.TagValueID='" + tagValue.TagValueID + "'")
                //      .CreateUnique("(tag)-[r:HAS]->(tagValue)");

                //query.ExecuteWithoutResultsAsync();


                //_graphClient.Cypher.CreateUniqueConstraint("T:TagValue", "T.TagValueID").ExecuteWithoutResultsAsync();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in SaveTagValue repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in SaveTagValue repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in SaveTagValue repository", ex);
            }
        }


        public List<TagValue> GetTagValueListById(Guid TagID)
        {
            List<TagValue> tagValueList = new List<TagValue>();
            try
            {

                var Query = _graphClient.Cypher
                    .Match("(tagValue:TagValue)")
                    .Where((TagValue tagValue) => tagValue.TagID.ToString() == TagID.ToString() && tagValue.IsActive == true)
                    .Return((tagValue) => new
                    {
                        tagValue = tagValue.As<TagValue>()
                    });

                tagValueList = Query.ResultsAsync.Result.Select(t =>
                    new TagValue
                    {
                        TagValueID = t.tagValue.TagValueID,
                        TagID = t.tagValue.TagID,
                        Value = t.tagValue.Value,
                    }).ToList();

                return tagValueList;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while Getting the GetTagValueListById", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while Getting the GetTagValueListById", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while Getting the GetTagValueListById", ex);
            }
        }



        public TagValue GetTagValueById(Guid TagID, Guid TagValueID)
        {
            TagValue tagValue = new TagValue();
            try
            {

                var Query = _graphClient.Cypher
                    .Match("(t:TagValue)")
                    .Where((TagValue t) => t.TagValueID.ToString() == TagValueID.ToString() && t.TagID.ToString() == TagID.ToString() && t.IsActive == true)
                    .Return(t => t.As<TagValue>());

                tagValue = Query.ResultsAsync.Result.FirstOrDefault();

                return tagValue;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while Getting the GetTagValueById", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while Getting the GetTagValueById", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while Getting the GetTagValueById", ex);
            }
        }
    }
}
