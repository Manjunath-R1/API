using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.Common.Exceptions.BusinessException;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class UserManager : IUserManager
    {
        private readonly ILogger<UserManager> _logger;
        private IUserRepository _userRepository;
        private IUserGroupRepository _userGroupRepository;

        public UserManager(IUserRepository userRepository, 
            IUserGroupRepository userGroupRepository,
            ILogger<UserManager> logger)
        {
            _userRepository = userRepository;
            _userGroupRepository = userGroupRepository;
            _logger = logger;

        }

        public DocumentBaseResponse SaveUser(UserEntity user)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            try
            {
                
                RepositoryUser repositoryUser = new RepositoryUser();
                repositoryUser.UserID = user.UserID;
                repositoryUser.UserGuID = user.UserGuID;
                repositoryUser.CreatedDateTime = user.CreatedDateTime;
                repositoryUser.CreatedByUserID = user.CreatedByUserID;
                repositoryUser.LastModifiedDateTime = user.LastModifiedDateTime;
                repositoryUser.LastModifiedByUserID = user.LastModifiedByUserID;
                repositoryUser.IsActive = user.IsActive;
                repositoryUser.UserName = user.UserName;
                repositoryUser.FirstName = user.FirstName;
                repositoryUser.LastName = user.LastName;
                repositoryUser.PasswordHash = user.PasswordHash;
                repositoryUser.PasswordSalt = user.PasswordSalt;
                repositoryUser.IsAccountActivated = user.IsAccountActivated;
                repositoryUser.IsLockedOut = user.IsLockedOut;
                repositoryUser.LastPasswordChangedDateTime = user.LastPasswordChangedDateTime;
                repositoryUser.FirstLoginDateTime = user.FirstLoginDateTime;
                repositoryUser.LastLoginDateTime = user.LastLoginDateTime;
                repositoryUser.LastLogoutDateTime = user.LastLogoutDateTime;
                repositoryUser.LastLockoutDateTime = user.LastLockoutDateTime;
                repositoryUser.FailedLoginAttemptCount = user.FailedLoginAttemptCount;
                repositoryUser.FailedLoginAttemptDateTime = user.FailedLoginAttemptDateTime;
                repositoryUser.FailedPasswordAttemptCount = user.FailedPasswordAttemptCount;
                repositoryUser.FailedPasswordAttemptDateTime = user.FailedPasswordAttemptDateTime;
                repositoryUser.AccountActivationDate = user.AccountActivationDate;

                if (repositoryUser.IsActive == false)
                {
                    this._userRepository.DeleteUserAccessRoleRelationship(repositoryUser.UserGuID);
                }

                this._userRepository.Save(repositoryUser);
                documentBaseResponse.IsSuccess = true;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while Saving user {0} with exception", user, ex));
                documentBaseResponse.IsSuccess = false;

            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while Saving user {0} with exception", user, ex));
                documentBaseResponse.IsSuccess = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while Saving user {0} with exception", user, ex));
                documentBaseResponse.IsSuccess = false;
            }
            return documentBaseResponse;
        }

        public DocumentBaseResponse UpdateUserGroups(List<RepositoryUserGroupMapping> repositoryUserGroups)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            DateTime currentTime = DateTime.Now;
            try
            {
                List<RepositoryUserGroupMapping> userGroups = new List<RepositoryUserGroupMapping>();
                foreach (var userGroup in repositoryUserGroups)
                {
                    RepositoryUserGroupMapping repositoryUserGroup = new RepositoryUserGroupMapping();
                    repositoryUserGroup = this._userGroupRepository.FirstOrDefault(a => a.IsActive == true && a.RepositoryUserID == userGroup.RepositoryUserID && a.GroupID == userGroup.GroupID);
                    if (userGroup.IsActive && repositoryUserGroup == null)
                    {
                        repositoryUserGroup = new RepositoryUserGroupMapping();
                        repositoryUserGroup.CreatedByUserID = userGroup.CreatedByUserID;
                        repositoryUserGroup.LastModifiedByUserID = userGroup.CreatedByUserID;
                        repositoryUserGroup.CreatedDateTime = currentTime;
                        repositoryUserGroup.LastModifiedDateTime = currentTime;
                        repositoryUserGroup.IsActive =true;
                        repositoryUserGroup.RepositoryUserID = userGroup.RepositoryUserID;
                        repositoryUserGroup.GroupID = userGroup.GroupID;
                        userGroups.Add(repositoryUserGroup);
                    }
                    else if (!userGroup.IsActive && repositoryUserGroup != null)
                    {
                        
                        repositoryUserGroup.LastModifiedByUserID = userGroup.CreatedByUserID;
                        repositoryUserGroup.LastModifiedDateTime = currentTime;
                        repositoryUserGroup.IsActive = false;
                        userGroups.Add(repositoryUserGroup);
                    }

                    this._userGroupRepository.SaveOrUpdate(userGroups);
                    
                }
                documentBaseResponse.IsSuccess = true;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while Updating UserGroups with exception", ex));
                documentBaseResponse.IsSuccess = false;

            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while Updating UserGroups with exception", ex));
                documentBaseResponse.IsSuccess = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while Updating UserGroups with exception", ex));
                documentBaseResponse.IsSuccess = false;
            }
            return documentBaseResponse;
        }

        public List<UserEntity> GetUsers()
        {
            try
            {
                List<UserEntity> userList = new List<UserEntity>();
                 var users = _userRepository.GetUsers();

                foreach(var user in users){
                    UserEntity userEntity = new UserEntity();  
                    userEntity.UserID = user.UserID;
                    userEntity.UserGuID = user.UserGuID;
                    userEntity.CreatedDateTime = user.CreatedDateTime;
                    userEntity.CreatedByUserID = user.CreatedByUserID;
                    userEntity.LastModifiedDateTime = user.LastModifiedDateTime;
                    userEntity.LastModifiedByUserID = user.LastModifiedByUserID;
                    userEntity.IsActive = user.IsActive;
                    userEntity.UserName = user.UserName;
                    userEntity.FirstName = user.FirstName;
                    userEntity.LastName = user.LastName;
                    userEntity.PasswordHash = user.PasswordHash;
                    userEntity.PasswordSalt = user.PasswordSalt;
                    userEntity.IsAccountActivated = user.IsAccountActivated;
                    userEntity.IsLockedOut = user.IsLockedOut;
                    userEntity.LastPasswordChangedDateTime = user.LastPasswordChangedDateTime;
                    userEntity.FirstLoginDateTime = user.FirstLoginDateTime;
                    userEntity.LastLoginDateTime = user.LastLoginDateTime;
                    userEntity.LastLogoutDateTime = user.LastLogoutDateTime;
                    userEntity.LastLockoutDateTime = user.LastLockoutDateTime;
                    userEntity.FailedLoginAttemptCount = user.FailedLoginAttemptCount;
                    userEntity.FailedLoginAttemptDateTime = user.FailedLoginAttemptDateTime;
                    userEntity.FailedPasswordAttemptCount = user.FailedPasswordAttemptCount;
                    userEntity.FailedPasswordAttemptDateTime = user.FailedPasswordAttemptDateTime;
                    userEntity.AccountActivationDate = user.AccountActivationDate;	
                    userList.Add(userEntity);
                }

                return userList;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching Users -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching Users -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching Users -{0}", ex));
                throw new BusinessException("", ex);
            }
        }

        public UserEntity GetUser(long userID)
        {
            try
            {
                var user = this._userRepository.GetUser(userID);
                UserEntity userEntity = new UserEntity();
                userEntity.UserID = user.UserID;
                userEntity.UserGuID = user.UserGuID;
                userEntity.CreatedDateTime = user.CreatedDateTime;
                userEntity.CreatedByUserID = user.CreatedByUserID;
                userEntity.LastModifiedDateTime = user.LastModifiedDateTime;
                userEntity.LastModifiedByUserID = user.LastModifiedByUserID;
                userEntity.IsActive = user.IsActive;
                userEntity.UserName = user.UserName;
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;
                userEntity.PasswordHash = user.PasswordHash;
                userEntity.PasswordSalt = user.PasswordSalt;
                userEntity.IsAccountActivated = user.IsAccountActivated;
                userEntity.IsLockedOut = user.IsLockedOut;
                userEntity.LastPasswordChangedDateTime = user.LastPasswordChangedDateTime;
                userEntity.FirstLoginDateTime = user.FirstLoginDateTime;
                userEntity.LastLoginDateTime = user.LastLoginDateTime;
                userEntity.LastLogoutDateTime = user.LastLogoutDateTime;
                userEntity.LastLockoutDateTime = user.LastLockoutDateTime;
                userEntity.FailedLoginAttemptCount = user.FailedLoginAttemptCount;
                userEntity.FailedLoginAttemptDateTime = user.FailedLoginAttemptDateTime;
                userEntity.FailedPasswordAttemptCount = user.FailedPasswordAttemptCount;
                userEntity.FailedPasswordAttemptDateTime = user.FailedPasswordAttemptDateTime;
                userEntity.AccountActivationDate = user.AccountActivationDate;
                return userEntity;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching Users -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching Users -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching Users -{0}", ex));
                throw new BusinessException("", ex);
            }
        }

        public UserEntity GetUser(Guid userID)
        {
            try
            { 
                var user = this._userRepository.GetUser(userID);
                UserEntity userEntity = new UserEntity();
                userEntity.UserID = user.UserID;
                userEntity.UserGuID = user.UserGuID;
                userEntity.CreatedDateTime = user.CreatedDateTime;
                userEntity.CreatedByUserID = user.CreatedByUserID;
                userEntity.LastModifiedDateTime = user.LastModifiedDateTime;
                userEntity.LastModifiedByUserID = user.LastModifiedByUserID;
                userEntity.IsActive = user.IsActive;
                userEntity.UserName = user.UserName;
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;
                userEntity.PasswordHash = user.PasswordHash;
                userEntity.PasswordSalt = user.PasswordSalt;
                userEntity.IsAccountActivated = user.IsAccountActivated;
                userEntity.IsLockedOut = user.IsLockedOut;
                userEntity.LastPasswordChangedDateTime = user.LastPasswordChangedDateTime;
                userEntity.FirstLoginDateTime = user.FirstLoginDateTime;
                userEntity.LastLoginDateTime = user.LastLoginDateTime;
                userEntity.LastLogoutDateTime = user.LastLogoutDateTime;
                userEntity.LastLockoutDateTime = user.LastLockoutDateTime;
                userEntity.FailedLoginAttemptCount = user.FailedLoginAttemptCount;
                userEntity.FailedLoginAttemptDateTime = user.FailedLoginAttemptDateTime;
                userEntity.FailedPasswordAttemptCount = user.FailedPasswordAttemptCount;
                userEntity.FailedPasswordAttemptDateTime = user.FailedPasswordAttemptDateTime;
                userEntity.AccountActivationDate = user.AccountActivationDate;
                return userEntity;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching Users -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching Users -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching Users -{0}", ex));
                throw new BusinessException("", ex);
            }
        }
    }
}
