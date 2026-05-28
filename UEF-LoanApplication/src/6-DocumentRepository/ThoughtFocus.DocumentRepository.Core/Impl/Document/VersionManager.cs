using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.Common.Exceptions.BusinessException;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class VersionManager : IVersionManager
    {

        private readonly ILogger<VersionManager> _logger;
        public IDocumentRepository _documentRepository;
        public int InitialMajorVersionValue = 0;
        public int InitialMinorVersion = 1;

        public VersionManager(IDocumentRepository documentRepository,
            ILogger<VersionManager> logger
            )
        {
            _documentRepository = documentRepository;
            _logger = logger;
        }

        public VersionEntity GetNextVersion(Guid documentID)
        {
            VersionEntity versionEntity = new VersionEntity();
            try
            {
                Document document = this._documentRepository.GetDocumentById(documentID);

                if (document != null)
                {
                    versionEntity.MajorVersion = document.MajorVersion.Value;
                    versionEntity.MinorVersion = document.MinorVersion.Value + 1;
                }
                else
                {
                    versionEntity = this.GetInitialVersion();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while fetching Next Version {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching Next Version {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching Next Version {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching Next Version {0}", ex));
                throw new BusinessException("", ex);
            }
            return versionEntity;
        }

        private VersionEntity GetInitialVersion()
        {
            return new VersionEntity
            {
                MajorVersion = this.InitialMajorVersionValue,
                MinorVersion = this.InitialMinorVersion
            };
        }
        
    }
}
