using System;
using Xunit;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Business.Interfaces.Contact;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.Contact;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Service.Interfaces;
using Moq;
using ThoughtFocus.Service.Impl;
using ThoughtFocus.Constants;
using ThoughtFocus.Repository.Interfaces.Contact;
using ThoughtFocus.Repository.Interfaces.User;
using AutoMapper;
using System.Collections.Generic;
using ThoughtFocus.Service.ModelsMapping;
using ThoughtFocus.UnitTests.DataProvider;
using ThoughtFocus.DataAccess;
using ThoughtFocus.Repository.Impl.User;
using ThoughtFocus.Repository.Impl.Contact;
using ThoughtFocus.DocumentRepository.Repository.Neo4j;
using Neo4jClient;
using ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.Domain.Request;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Neo4jClient.Execution;
using System.Text;
using System.Linq;

namespace ThoughtFocus.UnitTests.Services
{
    public class UploadDocumentTests
    {
        #region Fields
       // ApplicationDBContext context;
        public readonly IContactService mockContactService;
        public readonly IUserRepository userRepo;
        public readonly IContactRepository contactRepo;
        public readonly IContactService contactService;

        #endregion Fields

        public UploadDocumentTests()
        {
            // var db = new FakeAppDBContext();
            // context = db.CreateContextForInMemory();

            // var mockUserRepoLogger = new Mock<ILogger<UserRepositoryImpl>>();
            // userRepo = new UserRepositoryImpl(context, mockUserRepoLogger.Object);

            // contactRepo = new ContactRepositoryImpl(context);

            // //var _mapper = new Mock<IMapper>();
            // var contactInfo = new ModelMappingConfiguration();
            // var configuration = new MapperConfiguration(cfg => cfg.AddProfile(contactInfo));
            // IMapper mapper = new Mapper(configuration);

            // var modelMapping = new Mock<ModelMappingConfiguration>();
            // var mockContactServiceLogger = new Mock<ILogger<ContactServiceImpl>>();

            // contactService = new ContactServiceImpl(mockContactServiceLogger.Object, contactRepo, userRepo, mapper);

        }

        [Fact]
        public async Task UploadDocumentWithOutErrorAsync()
        {
            // arrange
            var fileUploadRequest = new FileUploadRequest();

            MultipartFormDataContent form = new MultipartFormDataContent();

            var filestream = await GetTestImage();

            form.Add(new StreamContent(filestream), "file", "C:/Users/104048.TFCORP/Downloads/10-28-2020 FINAL Gadsden SVR.pdf");

            //fileUploadRequest.FileName ="10-28-2020 FINAL Gadsden SVR.pdf";



            // var response = await client.PostAsync("/api/files", form);

            // Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // var fileResponse = await client.GetAsync("/test_images/base.png");
            // Assert.Equal(HttpStatusCode.OK, fileResponse.StatusCode);
        }
        private async Task<Stream> GetTestImage()
        {
            var memoryStream = new MemoryStream();
            var fileStream = System.IO.File.OpenRead("C:/Users/104048.TFCORP/Downloads/10-28-2020 FINAL Gadsden SVR.pdf");
            await fileStream.CopyToAsync(memoryStream);
            fileStream.Close();
            return memoryStream;
        }

        [Fact]
        public void SaveProjectWithOutErrors()
        {
            //var projectContext = new DocumentRepositoryNeo4jContext();

            var neo4JUri = new Uri("http://157.56.178.104:7474/db/nuldocumentsdev/");
            var neo4JUsername = "neo4j";
            var neo4JPassword = "admin";
            var graphClient = new GraphClient(neo4JUri, neo4JUsername, neo4JPassword);


            //Neo4j.Driver.IDriver driver = Neo4j.Driver.GraphDatabase.Driver("neo4j://157.56.178.104:7474/db/neo4j", Neo4j.Driver.AuthTokens.Basic("neo4j", "admin"));

            graphClient.ConnectAsync();

            ProjectRepositoryImpl _neo4jProjectRepository = new ProjectRepositoryImpl(graphClient);

            DateTime currentDateTime = DateTime.Now;

            var project = new Project();
            project.ProjectID = Guid.NewGuid();
            project.CreatedDateTime = currentDateTime;
            project.LastModifiedDateTime = currentDateTime;
            project.CreatedByUserID = 1;
            project.LastModifiedByUserID = 1;
            project.IsActive = true;
            project.Name = "ApplicationDocument";
            project.Description = "Application Document";
            project.PhysicalPath = "ApplicationDocument";
            project.StorageKey = "ApplicationDocument";
            project.IsInherit = false;
            project.ProjectTypeID = 1;

            _neo4jProjectRepository.SaveProject(project);

        }

        [Fact]
        public  void GraphConnectionTest()
        {


            // var graphClient = new GraphClient(new Uri("http://neo4j:admin@157.56.178.104/7474/db/neo4j"));

            // try
            // {
            //     await graphClient.ConnectAsync();
            // }
            // // ReSharper disable EmptyGeneralCatchClause
            // catch (NotImplementedException)
            // {
            //     // This will fail because we're not giving it the right
            //     // HTTP response, but we only care about the request for now
            // }

            const string username = "neo4j";
            const string password = "admin";
            var expectedHeader = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password)));

            var graphClient = new GraphClient(new Uri("http://157.56.178.104/7474/db/neo4j"), username, password);
            var httpClient = (HttpClientWrapper) graphClient.ExecutionConfiguration.HttpClient;

            Assert.Equal(expectedHeader, httpClient.Username + httpClient.Password);

            // var httpClient = Substitute.For<IHttpClient>();
            // httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Throws(new NotImplementedException());

            // var graphClient = new GraphClient(new Uri("http://admin:sectret@157.56.178.104/7474/db/neo4j"), httpClient);

            // try
            // {
            //     await graphClient.ConnectAsync();
            // }
            //     // ReSharper disable EmptyGeneralCatchClause
            // catch (NotImplementedException)
            // {
            //     // This will fail because we're not giving it the right
            //     // HTTP response, but we only care about the request for now
            // }
            // // ReSharper restore EmptyGeneralCatchClause

            // var httpCall = httpClient.ReceivedCalls().Last();
            // var httpRequest = (HttpRequestMessage) httpCall.GetArguments()[0];

            // Assert.Equal("Basic", httpRequest.Headers.Authorization.Scheme, StringComparer.OrdinalIgnoreCase);
            // Assert.Equal("dXNlcm5hbWU6cGFzc3dvcmQ=", httpRequest.Headers.Authorization.Parameter, StringComparer.OrdinalIgnoreCase);
        }
        

    }
}
