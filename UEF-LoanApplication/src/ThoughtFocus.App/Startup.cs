
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ThoughtFocus.DataAccess;
using ThoughtFocus.Service.ModelsMapping;
using ThoughtFocus.Service.Impl;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.Repository.Interfaces.Contact;
using ThoughtFocus.Repository.Impl.Contact;
using ThoughtFocus.Repository.Interfaces.ContactManagement;
using ThoughtFocus.Repository.Impl.ContactManagement;
using ThoughtFocus.Repository.Interfaces.Master;
using ThoughtFocus.Repository.Interfaces.Admin;
using ThoughtFocus.Repository.Impl.Master;
using ThoughtFocus.Repository.Interfaces.User;
using ThoughtFocus.Repository.Interfaces.Audit;
using ThoughtFocus.Repository.Impl.User;
using ThoughtFocus.Repository.Impl.Audit;
using ThoughtFocus.Business.Interfaces.Contact;
using ThoughtFocus.Business.Impl.Contact;
using ThoughtFocus.Business.Interfaces.User;
using ThoughtFocus.Business.Impl.User;
using ThoughtFocus.App.JWTTokenHelper;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Common.WorkFlowDataAccess;
using ThoughtFocus.Business.Impl.Master;
using ThoughtFocus.Business.Interfaces.Master;
using ThoughtFocus.RoleProvider.Impl;
using ThoughtFocus.RoleProvider.Interfaces;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.Repository.Impl.Application;
using ThoughtFocus.Business.Interfaces.Application;
using ThoughtFocus.Business.Impl.Application;
using ThoughtFocus.DocumentManager.Interfaces;
using ThoughtFocus.DocumentManager.Impl;
using ThoughtFocus.Domain;
using Neo4j.Driver;
using ThoughtFocus.DocumentRepository.Core.Impl;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using Neo4jClient;
using System;
using ThoughtFocus.Repository.Interfaces.FundingSource;
using ThoughtFocus.Repository.Impl.FundingSource;
using ThoughtFocus.Business.Impl.FundSource;
using ThoughtFocus.Business.Interfaces.FundSource;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Workflow;
using ThoughtFocus.Common.Utilities;
using ThoughtFocus.Repository.Impl.Admin;
using ThoughtFocus.Services.Interfaces;
using ThoughtFocus.Services.impl;
using ThoughtFocus.Business.Interfaces.EmailTemplate;
using ThoughtFocus.Business.Impl.EmailTemplate;
using ThoughtFocus.Repository.Interfaces.Notification;
using ThoughtFocus.Repository.Impl.Notification;
using ThoughtFocusRepository.Interfaces.Master;
using ThoughtFocus.Business.Interfaces;

using Microsoft.AspNetCore.Http;
using ThoughtFocus.DocumentRepository.AzureBlobStorage;
using ThoughtFocus.Common.WorkFlowRepository.impl;
using ThoughtFocus.Common.WorkFlowRepository.Interface;
using ThoughtFocus.Repository.Impl.SMS;
using ThoughtFocus.Business.Interfaces.SMS;
using ThoughtFocus.Business.Impl.SMS;
using ThoughtFocus.Repository.Interfaces.SMS;
using ThoughtFocus.DocumentManager.Impl.CleanUpService;

