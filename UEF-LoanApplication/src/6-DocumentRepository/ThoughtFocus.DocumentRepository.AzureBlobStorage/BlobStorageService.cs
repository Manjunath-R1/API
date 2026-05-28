using System;
using System.IO;
using Microsoft.Extensions.Logging;
using ThoughtFocus.DocumentRepository.StorageService;
using System.Threading.Tasks;
using ThoughtFocus.DocumentRepository.Domain;
using Azure.Storage.Blobs;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace ThoughtFocus.DocumentRepository.AzureBlobStorage
{
    public class BlobStorageService : IStorageService
    {
        private  readonly ILogger<BlobStorageService> _logger;
        private readonly ThoughtFocus.Domain.Common.DocumentStorageConfiguration _documentStorageConfiguration;

        public BlobStorageService(IOptions<ThoughtFocus.Domain.Common.DocumentStorageConfiguration> documentStorageConfiguration,
            ILogger<BlobStorageService> logger)
        {
            _documentStorageConfiguration = documentStorageConfiguration.Value;
            _logger = logger;
        }

        public Task<DocumentUploadResponse> UploadFileAsync(DocumentUploadRequest request)
        {
           // bool uploadStatus = false;
            DocumentUploadResponse documentUploadResponse = new DocumentUploadResponse();
            try
            {
                string containerName = _documentStorageConfiguration.ContainerName;
                //For logo and other documents
                if (request.DocumentTypeID == 0)
                    containerName = "applicationassets";
                // Get a reference to a container
                BlobContainerClient container = new BlobContainerClient(_documentStorageConfiguration.BlobStorageConnectionString, containerName);

                var upload = container.UploadBlobAsync(request.Key, request.InputStream);
                upload.Wait();
                //uploadStatus = true;
                return Task.FromResult<DocumentUploadResponse>(new DocumentUploadResponse { IsSuccess = true, FileSource = container.Uri.AbsoluteUri });
            }

            catch (Exception ex)
            {
                _logger.LogError("Error encountered at UploadBlobAsync(FileEntity fileEntity)>>", ex);
                throw ex;
            }
        }

        public Task<ProjectResponse> CreateProjectAsync(ProjectRequest projectRequest)
        {
            //bool projectStatus = false;
            try
            {
                // Get a reference to a container
                BlobContainerClient container = new BlobContainerClient(_documentStorageConfiguration.BlobStorageConnectionString, _documentStorageConfiguration.ContainerName);


                var folderKey = projectRequest.PhysicalPath + "/";

                var blob = container.GetBlobClient(folderKey); ;
                // or CloudStorageAccount.Parse("<your connection string>")

                if (!blob.Exists())
                {
                    var upload = container.UploadBlobAsync(folderKey, new MemoryStream());
                    upload.Wait();

                }
                //projectStatus = true;
                return Task.FromResult<ProjectResponse>(new ProjectResponse { IsSuccess = true });
            }

            catch (Exception ex)
            {
                _logger.LogError("Error encountered at UploadFileAsync(FileEntity fileEntity)>>", ex);
                throw ex;
            }
        }

        public async Task<IEnumerable<string>> ListBlobsAsync()
        {
            try
            {
                // Get a reference to a container
                BlobContainerClient container = new BlobContainerClient(_documentStorageConfiguration.BlobStorageConnectionString, _documentStorageConfiguration.ContainerName);




                var items = new List<string>();

                var blobs = container.GetBlobs();

                foreach (var blobItem in blobs)
                {
                    items.Add(blobItem.Name);
                }

                var blobContainer = container.GetBlobClient("");

                var downloadInfo = await blobContainer.DownloadAsync();

                //items = blobs;

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at UploadFileAsync(FileEntity fileEntity)>>", ex);
                throw ex;
            }
        }

        public async Task<DocumentResponse> DownloadDocumentAsync(DocumentRequest request)
        {
            //bool documentStatus = false;
            //string responseBody = "";

            DocumentResponse documentResponse = new DocumentResponse();
            try
            {
                // Get a reference to a container
                BlobContainerClient container = new BlobContainerClient(_documentStorageConfiguration.BlobStorageConnectionString, _documentStorageConfiguration.ContainerName);

                var blob = container.GetBlobClient(request.PhysicalPath);

                bool blobExists = blob.Exists();

                if (!blobExists)
                {
                    container = new BlobContainerClient(_documentStorageConfiguration.BlobStorageConnectionString, "applicationassets");
                    blob = container.GetBlobClient(request.PhysicalPath);
                }

                var fileStream = await blob.OpenReadAsync();

                //return fileStream;
                documentResponse.OutputStream = fileStream;
                documentResponse.Path = request.PhysicalPath;
                documentResponse.Name = request.Name;
                documentResponse.DocumentID = request.Id;
                documentResponse.StorageKey = request.PhysicalPath;
                documentResponse.IsSuccess = true;

                return documentResponse;
            }

            catch (Exception ex)
            {
                _logger.LogError("Error encountered at DownloadFile()>>", ex);
                throw ex;
            }
        }
    }
}
