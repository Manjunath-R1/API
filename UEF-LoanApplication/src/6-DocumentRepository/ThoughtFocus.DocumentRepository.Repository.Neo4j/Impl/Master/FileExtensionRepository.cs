using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.Common.Exceptions;
using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl.Master
{
    public class FileExtensionRepository : IFileExtensionTypeRepository
    {
        private readonly IGraphClient _graphClient;
        //private static readonly ILogger<FileExtensionRepository> Logger;

        #region Constructors

        public FileExtensionRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        #endregion Constructors

        public List<FileExtensionType> GetAll()
        {
            try
            {
                var feTypes = _graphClient.Cypher
                    .Match("(fetype:FileExtensionType)")
                    .Where((FileExtensionType fetype) => fetype.IsActive == true)
                    .Return((fetype) => fetype.As<FileExtensionType>())
                    .ResultsAsync.Result.ToList();

                return feTypes;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetAll() of FileExtensionRepository repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetAll() of FileExtensionRepository repository", ex);
            }
        }

        public FileExtensionType GetFileExtensionByValue(string value)
        {
            try
            {
                var type = _graphClient.Cypher
                    .Match("(fetype:FileExtensionType)")
                    .Where((FileExtensionType fetype) => fetype.IsActive == true && fetype.Value == value)
                    .Return((fetype) => fetype.As<FileExtensionType>()).ResultsAsync.Result.FirstOrDefault();

                return type;
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetFileExtensionByValue() of FileExtensionRepository repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetFileExtensionByValue() of FileExtensionRepository repository", ex);
            }
        }

        public FileExtensionType GetExtensionTypeByID(Guid typeID)
        {
            try
            {
                var type = _graphClient.Cypher
                    .Match("(fetype:FileExtensionType)")
                    .Where((FileExtensionType fetype) => fetype.IsActive == true && fetype.FileExtensionTypeID.ToString() == typeID.ToString())
                    .Return((fetype) => fetype.As<FileExtensionType>())
                    .ResultsAsync.Result.FirstOrDefault();

                return type;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetFileExtensionByID() of FileExtensionRepository repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetFileExtensionByID() of FileExtensionRepository repository", ex);
            }
        }

        public void SaveFileExtensionType(FileExtensionType fileExtensionType)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                if (fileExtensionType != null)
                {
                    var query = _graphClient
                          .Cypher
                          .Match("(feCategory:FileExtensionCategory)")
                          .Where("feCategory.FileExtensionCategoryID=" + fileExtensionType.FileExtensionCategoryID) 
                          .Merge("(fileExtensionType:FileExtensionType { FileExtensionTypeID: {FileExtensionTypeID} })")   
                          .CreateUnique("(fileExtensionType)-[r:BELONGS_TO]->(feCategory)")
                          .Set("fileExtensionType = {fileExtensionType}")
                          .WithParams(new
                          {
                              FileExtensionTypeID = fileExtensionType.FileExtensionTypeID,
                              fileExtensionType
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