namespace ThoughtFocus.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseLazyLoadingProxies()
                .UseSqlServer(Configuration.GetConnectionString("AppDBConnection"),
                            sqlServerOptionsAction: sqlOptions =>
                            {
                                sqlOptions.EnableRetryOnFailure();

                            });
            });

            services.AddDbContext<ApplicationAuditDBContext>(options =>
            {
                options.UseLazyLoadingProxies()
                   .UseSqlServer(Configuration.GetConnectionString("AppDBConnection"),
                               sqlServerOptionsAction: sqlOptions =>
                               { sqlOptions.EnableRetryOnFailure(); });
            });

            services.AddDbContext<WorkFlowContext>(options =>
            {
                options.UseLazyLoadingProxies()
                   .UseSqlServer(Configuration.GetConnectionString("AppDBConnection"),
                               sqlServerOptionsAction: sqlOptions =>
                               { sqlOptions.EnableRetryOnFailure(); });
            });

            services.AddCors();

            services.AddAutoMapper(typeof(ModelMappingConfiguration));
            services.AddControllersWithViews();

            //Neo4j
            var neo4jClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "admin");
            neo4jClient.ConnectAsync();
            services.AddSingleton<IGraphClient>(neo4jClient);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<SendEmail>();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            // SMSSettings
           // services.AddTransient<SendSMS>();
            services.Configure<SMSSettings>(Configuration.GetSection("SMSSettings"));

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.Configure<SqlConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            services.Configure<DocumentStorageConfiguration>(Configuration.GetSection("DocumentStorageConfiguration"));

            services.AddScoped<AuthorizeAttribute>();

            //Contact Repository
            services.AddScoped<IContactInvitationInfoRepository, ContactInvitationInfoRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

            //BusinessEntity Repository
            services.AddScoped<IBusinessEntityRepository, BusinessEntityRepository>();

            //Business Contact Repository
            services.AddScoped<IBusinessContactRepository, BusinessContactRepository>();

            //Program Invitation Repository
            services.AddScoped<IProgramInvitationRepository, ProgramInvitationRepository>();

            //Notifications mode
            services.AddScoped<INotificationModeRepository, NotificationModeRepository>();

            //Program Invitee Repository
            services.AddScoped<IProgramInviteeRepository, ProgramInviteeRepository>();

            

            // CiviCRMDataExport Log Repository
            services.AddScoped<ICiviCRMDataExportLogRepository, CiviCRMDataExportLogRepository>();

            //Master Repository
            services.AddScoped<IAccountStatusRepository, AccountStatusRepositoryImpl>();
            services.AddScoped<ICountryRepository, CountryRepositoryImpl>();
            services.AddScoped<IMenuRepository, MenuRepositoryImpl>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepositoryImpl>();
            services.AddScoped<IRoleRepository, RoleRepositoryImpl>();
            services.AddScoped<ISalutationRepository, SalutationRepositoryImpl>();
            services.AddScoped<IStateRepository, StateRepositoryImpl>();
            services.AddScoped<IRaceRepository, RaceRepositoryImpl>();
            services.AddScoped<IApplicationStatusRepository, ApplicationStatusRepositoryImpl>();
            services.AddScoped<IApplicationTypeRepository, ApplicationTypeRepositoryImpl>();
            services.AddScoped<IEthnicityRepository, EthnicityRepositoryImpl>();
            services.AddScoped<IGenderRepository, GenderRepositoryImpl>();
            services.AddScoped<IVeteranRepository, VeteranRepositoryImpl>();
            services.AddScoped<IAffiliateRepository, AffiliateRepositoryImpl>();
            services.AddScoped<IBusinessOwnerRepository, BusinessOwnerRepository>();

            services.AddScoped<IBusinessOwnerMasterRepository, BusinessOwnerMasterRepository>();
            services.AddScoped<IBusinessTypeRepository, BusinessTypeRepositoryImpl>();
            services.AddScoped<IBusinessRoleRepository, BusinessRoleRepositoryImpl>();
            services.AddScoped<IIndustryTypeRepository, IndustryTypeRepositoryImpl>();
            services.AddScoped<IFundingTypeRepository, FundingTypeRepositoryImpl>();
            services.AddScoped<IAgreementRepository, AgreementRepositoryImpl>();
            services.AddScoped<IHelpfulGuideTemplateRepository, HelpfulGuideTemplateRepositoryImpl>();
            services.AddScoped<INotificationModeRepositoryImpl, NotificationModeRepositoryImpl>();



            services.AddScoped<IGenaralOptionRepository, GenaralOptionRepository>();
            //User Repository
            services.AddScoped<IRUserRoleRepository, RUserRoleRepositoryImpl>();
            services.AddScoped<IUserAccountActivationInfoRepository, UserAccountActivationInfoRepositoryImpl>();
            services.AddScoped<IUserMgmtRepository, UserMgmtRepositoryImpl>();
            services.AddScoped<IUserPasswordResetInfoRepository, UserPasswordResetInfoRepositoryImpl>();
            services.AddScoped<IUserProfileLoginInfoRepository, UserProfileLoginInfoRepositoryImpl>();
            services.AddScoped<IUserRepository, UserRepositoryImpl>();
            services.AddScoped<IUserSecurityQuestionInfoRepository, UserSecurityQuestionInfoRepositoryImpl>();

            //Application Repository
            services.AddScoped<ILoanApplicantRepository, LoanApplicantRepository>();
            services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
            services.AddScoped<ILoanBusinessDetailRepository, LoanBusinessDetailRepository>();

            services.AddScoped<ILoanBusinessDetailMasterRepository, LoanBusinessDetailMasterRepository>();
            services.AddScoped<IFundingApplicationRepository, FundingApplicationRepository>();
            services.AddScoped<IQuestionResponseRepository, QuestionResponseRepository>();

            //Document Repository
            services.AddScoped<IApplicationDocumentRepository, ApplicationDocumentRepository>();
            services.AddScoped<IUserAccountActivationInfoRepository, UserAccountActivationInfoRepositoryImpl>();
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepositoryImpl>();

            //Funding Source Repository
            services.AddScoped<IFundingSourceRepository, FundingSourceRepository>();
            services.AddScoped<IFundUtilizationRepository, FundUtilizationRepository>();
            services.AddScoped<IFundingEntityRepository, FundingEntityRepository>();

            //Question Repository
            services.AddScoped<IQuestionsRepository, QuestionsRepository>();

            //Funding Source Business
            services.AddScoped<IFundingSource, FundingSource>();

            services.AddScoped<ISessionDetailsRepository, AuditSessionRepository>();
            //Contact Business 
            services.AddTransient<IContact, ContactImpl>();

            //Application Service
            services.AddTransient<ILoanApplication, LoanApplicationImpl>();

            //Account Business
            services.AddTransient<IUserAccount, UserAccountImpl>();

            //ListRoleBusiness
            services.AddTransient<IListRole, ListRoleImpl>();
            services.AddTransient<IViewRole, ViewRoleImpl>();

            //Document Business
            services.AddTransient<IDocFileUploader, DocFileUploader>();
            services.AddTransient<IDocumentUploadManager, DocumentUploadManager>();
            services.AddTransient<IDocumentVisitor, DocumentUploadVisitor>();
            services.AddTransient<IFileManager, FileManager>();

            //Notification Repository 
            services.AddScoped<IActivityNotificationRepository, ActivityNotificationRepository>();
            services.AddScoped<IEmailNotificationHeaderFooterRepository, EmailNotificationHeaderFooterRepository>();
            services.AddScoped<IEmailNotificationLogRepository, EmailNotificationLogRepository>();
            services.AddScoped<IEmailPlaceholderRepository, EmailPlaceholderRepository>();
            services.AddScoped<INotificationRecipientEmailAddressRepository, NotificationRecipientEmailAddressRepository>();
            services.AddScoped<INotificationRecipientsRepository, NotificationRecipientsRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationModeRepository, NotificationModeRepository>();

            //Notification service 
            services.AddTransient<INotificationService, NotificationService>();

            //Notification Business 
            services.AddTransient<IEmailManager, EmailManager>();
            services.AddTransient<IEmailTemplateHeaderFooter, EmailTemplateHeaderFooterImpl>();
            services.AddTransient<IEmailTemplateManager, EmailTemplateManager>();
            services.AddTransient<IPlaceHoldersManager, PlaceHoldersManager>();
            services.AddTransient<IPostEmailActionManager, PostEmailActionManager>();
            services.AddTransient<IPreEmailConditionManager, PreEmailConditionManager>();

            //SMS Manager
            services.AddTransient<ISMSManager,SMSManager>();
            services.AddTransient<ISendSMS, SendSMS>();


            services.AddScoped<DocumentRepository.Repository.Core.IFileExtensionTypeRepository, DocumentRepository.Repository.Neo4j.Impl.Master.FileExtensionRepository>();
            services.AddScoped<DocumentRepository.StorageService.IStorageService, BlobStorageService>();
            services.AddScoped<DocumentRepository.Repository.Core.IDocumentRepository, DocumentRepository.Repository.Neo4j.Impl.DocumentRepositoryImpl>();
            services.AddScoped<DocumentRepository.Repository.Core.IDocumentTagRepository, DocumentRepository.Repository.Neo4j.Impl.DocumentTagRepository>();
            services.AddScoped<DocumentRepository.Repository.Core.IDocumentVersionHistoryRepository, DocumentRepository.Repository.Neo4j.Impl.DocumentVersionHistoryRepository>();
            services.AddScoped<DocumentRepository.Repository.Core.IActionLogRepository, DocumentRepository.Repository.Neo4j.Impl.Action.ActionLogRepository>();
            services.AddScoped<DocumentRepository.Repository.Core.ISharedDocumentLogRepository, DocumentRepository.Repository.Neo4j.Impl.Action.SharedDocumentLogRepository>();
            services.AddScoped<DocumentRepository.Repository.Core.IAccessRoleRepository, DocumentRepository.Repository.Neo4j.Impl.AccessRoleRepository>();
            services.AddScoped<DocumentRepository.Repository.Core.IFileExtensionCategoryRepository, DocumentRepository.Repository.Neo4j.Impl.Master.FileExtensionCategoryRepository>();
            services.AddScoped<DocumentRepository.Repository.Core.IGroupRepository, DocumentRepository.Repository.Neo4j.Impl.GroupRepository>();
            services.AddScoped<DocumentRepository.Repository.Core.ITagRepository, DocumentRepository.Repository.Neo4j.Impl.Master.TagRepository>();
            services.AddScoped<DocumentRepository.Repository.Core.ITagTypeRepository, DocumentRepository.Repository.Neo4j.Impl.Master.TagTypeRepository>();
            services.AddScoped<DocumentRepository.Repository.Core.ITagValueRepository, DocumentRepository.Repository.Neo4j.Impl.Master.TagValueRepository>();
            services.AddScoped<DocumentRepository.Repository.Core.IProjectRepository, DocumentRepository.Repository.Neo4j.Impl.ProjectRepositoryImpl>();
            services.AddScoped<DocumentRepository.Repository.Core.IUserGroupRepository, DocumentRepository.Repository.Neo4j.Impl.User.UserGroupRepository>();
            services.AddScoped<DocumentRepository.Repository.Core.IUserRepository, DocumentRepository.Repository.Neo4j.Impl.User.UserRepository>();

            //SMS repository
            services.AddScoped<ISMSNotificationTemplateRepository,SMSNotificationTemplateRepository>();

            services.AddTransient<IAccessRoleManager, AccessRoleManager>();
            services.AddTransient<IActionLogger, ActionLogger>();
            services.AddTransient<IAuthorizer, Authorizer>();
            services.AddTransient<IAuthorizerStrategy, OwnerAuthorizer>();
            services.AddTransient<IAuthorizerStrategy, UserAuthorizer>();
            services.AddTransient<IAuthorizerStrategy, GroupAuthorizer>();
            services.AddTransient<IDistributionManager, DistributionManager>();
            services.AddTransient<IDocumentDownloader, DocumentDownloader>();
            services.AddTransient<IDocumentInformationProvider, DocumentInformationProvider>();
            services.AddTransient<IDocumentUploader, DocumentUploader>();
            services.AddTransient<IDocumentSeeker, DocumentSeeker>();
            services.AddTransient<IDocumentTagValueValidator, DocumentTagValueValidator>();
            services.AddTransient<ILockManager, LockManager>();
            services.AddTransient<ITagManager, TagManager>();
            services.AddTransient<IVersionManager, VersionManager>();
            services.AddTransient<IGroupManager, GroupManager>();
            services.AddTransient<IProjectManager, ProjectManager>();
            services.AddTransient<IUserManager, UserManager>();

            //Contact Service
            services.AddTransient<IContactService, ContactService>();

            //Business Entity Service
            services.AddTransient<IAdminService, AdminService>();

            //Account Service
            services.AddTransient<IAccountService, AccountServiceImpl>();

            //Master Service
            services.AddTransient<IMasterService, MasterServiceImpl>();

            //Application Service
            services.AddTransient<IApplicationService, ApplicationServiceImpl>();

            //Application Service
            services.AddTransient<IApplicationService, ApplicationServiceImpl>();
            services.AddTransient<ILoanApplicationAgreementDetailRepository, LoanApplicationAgreementDetailRepository>();
            //Account Controller
            services.AddTransient<IUserLoginService, UserLoginServiceImpl>();
            //Document Service
            services.AddTransient<IDocumentService, DocumentServiceImpl>();
            services.AddTransient<IApplicationDocumentFacade, ApplicationDocumentFacade>();

            //Funding Service
            services.AddTransient<IFundingSourceService, FundingSourceService>();
            services.AddTransient<IUrbanLeagueAffiliateRepository, UrbanLeagueAffiliateRepositoryImpl>();

            services.AddScoped<IWorkflowProcessTransitionHistoryRepository, WorkflowProcessTransitionHistoryRepository>();

            //Dashboard Service
            services.AddTransient<IDashboardService, DashboardServiceImpl>();

            services.AddTransient<ICleanUpApplicationDocumentRepository, CleanUpApplicationDocumentRepository>();
            //SMSNotificationService
            //services.AddTransient<ISMSNotificationService,SMSNotificationService>();

            services.AddScoped<WorkflowInit>();
            services.AddScoped<WorkflowRole>();
            services.AddScoped<WorkflowRule>();
            services.AddScoped<WorkflowActions>();
            services.AddScoped<LoanApplicationRepository>();
            services.AddScoped<ApplicationStatusRepositoryImpl>();
            services.AddScoped<ProgramInvitationRepository>();
            services.AddScoped<ProgramInviteeRepository>();
            services.AddScoped<EmailTemplateManager>();
            services.AddScoped<FundUtilizationRepository>();
            services.AddScoped<LoanApplicationCommentRepository>();
            services.AddScoped<LoanApplicationAgreementDetailRepository>();
            services.AddScoped<ProgramInviteeRepository>();
            services.AddScoped<NotificationModeRepository>();
            services.AddTransient<CleanUpProcessingService>();
            services.AddSingleton<SqlConnectionStrings>(new SqlConnectionStrings()
            {
                AppDBConnection = Configuration.GetConnectionString("AppDBConnection")
            });

            var serviceProvider = services.BuildServiceProvider();
            
                DependencyHelper.WorkflowInit = serviceProvider.GetService<WorkflowInit>();
                DependencyHelper.WorkflowRole = serviceProvider.GetService<WorkflowRole>();
                DependencyHelper.WorkflowRule = serviceProvider.GetService<WorkflowRule>();
                DependencyHelper.LoanApplicationRepository = serviceProvider.GetService<LoanApplicationRepository>();
                DependencyHelper.ApplicationStatusRepository = serviceProvider.GetService<ApplicationStatusRepositoryImpl>();
                DependencyHelper.EmailTemplateManager = serviceProvider.GetService<EmailTemplateManager>();
                DependencyHelper.SqlConnectionStrings = serviceProvider.GetService<SqlConnectionStrings>();
                DependencyHelper.ProgramInvitationRepository = serviceProvider.GetService<ProgramInvitationRepository>();
                DependencyHelper.FundUtilizationRepository = serviceProvider.GetService<FundUtilizationRepository>();
                DependencyHelper.LoanApplicationCommentRepository = serviceProvider.GetService<LoanApplicationCommentRepository>();
                DependencyHelper.LoanApplicationAgreementDetailRepository = serviceProvider.GetService<LoanApplicationAgreementDetailRepository>();
                //DependencyHelper.ProgramInviteeRepository = serviceProvider.GetService<ProgramInviteeRepository>();
                //DependencyHelper.NotificationModeRepository = serviceProvider.GetService<NotificationModeRepository>();

                DependencyHelper.WorkflowActions =  serviceProvider.GetService<WorkflowActions>();
            
            // Enable Swagger   
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ThoughtFocus Application",
                    Version = "v1",
                    Description = "ThoughtFocus Application"
                });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Thought Focus Application");
            });

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
                  {
                      endpoints.MapControllers();
                  });


        }
    }
}
