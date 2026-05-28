using ThoughtFocus.Business.Impl.Contact;
using Xunit;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Business.Interfaces.Contact;
using ThoughtFocus.Domain;
using ThoughtFocus.Repository.Interfaces.Master;
using ThoughtFocus.Repository.Interfaces.User;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Repository.Interfaces.Contact;
using Moq;  
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Constants;
using ThoughtFocus.Domain.Response;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using ThoughtFocus.DocumentRepository.StorageService;
using ThoughtFocus.DocumentRepository.AzureBlobStorage;
using ThoughtFocus.Domain.Common;

namespace ThoughtFocus.UnitTests.BlobStorage
{
    public class BlobStorageTests
    {
        #region Fields
        //public readonly IStorageService mockCreateContactBusiness;
        
        #endregion Fields

        [Fact]
        public void GetBlobs()
        {
            DocumentStorageConfiguration appSettings = new DocumentStorageConfiguration(){BlobStorageConnectionString="test"};
            IOptions<DocumentStorageConfiguration> options = Options.Create(appSettings);
            BlobStorageService _blobStorageService = new BlobStorageService(options,null);
            var result = _blobStorageService.UploadFileAsync(null);
            var list = _blobStorageService.ListBlobsAsync();
        } 

    }
}