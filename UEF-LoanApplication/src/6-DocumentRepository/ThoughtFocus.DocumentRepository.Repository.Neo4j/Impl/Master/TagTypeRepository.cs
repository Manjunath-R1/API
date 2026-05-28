using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.Repository.Neo4j;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using Neo4jClient;
using System.Data.SqlClient;
using ThoughtFocus.Common.Exceptions;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl.Master
{
    public class TagTypeRepository : AbstractNeo4jBaseRepository<TagType>, ITagTypeRepository
    {

        //private static readonly ILogger<TagTypeRepository> Logger;
        private readonly IGraphClient _graphClient;

        #region Constructors

        public TagTypeRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        #endregion Constructors 
    
    
        public void SaveTagTypes(List<TagType> tagTypes)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                _graphClient.Cypher
                    .Unwind(tagTypes, "tagType")
                    .Merge("(T:TagType {TagTypeID : tagType.TagTypeID})")
                    .OnCreate()
                    .Set("T = tagType")
                    .ExecuteWithoutResultsAsync();

                _graphClient.Cypher.CreateUniqueConstraint("T:TagType", "T.TagTypeID").ExecuteWithoutResultsAsync();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in SaveTagType repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in SaveTagType repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in SaveTagType repository", ex);
            }
        }

        public void SaveTagType(TagType tagType)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();


                var query = _graphClient
                        .Cypher
                        .Merge("(tagType:TagType { TagTypeID: {TagTypeID} })")
                        .OnCreate()
                        .Set("tagType = {tagType}")
                        .WithParams(new
                        {
                            TagTypeID = tagType.TagTypeID,
                            tagType
                        });

                query.ExecuteWithoutResultsAsync();

                _graphClient.Cypher.CreateUniqueConstraint("T:TagType", "T.TagTypeID").ExecuteWithoutResultsAsync();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in SaveTagType repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in SaveTagType repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in SaveTagType repository", ex);
            }
        }
    }
}
