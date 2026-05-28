using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ThoughtFocus.DocumentRepository.Repository.Neo4j;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Repository.Core;
using Neo4jClient;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain.Response;
using Neo4jClient.Cypher;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl
{
    public class DocumentRepositoryImpl : AbstractNeo4jBaseRepository<Document>, IDocumentRepository
    {
        private readonly ILogger<DocumentRepositoryImpl> _logger;
        private readonly IGraphClient _graphClient;

        #region Constructors

        public DocumentRepositoryImpl(IGraphClient graphClient, ILogger<DocumentRepositoryImpl> logger)
        {
            _graphClient = graphClient;
            _logger = logger;
        }

        #endregion Constructors

        public void SaveDocuments(List<DataAccess.Neo4j.Document> documents)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                _graphClient.Cypher
                    .Unwind(documents, "document")
                    .Merge("(d:Document {DocumentID : document.DocumentID})")
                    .Set("d = document")
                    .ExecuteWithoutResultsAsync();

                _graphClient.Cypher.CreateUniqueConstraint("d:Document", "d.DocumentID").ExecuteWithoutResultsAsync();

                foreach (var document in documents)
                    CreateDocumentFileExtensionMapping(document);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }
        public void SaveDocument(Document doc)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                if (doc != null)
                {
                   var query= _graphClient
                        .Cypher
                        .Merge("(document:Document { DocumentID: {id} })")
                        .OnCreate().Set("document = {document}")
                        .OnMatch().Set("document = {document}")
                        .WithParams(new
                        {
                            id = doc.DocumentID,
                            document = doc
                        })
                        ;

                   query.ExecuteWithoutResultsAsync();
						CreateDocumentFileExtensionMapping(doc);           
                }

            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }
        public void SaveDocumentProjectMapping(Document document)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                if (document != null && _graphClient.IsConnected)
                {
                    foreach (Project documentProject in document.Projects)
                    {
                        //Check document is already exists in any project
                        var checkProject = _graphClient.Cypher
                            .Match("(doc:Document)-[relation:BELONGS_TO]->(project:Project)")
                            .Where("doc.DocumentID ='" + document.DocumentID + "'")
                            .Return(project => project.As<Project>());
                        List<Project> projects = checkProject.ResultsAsync.Result.ToList();


                        var query = _graphClient.Cypher
                                                .Match("(doc:Document)", "(project:Project)")
                                                .Where("doc.DocumentID ='" + document.DocumentID + "'")
                                                .AndWhere((Project project) => project.ProjectID.ToString() == documentProject.ProjectID.ToString())
                                                .CreateUnique("(doc)-[r:BELONGS_TO]->(project)");

                        //if document is not exists in any project the create IS_PRIMARY relationship
                        if (projects == null || (projects != null && projects.Count == 0))
                        {
                            query = query.CreateUnique("(doc)-[relation:IS_PRIMARY]->(project)");
                        }

                        query.ExecuteWithoutResultsAsync();
                    }

                }

            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }
        public void CreateCurrentVersion()
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                if (_graphClient.IsConnected)
                {
                    _graphClient.Cypher.Merge("(currentVersion:CurrentVersion { Name : 'Current Version' })").ExecuteWithoutResultsAsync();
                }

            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }
        public void CreateCurrentVersionMapping(Document currentVersiondocument)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                if (currentVersiondocument != null && _graphClient.IsConnected)
                {

                    /*-------------------Fetching Existing Relation-----------------------*/
                    List<Document> currentVersions = _graphClient.Cypher
                                       .Match("(document:Document)", "(currentVersion:CurrentVersion)")
                                       .Where((Document document) => document.Key.ToString() == currentVersiondocument.Key.ToString() && document.DocumentID != currentVersiondocument.DocumentID)
                                       .Return(document => document.As<Document>()).ResultsAsync.Result.ToList();

                    if (currentVersions != null && currentVersions.Count > 0)
                    {
                        /*-------------------Deleting previous current version relationship-----------------------*/
                        foreach (Document currentVersion in currentVersions)
                        {
                            _graphClient.Cypher.Match("(document:Document)-[r:IS_AT]->(currentVersion:CurrentVersion)")
                                               .Where((Document document) => document.DocumentID.ToString() == currentVersion.DocumentID.ToString())
                                                .Delete("r");
                        }

                        if (currentVersions.Count > 1)
                            _logger.LogError("Found more than one document with same key marked as latest version. Document Key :" + currentVersiondocument);
                    }

                    /*-------------------Creating Current version relation-----------------------*/
                    var query = _graphClient.Cypher
                                       .Match("(document:Document)", "(currentVersion:CurrentVersion)")
                                       .Where((Document document) => document.DocumentID.ToString() == currentVersiondocument.DocumentID.ToString())
                                       .CreateUnique("(document)-[r:IS_AT]->(currentVersion)");
                    query.ExecuteWithoutResultsAsync();

                }

            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }
        public void CreateCurrentVersionMapping(List<Document> currentVersiondocument)
        {
             if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();
            var documentList = currentVersiondocument.Select(x => x.DocumentID).ToList();
            var query = _graphClient.Cypher
                                      .Unwind(documentList, "documentversion")
                                      .Match("(document:Document)", "(currentVersion:CurrentVersion)")
                                      .Where("document.DocumentID = documentversion")
                                      .CreateUnique("(document)-[r:IS_AT]->(currentVersion)");
            query.ExecuteWithoutResultsAsync();
        }
        public void CreateDocumentFileExtensionMapping(Document document)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                if (document != null && _graphClient.IsConnected)
                {
                    var query = _graphClient.Cypher
                                   .Match("(doc:Document)", "(fileExtension:FileExtensionType)")
                                   .Where((Document doc) => doc.DocumentID == document.DocumentID)
                                   .AndWhere((FileExtensionType fileExtension) => fileExtension.FileExtensionTypeID.ToString() == document.FileExtensionTypeID.ToString())
                                   .CreateUnique("(doc)-[r:HAS]->(fileExtension)");
                    query.ExecuteWithoutResultsAsync();
                }

            }
            catch (NeoException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }
        public List<Document> GetDocumentsByParentId(Guid parentId, Guid userID)
        {
            try
            {
                 if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();
                var documentsQuery = _graphClient.Cypher
                                        .Match(string.Format("(project:Project)<-[relation: BELONGS_TO]-(document:Document)"))
                                        .Where((Project project) => project.ProjectID == parentId)
                                        .AndWhere((Document document) => document.IsActive == true && document.IsInherit == true)
                                        .Return((document, project) => new
                                        {
                                            Document = document.As<DataAccess.Neo4j.Document>(),
                                            Project = project.As<DataAccess.Neo4j.Project>()
                                        })
                                        .Union()
                                        .Match(string.Format("(project:Project)<-[relation: BELONGS_TO]-(document:Document)<-[r:IS_OWNER]-(repositoryUser:RepositoryUser)"))
                                        .Where((Project project) => project.ProjectID == parentId)
                                        .AndWhere((Document document) => document.IsActive == true && document.IsInherit == false)
                                        .AndWhere((RepositoryUser repositoryUser) => repositoryUser.UserGuID == userID)
                                        .Return((document, project) => new
                                        {
                                            Document = document.As<DataAccess.Neo4j.Document>(),
                                            Project = project.As<DataAccess.Neo4j.Project>()
                                        })
                                        .Union()
                                        .Match(string.Format("(project:Project)<-[relation: BELONGS_TO]-(document:Document)<-[r1:CAN_ACCESS]-(repositoryUser:RepositoryUser)"))
                                        .Where((Project project) => project.ProjectID == parentId)
                                        .AndWhere((Document document) => document.IsActive == true && document.IsInherit == false)
                                        .AndWhere((RepositoryUser repositoryUser) => repositoryUser.UserGuID == userID)
                                        .With("r1, document, project")
                                        .Match("(accessRole:AccessRole{Name:r1.RoleName})" + "-[m:CONTAINS]->" + "(permission:Permission{Name:'View'})")
                                        .Return((document, project) => new
                                        {
                                            Document = document.As<DataAccess.Neo4j.Document>(),
                                            Project = project.As<DataAccess.Neo4j.Project>()
                                        })
                                        .Union()
                                        .Match("(repositoryUser:RepositoryUser)" + "-[r2:BELONGS_TO]->" + "(group:Group)")
                                        .Where((RepositoryUser repositoryUser) => repositoryUser.UserGuID == userID)
                                        .With("group")
                                        .Match("(group:Group{GroupID:group.GroupID})" + "-[r3:CAN_ACCESS]->" + "(document:Document)" + "-[relation: BELONGS_TO]->" + "(project:Project)")
                                        .Where((Project project) => project.ProjectID == parentId)
                                        .AndWhere((Document document) => document.IsActive == true && document.IsInherit == false)
                                        .With("r3,project,document")
                                        .Match("(accessRole:AccessRole{Name:r3.RoleName})" + "-[m:CONTAINS]->" + "(permission:Permission{Name:'View'})")
                                        .Return((document, project) => new
                                        {
                                            Document = document.As<DataAccess.Neo4j.Document>(),
                                            Project = project.As<DataAccess.Neo4j.Project>()
                                        });

                var documentCollection = documentsQuery.ResultsAsync.Result.Select(d =>
                    new Document
                    {
                        DocumentID = d.Document.DocumentID,
                        CreatedDateTime = d.Document.CreatedDateTime,
                        LastModifiedDateTime = d.Document.LastModifiedDateTime,
                        CreatedByUserID = d.Document.CreatedByUserID,
                        LastModifiedByUserID = d.Document.LastModifiedByUserID,
                        IsActive = d.Document.IsActive,
                        Name = d.Document.Name,
                        Path = d.Document.Path,
                        IsLocked = d.Document.IsLocked,
                        FileExtensionTypeID = d.Document.FileExtensionTypeID,
                        FileSize = d.Document.FileSize,
                        LastUploadedDate = d.Document.LastUploadedDate,
                        Projects = new List<Project> { new Project
                            {
                                ProjectID = d.Project.ProjectID
                            }
                        }
                    }).ToList();

                //var results = documentsQuery.Results.ToList();
                //var documentCollection = new List<dynamic>();
                //foreach (var result in results)
                //{
                //    documentCollection.Add(JsonConvert.DeserializeObject<dynamic>(result.Relation.Data));
                //}



                return documentCollection;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetDocumentsByParentId repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetDocumentsByParentId repository", ex);
            }
        }
        public FilterResponse SearchDocuments(FilterRequest filterRequest)
        {
            try
            {
                var criteria = _graphClient.Cypher
                   .Match("(document:Document)");

                var filters = new List<ICriteria>{
                    //new ByDocumentContent(),
                    new ByDocumentName(),
                    new ByDocumentNumber(),
                    new ByDocumentExtension(),
                    new OnDate(),
                    new ByTimeLine(),
                    new BetweenDates(),
                    new ByTags()
                };

                var whereAdded = true;

                criteria = criteria.Where<Document>(document => document.IsActive == true);

                foreach (var filter in filters.OrderBy(f => f.OrderIndex))
                {
                    criteria = filter.MeetCriteria(criteria, filterRequest.Filters, ref whereAdded);
                }

                if (filterRequest.Sort != null)
                {
                    if (filterRequest.Ascending)
                        criteria = criteria.OrderBy(filterRequest.Sort);
                    else
                        criteria = criteria.OrderByDescending(filterRequest.Sort);
                }
                var totalCount = criteria.Return((document) => document.Count()).ResultsAsync.Result.FirstOrDefault();

                criteria = criteria.Return((document, t) => document.As<Document>());
                criteria = ((ICypherFluentQuery<Document>)criteria).Skip(filterRequest.PageIndex).Limit(filterRequest.PageLength);

                var documents = ((ICypherFluentQuery<Document>)criteria).ResultsAsync.Result.ToList();

                return new FilterResponse
                {
                    Documents = documents,
                    TotalCount = totalCount
                };
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in SearchDocuments", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in SearchDocuments", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in SearchDocuments", ex);
            }
        }
        public Document GetDocumentById(Guid documentID)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();
                var documentsQuery = _graphClient.Cypher
                                     .Match(string.Format("(document:Document)"))
                                     .Where((Document document) => document.DocumentID.ToString() == documentID.ToString() && document.IsActive == true)
                                     .Return((document) => new
                                     {
                                         Document = document.As<DataAccess.Neo4j.Document>(),
                                         // Relation = relation.As<Node<string>>(),
                                     });

                var documentProjectRelation = documentsQuery.ResultsAsync.Result.Select(d =>
                    new Document
                    {
                        DocumentID = d.Document.DocumentID,
                        CreatedDateTime = d.Document.CreatedDateTime,
                        LastModifiedDateTime = d.Document.LastModifiedDateTime,
                        CreatedByUserID = d.Document.CreatedByUserID,
                        LastModifiedByUserID = d.Document.LastModifiedByUserID,
                        IsActive = d.Document.IsActive,
                        Name = d.Document.Name,
                        Path = d.Document.Path,
                        MajorVersion = d.Document.MajorVersion,
                        MinorVersion = d.Document.MinorVersion,
                        Number = d.Document.Number,
                        Key = d.Document.Key,
                        StorageKey = d.Document.StorageKey,
                        IsLocked = d.Document.IsLocked,
                        LockedByUserID = d.Document.LockedByUserID,
                        FileExtensionTypeID = d.Document.FileExtensionTypeID,
                        FileSize = d.Document.FileSize,
                        LastUploadedDate = d.Document.LastUploadedDate,
                        IsInherit=d.Document.IsInherit
                    }).FirstOrDefault();


                return documentProjectRelation;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetDocumentById repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetDocumentById repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetDocumentById repository", ex);
            }
        }
        public Document GetLastDocument()
        {
            try
            {
                 if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();
                var document = _graphClient.Cypher
                    .Match("(d:Document)")
                    .Return(d => d.As<Document>())
                    .OrderBy("d.CreatedDateTime DESC")
                    .Limit(1);
                var result = document.ResultsAsync.Result.FirstOrDefault();
                if (result == null)
                {
                    result = new Document();
                }

                return result;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetDocumentsByParentId repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetDocumentsByParentId repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetDocumentsByParentId repository", ex);
            }
        }
        public Document GetDocumentByNameAndProjectID(string FileName, Guid ProjectId)
        {
            try
            {
                 if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();
                var documentQuery = _graphClient.Cypher
                    .Match(string.Format("(d:Document)-[relation: BELONGS_TO]->(parent:Project)"))
                    .Where((Document d) => d.Name == FileName && d.IsActive == true)
                    .AndWhere((Project parent) => parent.ProjectID.ToString() == ProjectId.ToString())
                    .Return(d => d.As<Document>());

                var document = documentQuery.ResultsAsync.Result.FirstOrDefault();

                return document;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in ValidateDocument repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in ValidateDocument repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in ValidateDocument repository", ex);
            }
        }
        public Document GetDocument(Guid documentID, Guid ProjectId)
        {
            try
            {
                 if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var documentQuery = _graphClient.Cypher
                    .Match(string.Format("(d:Document)-[relation: BELONGS_TO]->(parent:Project)"))
                    .Where((Document d) => d.DocumentID == documentID && d.IsActive == true)
                    .AndWhere((Project parent) => parent.ProjectID.ToString() == ProjectId.ToString())
                    .Return(d => d.As<Document>());

                var document = documentQuery.ResultsAsync.Result.FirstOrDefault();

                return document;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetDocument repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetDocument repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetDocument repository", ex);
            }
        }
        public Document GetDocumentPrimaryProject(Guid documentID)
        {
            try
            {
                 if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var documentsQuery = _graphClient.Cypher
                                 .Match(string.Format("(document:Document)-[relation: BELONGS_TO]->(project:Project)"))
                                 .Match(string.Format("(document:Document)-[relation1: IS_PRIMARY]->(project:Project)"))
                                 .Where((Document document) => document.DocumentID.ToString() == documentID.ToString() && document.IsActive == true)
                                 .Return((document, project) => new
                                 {
                                     Document = document.As<DataAccess.Neo4j.Document>(),
                                     // Relation = relation.As<Node<string>>(),
                                     Project = project.As<DataAccess.Neo4j.Project>()
                                 });

                var documentProjectRelation = documentsQuery.ResultsAsync.Result.Select(d =>
                    new Document
                    {
                        DocumentID = d.Document.DocumentID,
                        CreatedDateTime = d.Document.CreatedDateTime,
                        LastModifiedDateTime = d.Document.LastModifiedDateTime,
                        CreatedByUserID = d.Document.CreatedByUserID,
                        LastModifiedByUserID = d.Document.LastModifiedByUserID,
                        IsActive = d.Document.IsActive,
                        Name = d.Document.Name,
                        Path = d.Document.Path,
                        MajorVersion = d.Document.MajorVersion,
                        MinorVersion = d.Document.MinorVersion,
                        Number = d.Document.Number,
                        Key = d.Document.Key,
                        StorageKey = d.Document.StorageKey,
                        IsLocked = d.Document.IsLocked,
                        LockedByUserID = d.Document.LockedByUserID,
                        FileExtensionTypeID = d.Document.FileExtensionTypeID,
                        FileSize = d.Document.FileSize,
                        LastUploadedDate = d.Document.LastUploadedDate,
                        Projects = new List<Project> { new Project
                            {
                                ProjectID = d.Project.ProjectID
                            }
                        }
                    }).FirstOrDefault();


                return documentProjectRelation;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetDocumentPrimaryProject repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetDocumentPrimaryProject repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetDocumentPrimaryProject repository", ex);
            }
        }
        public Document GetDocumentVersionById(Guid documentVersionID)
        {
            try
            {
                 if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var documentVersionHistoryQuery = _graphClient.Cypher
                   .Match("(d:DocumentVersionHistory)")
                   .Where((DocumentVersionHistory d) => d.DocumentVersionHistoryID == documentVersionID && d.IsActive == true)
                   .Return(d => d.As<DocumentVersionHistory>());

                var documentVersionHistory = documentVersionHistoryQuery.ResultsAsync.Result.FirstOrDefault();
                if (documentVersionHistory != null)
                {
                    var documentsQuery = _graphClient.Cypher
                                    .Match(string.Format("(document:Document)-[relation: BELONGS_TO]->(project:Project)"))
                                    .Where((Document document) => document.DocumentID == documentVersionHistory.DocumentID)
                                    .Return((document, project) => new
                                    {
                                        Document = document.As<DataAccess.Neo4j.Document>(),
                                        Project = project.As<DataAccess.Neo4j.Project>()
                                    });
                    Document parentdocument = documentsQuery.ResultsAsync.Result.Select(d =>
                        new Document
                        {
                            DocumentID = d.Document.DocumentID,
                            Number = d.Document.Number,
                            IsLocked = d.Document.IsLocked,
                            LockedByUserID = d.Document.LockedByUserID,
                            LastUploadedDate = d.Document.LastUploadedDate,
                            Projects = new List<Project> { new Project
                            {
                                ProjectID = d.Project.ProjectID
                            }
                        }
                        }).FirstOrDefault();

                    parentdocument.CreatedDateTime = documentVersionHistory.CreatedDateTime;
                    parentdocument.LastModifiedDateTime = documentVersionHistory.LastModifiedDateTime;
                    parentdocument.CreatedByUserID = documentVersionHistory.CreatedByUserID;
                    parentdocument.LastModifiedByUserID = documentVersionHistory.LastModifiedByUserID;
                    parentdocument.IsActive = documentVersionHistory.IsActive;
                    parentdocument.Name = documentVersionHistory.Name;
                    parentdocument.Path = documentVersionHistory.Path;
                    parentdocument.MajorVersion = documentVersionHistory.MajorVersion;
                    parentdocument.MinorVersion = documentVersionHistory.MinorVersion;
                    parentdocument.Key = documentVersionHistory.Key;
                    parentdocument.StorageKey = documentVersionHistory.StorageKey;
                    parentdocument.FileExtensionTypeID = documentVersionHistory.FileExtensionTypeID;
                    parentdocument.FileSize = documentVersionHistory.FileSize; 
                    return parentdocument;
                }
                return null;

            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetDocumentById repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetDocumentById repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetDocumentById repository", ex);
            }
        }
        
    }

}
