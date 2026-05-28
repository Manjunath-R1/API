using System;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Repository.Interfaces.Master;
using ThoughtFocus.Repository.Interfaces.User; 
using ThoughtFocus.Business.Interfaces;
using ThoughtFocusRepository.Interfaces.Master;
using ThoughtFocus.Repository.Interfaces.Notification;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.Domain.EmailTemplate;

namespace ThoughtFocus.Business.Impl.EmailTemplate
{
    public class EmailTemplateHeaderFooterImpl : IEmailTemplateHeaderFooter
    {
        #region Fields

        private IRUserRoleRepository userRoleRepository;
         private INotificationRepository notificationRepository;
        private IEmailNotificationHeaderFooterRepository emailNotificationHeaderFooterRepository;


        #endregion Fields

        #region Constructor

        public EmailTemplateHeaderFooterImpl(INotificationRepository notificationRepository, IEmailNotificationHeaderFooterRepository emailNotificationHeaderFooterRepository, IRUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
            this.notificationRepository = notificationRepository;
            this.emailNotificationHeaderFooterRepository = emailNotificationHeaderFooterRepository;
        }

        #endregion Constructor

        #region Methods

        public EmailTemplateUploadResponse SaveEmailTemplateFooter(string emailTemplateFooter, long userId)
        {
              EmailTemplateUploadResponse emailTemplateUploadResponse = new EmailTemplateUploadResponse();
             EmailTemplateHeaderFooterEntity emailTemplateHeaderFooterEntity = new EmailTemplateHeaderFooterEntity();
            var currentDate = DateTime.Now;
            try
            {
                EmailTemplateHeaderFooter emailNotificationFooter = this.emailNotificationHeaderFooterRepository.GetEmailTemplateFooter();
                emailTemplateFooter = "<tr><td>" + emailTemplateFooter + "</td></tr>";
                if (emailNotificationFooter == null)
                {
                    emailNotificationFooter = new EmailTemplateHeaderFooter();

                    emailNotificationFooter.CreatedByUserID = userId;
                    emailNotificationFooter.CreatedDateTime = currentDate;
                    emailNotificationFooter.LastModifiedByUserID = userId;
                    emailNotificationFooter.LastModifiedDateTime = currentDate;
                    emailNotificationFooter.IsActive = true;
                    emailNotificationFooter.Footer = emailTemplateFooter;
                }
                else
                {

                    if (emailNotificationFooter.Footer == null)
                    {
                        emailNotificationFooter.LastModifiedByUserID = userId;
                        emailNotificationFooter.LastModifiedDateTime = currentDate;
                        emailNotificationFooter.Footer = emailTemplateFooter;
                    }
                    else
                    {

                        emailTemplateHeaderFooterEntity.CreatedByUserID = emailNotificationFooter.CreatedByUserID;
                        emailTemplateHeaderFooterEntity.CreatedDateTime = emailNotificationFooter.CreatedDateTime;
                        emailTemplateHeaderFooterEntity.DocumentTypeID = Convert.ToInt32(emailNotificationFooter.DocumentTypeID);
                        emailTemplateHeaderFooterEntity.LogoID = emailNotificationFooter.LogoGUID;
                        emailTemplateHeaderFooterEntity.FolderName = emailNotificationFooter.FolderName;
                        emailTemplateHeaderFooterEntity.ImageKey = emailNotificationFooter.ImageKey;


                        this.emailNotificationHeaderFooterRepository.MakeEmailTemplateHeaderFooterInactive(emailNotificationFooter);
                        emailNotificationFooter = new EmailTemplateHeaderFooter();
                        emailNotificationFooter.EmailTemplateHeaderFooterID = 0;
                        emailNotificationFooter.CreatedByUserID = emailTemplateHeaderFooterEntity.CreatedByUserID;
                        emailNotificationFooter.CreatedDateTime = emailTemplateHeaderFooterEntity.CreatedDateTime;
                        emailNotificationFooter.LastModifiedByUserID = userId;
                        emailNotificationFooter.LastModifiedDateTime = currentDate;
                        emailNotificationFooter.DocumentTypeID = emailTemplateHeaderFooterEntity.DocumentTypeID;
                        emailNotificationFooter.LogoGUID = emailTemplateHeaderFooterEntity.LogoID;
                        emailNotificationFooter.FolderName = emailTemplateHeaderFooterEntity.FolderName;
                        emailNotificationFooter.ImageKey = emailTemplateHeaderFooterEntity.ImageKey;
                        emailNotificationFooter.IsActive = true;
                        emailNotificationFooter.Footer = emailTemplateFooter;
                    }

                }
                if (this.emailNotificationHeaderFooterRepository.AddorUpdateFooter(emailNotificationFooter) == true)
                {
                    emailTemplateUploadResponse.IsSuccess = true;
                    emailTemplateUploadResponse.Message = "Footer uploaded successfully.";
                    emailTemplateUploadResponse.emailTemplateHeaderFooterViewModel = new EmailTemplateHeaderFooterViewModel();
                    emailTemplateUploadResponse.emailTemplateHeaderFooterViewModel.EmailTemplateHeaderFooterID = emailNotificationFooter.EmailTemplateHeaderFooterID;
                }
                else
                {
                    emailTemplateUploadResponse.IsSuccess = false;
                    emailTemplateUploadResponse.Message = "Unable to save. Please try after sometime.";
                }
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (BusinessException ex)
            {
                throw new BusinessException("Failed to get data from business layer", ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Failed to get data from business layer", ex);
            }
              return emailTemplateUploadResponse;
        }


        #endregion Methods

    }
}
