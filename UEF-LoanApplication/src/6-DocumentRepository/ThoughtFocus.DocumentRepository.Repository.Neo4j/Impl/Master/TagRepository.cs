using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl.Master
{
    public class TagRepository : ITagRepository
    {

        //private static readonly ILogger<TagRepository> Logger;
        private readonly IGraphClient _graphClient;

        #region Constructors

        public TagRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        #endregion Constructors

        public void Save(Tag tag)
        {
             try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient
                     .Cypher
                     .Match("(tagType:TagType)")
                     .Where("tagType.TagTypeID=" + tag.TagTypeID)
                     .Merge("(tag:Tag { TagID: { TagID } })")
                     .CreateUnique("(tag)-[r:BELONGS_TO]->(tagType)")
                     .Set("tag = {tag}")
                     .WithParams(new
                     {
                         TagID = tag.TagID,
                         tag
                     });

                query.ExecuteWithoutResultsAsync();            

                
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in SaveTag repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in SaveTag repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in SaveTag repository", ex);
            }
        }

        public List<Tag> GetAll()
        {
            try
            {
                var tags = _graphClient.Cypher
                    .Match("(tag:Tag)")
                    .Where((Tag tag) => tag.IsActive == true)
                    .Return((tag) => tag.As<Tag>())
                    .ResultsAsync.Result.ToList();

                return tags;
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetAll() of TagRepository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetAll() of TagRepository", ex);
            }
        }

        public Tag GetTagByName(string tagName)
        {
            try
            {
                var tagEntry = _graphClient.Cypher
                    .Match("(tag:Tag)")
                    .Where((Tag tag) => tag.IsActive == true && tag.Name == tagName)
                    .Return((tag) => tag.As<Tag>())
                    .ResultsAsync.Result.FirstOrDefault();

                return tagEntry;
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetTagByName() of TagRepository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetTagByName() of TagRepository", ex);
            }
        }

        public IList<Tag> SearchByName(string searchText)
        {
            try
            {
                var query = _graphClient.Cypher
                    .Match("(tag:Tag)")
                    .Where<Tag>(tag => tag.IsActive == true)
                    .AndWhere("LOWER(tag.Name) CONTAINS LOWER({searchText})")
                    .WithParam("searchText", searchText)
                    .Return((tag) => tag.As<Tag>());

                return query.ResultsAsync.Result.ToList();
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetTagByName() of TagRepository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetTagByName() of TagRepository", ex);
            }
        }
    }
}
