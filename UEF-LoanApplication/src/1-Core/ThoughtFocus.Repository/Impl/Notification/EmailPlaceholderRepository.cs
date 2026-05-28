 
using System;
using System.Collections.Generic; 
using System.Data.SqlClient;
using System.Linq;
using ThoughtFocus.DataAccess.Models; 
using ThoughtFocus.Repository.Interfaces.Notification;
using ThoughtFocus.Common.Exceptions; 
using System.Reflection; 
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.DataAccess;
using Microsoft.EntityFrameworkCore;
using ThoughtFocus.DataAccess.Models.Admin;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.Domain.Enumeration;

namespace ThoughtFocus.Repository.Impl.Notification
{
    public class EmailPlaceholderRepository : AbstractEFApplicationBaseRepository<EmailTemplatePlaceholders>, IEmailPlaceholderRepository
    {
        #region Fields

        private ApplicationDBContext _context;
         
        #endregion Fields

        #region Constructors

        public EmailPlaceholderRepository(ApplicationDBContext context)
            : base(context)
        {
            this._context = context;
        }

        #endregion Constructors

        public ThoughtFocus.DataAccess.Models.Contact.Contact GetContactPlaceholderData(long ContactID)
        {
             ThoughtFocus.DataAccess.Models.Contact.Contact  contact=null;
            try
            {   
                contact = _context.Contacts.FirstOrDefault(x => x.ContactID == ContactID && x.IsActive == true);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at EmailPlaceholderRepository-> GetContactPlaceholderData", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("DbUpdateConcurrencyException Exception at EmailPlaceholderRepository-> GetContactPlaceholderData", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception at EmailPlaceholderRepository-> GetContactPlaceholderData", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at EmailPlaceholderRepository-> GetContactPlaceholderData", ex);
            }
            return contact;
        }

        public ProgramInvitation GetProgramPlaceholderData(long ProgramInvitationID)
        {
             ProgramInvitation  programInvitation = null;
            try
            {   
                programInvitation = _context.ProgramInvitations.FirstOrDefault(x => x.ProgramInvitationID == ProgramInvitationID && x.IsActive == true);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at EmailPlaceholderRepository-> GetProgramPlaceholderData", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("DbUpdateConcurrencyException Exception at EmailPlaceholderRepository-> GetProgramPlaceholderData", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception at EmailPlaceholderRepository-> GetProgramPlaceholderData", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at EmailPlaceholderRepository-> GetProgramPlaceholderData", ex);
            }
            return programInvitation;
        }

        public BusinessEntity GetBusinessEntityPlaceholderData(long businessID)
        {
             BusinessEntity  businessEntity = null;
            try
            {   
                businessEntity = _context.BusinessEntity.FirstOrDefault(x => x.ID == businessID && x.IsActive == true);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at EmailPlaceholderRepository-> GetBusinessEntityPlaceholderData", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("DbUpdateConcurrencyException Exception at EmailPlaceholderRepository-> GetBusinessEntityPlaceholderData", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception at EmailPlaceholderRepository-> GetBusinessEntityPlaceholderData", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at EmailPlaceholderRepository-> GetBusinessEntityPlaceholderData", ex);
            }
            return businessEntity;
        }
        

         public List<ThoughtFocus.DataAccess.Models.Contact.Contact> GetContactDetailsByRole(string roleName)
        {
            List<ThoughtFocus.DataAccess.Models.Contact.Contact> Contacts = new List<ThoughtFocus.DataAccess.Models.Contact.Contact>();
            List<ThoughtFocus.DataAccess.Models.User.UserRole> UserRoles = new List<ThoughtFocus.DataAccess.Models.User.UserRole>(); 
            try
            {   
                Role role = _context.Roles.FirstOrDefault(x => x.RoleName == roleName && x.IsActive == true);

                if( role!= null)
                    UserRoles = _context.RUserRoles.Where(y => y.RoleID == role.RoleID && y.IsActive == true).ToList();

                foreach(var user in UserRoles)
                {
                    ThoughtFocus.DataAccess.Models.Contact.Contact contact = _context.Contacts.FirstOrDefault(x =>  x.IsActive == true && x.Users.UserID == user.UserID);

                    if(contact != null)
                    Contacts.Add(contact);
                }
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at EmailPlaceholderRepository-> GetContactPlaceholderData", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("DbUpdateConcurrencyException Exception at EmailPlaceholderRepository-> GetContactPlaceholderData", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception at EmailPlaceholderRepository-> GetContactPlaceholderData", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at EmailPlaceholderRepository-> GetContactPlaceholderData", ex);
            }
            return Contacts;
        }

