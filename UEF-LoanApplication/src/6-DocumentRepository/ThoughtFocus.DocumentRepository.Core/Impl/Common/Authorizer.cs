using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Enumeration;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain.Response;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.Common.Exceptions.BusinessException;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class Authorizer : IAuthorizer
    {
        #region Fields

        private readonly ILogger<Authorizer> _logger;
        private IAccessRoleRepository _accessRoleRepository;
        private IGroupRepository _groupRepository;

        #endregion Fields

        #region Constructor

        public Authorizer(IAccessRoleRepository accessRoleRepository, IGroupRepository groupRepository,
            ILogger<Authorizer> logger
            )
        {
            _accessRoleRepository = accessRoleRepository;
            _groupRepository = groupRepository;
            _logger = logger;
        }

        #endregion Constructor

        #region Methods

        public AuthorizationResponse AuthorizeUser(AuthorizeRequest authorizeRequest)
        {
            AuthorizationResponse authorizationResponse = new AuthorizationResponse();
            try
            {
                InheritanceRequest inheritanceRequest = new InheritanceRequest();
                InheritanceResponse inheritanceResponse = new InheritanceResponse();

                inheritanceRequest.ContentNodeName = authorizeRequest.ContentNodeName;
                inheritanceRequest.ContentKeyName = authorizeRequest.ContentKeyName;
                inheritanceRequest.ContentID = authorizeRequest.ContentID;

                inheritanceResponse = _accessRoleRepository.GetInheritedProjectOrDocument(inheritanceRequest);
                if (inheritanceResponse != null)
                {
                    authorizeRequest.ContentNodeName = inheritanceResponse.ContentNodeName;
                    authorizeRequest.ContentKeyName = inheritanceResponse.ContentKeyName;
                    authorizeRequest.ContentID = inheritanceResponse.ContentID;
                }

                List<IAuthorizerStrategy> authorizerStrategies = GetAuthorizationStrategies();


                authorizeRequest.AssigneeNodeName = NodeNameEnumeration.RepositoryUser.ToString();
                authorizeRequest.AssigneeKeyName = NodeKeyNameEnumeration.UserGuID.ToString();


                foreach (var authorizerStrategy in authorizerStrategies)
                {
                    authorizationResponse = authorizerStrategy.GetAuthorization(authorizeRequest);

                    if (authorizationResponse.IsAllowed)
                    {
                        break;
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }

            return authorizationResponse;
        }

        private List<IAuthorizerStrategy> GetAuthorizationStrategies()
        {
            var authorizationType = typeof(IAuthorizerStrategy);
            var authorizationTypes = authorizationType.Assembly.GetTypes()
                .Where(p => authorizationType.IsAssignableFrom(p) && p.IsClass);

            var authorizerStrategies = authorizationTypes
                .Select(x => (IAuthorizerStrategy)Activator.CreateInstance(x, _accessRoleRepository,_groupRepository)).OrderBy(x => x.ExcicutionOrder)
                .ToList();
            return authorizerStrategies;
        }

        #endregion Methods
    }
}
