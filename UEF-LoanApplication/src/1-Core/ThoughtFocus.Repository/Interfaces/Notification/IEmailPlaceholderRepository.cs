using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.DataAccess.Models.Admin;
using ThoughtFocus.DataAccess.Models.Application;

namespace ThoughtFocus.Repository.Interfaces.Notification
{
    public interface IEmailPlaceholderRepository : IEFApplicationBaseRepository<EmailTemplatePlaceholders>
    {
        
        ThoughtFocus.DataAccess.Models.Contact.Contact GetContactPlaceholderData(long ContactID);
        ProgramInvitation GetProgramPlaceholderData(long ProgramInvitationID);
        BusinessEntity GetBusinessEntityPlaceholderData(long businessID);
        List<ThoughtFocus.DataAccess.Models.Contact.Contact> GetContactDetailsByRole(string roleName);
        List<ThoughtFocus.DataAccess.Models.Contact.BusinessUser> GetBorrowerDetails(long applicationID);
        List<ThoughtFocus.DataAccess.Models.User.User> GetNULTreasuryDetails();
        LoanApplication GetLoanApplicationDetails(long loanApplicationID);  
        List<ThoughtFocus.DataAccess.Models.User.User> GetControllerDetails();
    }
}
