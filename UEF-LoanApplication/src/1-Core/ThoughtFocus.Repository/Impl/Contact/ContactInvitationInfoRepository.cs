using System;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DataAccess;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.Repository.Interfaces.ContactManagement;

namespace ThoughtFocus.Repository.Impl.ContactManagement
{
 

    public class ContactInvitationInfoRepository : AbstractEFApplicationBaseRepository<ContactInvitationInfo>, IContactInvitationInfoRepository
    {

         #region Fields

        private ApplicationDBContext _Context;
       
        #endregion Fields
        #region Constructors

        public ContactInvitationInfoRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
        }       

        #endregion Constructors

         #region Methods

          public ContactInvitationInfo GetContactInvitationInfoByToken(string TokenID,long contactId)
        {
            try
             {
                var query = GetAll().OrderByDescending(o=>o.ContactInvitationInfoID).FirstOrDefault(x => x.TokenID == TokenID && x.ContactID==contactId);
                return query;
             }
             catch (SqlException ex)
             {
                 throw new RepositoryException("Exception in ContactInvitationInfoRepository-> GetContactInvitationInfoByToken", ex);
             }
             catch (DbUpdateException ex)
             {
                 throw new RepositoryException("Exception in ContactInvitationInfoRepository-> GetContactInvitationInfoByToken", ex);
             }
             catch (ObjectDisposedException ex)
             {
                 throw new RepositoryException("Exception in ContactInvitationInfoRepository-> GetContactInvitationInfoByToken", ex);
             }
             catch (Exception ex)
             {
                 throw new RepositoryException("Exception in ContactInvitationInfoRepository-> GetContactInvitationInfoByToken", ex);
             }
        }

        public void SaveOrUpdateContactInvitation(ContactInvitationInfo contactInvitationInfo, long? userID)
        {
            try
            {                 
                if (contactInvitationInfo.ContactInvitationInfoID > 0)
                {                    
                    _Context.Entry(contactInvitationInfo).State = EntityState.Modified;
                }                   
                else
                    _Context.ContactInvitationInfos.Add(contactInvitationInfo);              
                 
               this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in ContactInvitationInfoRepository-> ContactInvitationInfo", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in ContactInvitationInfoRepository-> ContactInvitationInfo", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in ContactInvitationInfoRepository-> ContactInvitationInfo", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in ContactInvitationInfoRepository-> ContactInvitationInfo", ex);
            }
        }
         #endregion Methods
    }
}