         public List<ThoughtFocus.DataAccess.Models.Contact.BusinessUser> GetBorrowerDetails(long applicationID)
        {
            List<ThoughtFocus.DataAccess.Models.Contact.BusinessUser> BusinessUser = new List<ThoughtFocus.DataAccess.Models.Contact.BusinessUser>();
            try
            {   
                LoanApplication application = _context.LoanApplications.FirstOrDefault(x => x.LoanApplicationID == applicationID && x.IsActive == true);

                if(application != null)
                {
                    BusinessUser = _context.BusinessUsers.Where( x =>  x.IsActive == true && x.BusinessID == application.ProgramInvitation.BusinessID).ToList();
                }
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at EmailPlaceholderRepository-> GetBorrowerDetails", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("DbUpdateConcurrencyException Exception at EmailPlaceholderRepository-> GetBorrowerDetails", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception at EmailPlaceholderRepository-> GetBorrowerDetails", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at EmailPlaceholderRepository-> GetBorrowerDetails", ex);
            }
            return BusinessUser;
        }

        public List<ThoughtFocus.DataAccess.Models.User.User> GetNULTreasuryDetails()
        {
            List<ThoughtFocus.DataAccess.Models.User.User> User = new List<ThoughtFocus.DataAccess.Models.User.User>();
            try
            {   

                User = (from user in _context.Users
                        join userRole in _context.RUserRoles
                            on user.UserID equals userRole.UserID
                        where user.IsActive == true && 
                            userRole.IsActive == true && 
                            userRole.RoleID == (long)RoleIDEnumerations.NULTreasury
                        select user
                        ).ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at EmailPlaceholderRepository-> GetNULTreasuryDetails", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("DbUpdateConcurrencyException Exception at EmailPlaceholderRepository-> GetNULTreasuryDetails", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception at EmailPlaceholderRepository-> GetNULTreasuryDetails", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at EmailPlaceholderRepository-> GetNULTreasuryDetails", ex);
            }
            return User;
        }

        public LoanApplication GetLoanApplicationDetails(long loanApplicationID)
        {
            LoanApplication application = new LoanApplication();
            try
            {   
                application = _context.LoanApplications.FirstOrDefault(x => x.LoanApplicationID == loanApplicationID && x.IsActive == true);
               
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at EmailPlaceholderRepository-> GetLoanApplicationDetails", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("DbUpdateConcurrencyException Exception at EmailPlaceholderRepository-> GetLoanApplicationDetails", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception at EmailPlaceholderRepository-> GetLoanApplicationDetails", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at EmailPlaceholderRepository-> GetLoanApplicationDetails", ex);
            }
            return application;
        }

        public List<ThoughtFocus.DataAccess.Models.User.User> GetControllerDetails()
        {
            List<ThoughtFocus.DataAccess.Models.User.User> User = new List<ThoughtFocus.DataAccess.Models.User.User>();
            try
            {   

                User = (from user in _context.Users
                        join userRole in _context.RUserRoles
                            on user.UserID equals userRole.UserID
                        where user.IsActive == true && 
                            userRole.IsActive == true && 
                            userRole.RoleID == (long)RoleIDEnumerations.Controller
                        select user
                        ).ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at EmailPlaceholderRepository-> GetControllerDetails", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("DbUpdateConcurrencyException Exception at EmailPlaceholderRepository-> GetControllerDetails", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception at EmailPlaceholderRepository-> GetControllerDetails", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at EmailPlaceholderRepository-> GetControllerDetails", ex);
            }
            return User;
        }

      
        
    }
}
