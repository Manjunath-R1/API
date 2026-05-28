using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DocumentRepository.Repository.Neo4j;

using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using Neo4jClient;
using ThoughtFocus.Common.Exceptions;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl.Master
{
    public class FileExtensionCategoryRepository : AbstractNeo4jBaseRepository<FileExtensionCategory>, IFileExtensionCategoryRepository
    {
        private IGraphClient _graphClient;
        //private static readonly ILogger<FileExtensionCategoryRepository> Logger;

        #region Constructors

        public FileExtensionCategoryRepository(IGraphClient graphClient)
        {
            this._graphClient = graphClient;
        }

        #endregion Constructors

        public List<FileExtensionCategory> GetAll()
        {
            try
            {
                var fileExCategories = _graphClient.Cypher
                    .Match("(feCategory:FileExtensionCategory)")
                    .Where((FileExtensionCategory feCategory) => feCategory.IsActive == true)
                    .Return((feCategory) => feCategory.As<FileExtensionCategory>());

                var feCategories = fileExCategories.ResultsAsync.Result.ToList();

                return feCategories;
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetAll() of FileExtensionCategoryRepository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetAll() of FileExtensionCategoryRepository", ex);
            }
        }


        public void SaveFileExtensionCategory(FileExtensionCategory fileExtensionCategory)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                if (fileExtensionCategory != null)
                {
                    var query = _graphClient
                          .Cypher
                          .Merge("(fileExtensionCategory:FileExtensionCategory { FileExtensionCategoryID: {FileExtensionCategoryID} })")
                          .OnCreate()
                          .Set("fileExtensionCategory = {fileExtensionCategory}")
                          .WithParams(new
                          {
                              FileExtensionCategoryID = fileExtensionCategory.FileExtensionCategoryID,
                              fileExtensionCategory
                          });
                    query.ExecuteWithoutResultsAsync();
                }


            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while inserting the SaveFile Extension Category", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while inserting the SaveFile Extension Category", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while inserting the SaveFile Extension Category ", ex);
            }
        }
    }


}
