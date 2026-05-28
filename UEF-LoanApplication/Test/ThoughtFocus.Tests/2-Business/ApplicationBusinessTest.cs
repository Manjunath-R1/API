// using ThoughtFocus.Business.Impl.Contact;
// using Xunit;
// using Microsoft.Extensions.Logging;
// using ThoughtFocus.Business.Interfaces.Application;
// using ThoughtFocus.Domain;
// using ThoughtFocus.Business.Impl.Application;
// using ThoughtFocus.Domain.Params;
// using ThoughtFocus.Common.Exceptions.BusinessException;
// using ThoughtFocus.Domain.User;
// using ThoughtFocus.Repository.Interfaces.Application;
// using Moq;  
// using ThoughtFocus.Common.Exceptions;
// using ThoughtFocus.Constants;
// using ThoughtFocus.DataAccess;
// using ThoughtFocus.Domain.Response;
// using System.Linq; 
// using ThoughtFocus.Domain.Common;
// using System.Collections.Generic;
// using ThoughtFocus.UnitTests.DataProvider;


// namespace ThoughtFocus.UnitTests.Business
// {
//     public class ApplicationBusinessTest
//     {
//         #region Fields
//         public readonly ILoanApplication mockApplicationBusiness;
//         ApplicationDBContext context;

//         #endregion Fields


//           [Fact]
//         public void CanGetApplicationList()
//         {   
                
//             var db = new FakeAppDBContext();
//             context = db.CreateContextForInMemory();
            
//             var mockLogger = new Mock<ILogger<LoanApplicationImpl>>();
//             // var mockMapper = new Mock<IMapper>();  
//             var mockLoanApplicationRepository = new Mock<ILoanApplicationRepository>();
//             var mockLoanApplicationBuisness = new Mock<ILoanApplication>();
          
//             //var applicationListResponse = new Mock<ApplicationListResponse>().Object;

//             var LoanApplication = new Mock<IQueryable<ThoughtFocus.DataAccess.Models.Application.LoanApplication>>().Object;
//             var PageFilterEntity = new Mock<PageFilterEntity>().Object;
//             var userSessionEntity = new Mock<UserSessionEntity>().Object;
//             var CommonCreationParam = new Mock<CommonCreationParam>(CommonCreationStatus.Success, MessageConstants.Success, null).Object;

//             mockLoanApplicationRepository.Setup(x => x.GetLoanApplications()).Returns(LoanApplication);

//             PageFilterEntity pageFilterEntity = new PageFilterEntity();
//             List<FilterParameter> FilterParameters = new List<FilterParameter>();
//             FilterParameter FilterParameter = new FilterParameter();
//             FilterParameter.Key = "BusinessName";
//             FilterParameters.Add(FilterParameter);
//             pageFilterEntity.FilterParameters = FilterParameters;

//             var mockApplicationBusiness = new LoanApplicationImpl(mockLogger.Object, 
//                                                 mockLoanApplicationRepository.Object); 

//             var applicationListResponse = mockApplicationBusiness.GetAllLoanApplicationInformation(pageFilterEntity);

//             Assert.Equal("2",applicationListResponse.ApplicationPageResultEntity.TotalRecordCount.ToString());
                                    



            
//         }
//     }
    
    
// }