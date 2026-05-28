using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using ThoughtFocus.App.Utilities;
using ThoughtFocus.App.ViewModels;
using ThoughtFocus.Constants;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.RoleProvider.Interfaces;
using ThoughtFocus.Service.Interfaces;

namespace ThoughtFocus.App.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ContactController : ControllerBase
    {
        #region Fields

        public ILogger<ContactController> _logger;
        private IContactService contactServiceImpl;


        public IListRole _listRole;
        #endregion Fields
        #region Constructors
        public ContactController(IContactService contactServiceImpl,
         ILogger<ContactController> logger, IListRole listRole)
        {
            _logger = logger;
            this.contactServiceImpl = contactServiceImpl;
            _listRole = listRole;

        }
        #endregion Constructors

        #region Methods

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("InviteContact")]
        public ContactDataResponse InviteContact(ContactRequest contact)
        {
            ContactDataResponse contactDataResponse = new ContactDataResponse();
            CommonResponse commonResponse = null;
            commonResponse = this.contactServiceImpl.AddContact(contact, LoginUserInformation.getLoggedInUser(HttpContext));

            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {
                contactDataResponse.IsSuccess = true;
                contactDataResponse.Message = "Contact saved successfully.";
                return contactDataResponse;
            }
            else
            {
                contactDataResponse.IsSuccess = false;
                contactDataResponse.Message = "Failed to save Contact.";
                contactDataResponse.ValidationErrors = commonResponse.ValidationErrors;
                return contactDataResponse;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ActivateAccount")]
        public ActivationResponse GetActivateAccountInfo(string t, string c)
        {
            ActivationResponse response = new ActivationResponse();
            response.IsSuccess = false;
            if (t == string.Empty || c == string.Empty)
            {
                response.Message = "Invalid request";
            }
            else
            {
                var activationDetails = this.contactServiceImpl.GetActivateAccountInfo(t, c);
                if (activationDetails.IsSuccess)
                {
                    response.UserId = activationDetails.UserId;
                    response.ContactId = activationDetails.ContactId;
                    response.TokenID = activationDetails.TokenID;
                    response.IsSuccess = true;
                }
                else
                    response.Message = "Invalid request";
            }
            return response;

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("ActivateUserAccount")]
        public ActivationResponse UpdateUserAccount(AccountActivationParam accountActivationParam)
        {
            ActivationResponse response = new ActivationResponse();
            response.IsSuccess = false;
            if (!ModelState.IsValid)
            {
                response.ValidationErrors = this.ModelState.Values.SelectMany(modelState => modelState.Errors, (modelState, error) => error.ErrorMessage).ToList();
            }
            else
            {
                var activationDetails = this.contactServiceImpl.ActivateAccount(accountActivationParam);
                if (activationDetails.IsSuccess)
                {
                    response.UserId = activationDetails.UserId;
                    response.ContactId = activationDetails.ContactId;
                    response.IsSuccess = true;
                    response.Message = activationDetails.Message;
                }
                else
                    response.Message = "Invalid request";
            }

            return response;

        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetContact")]
        public ContactDataResponse GetContact(long contactID)
        {
            ContactDataResponse contactDataResponse = new ContactDataResponse();
            try
            {
                ContactViewEntityResponse contactViewEntityResponse = null;

                contactViewEntityResponse = this.contactServiceImpl.GetContactInfoById(contactID, LoginUserInformation.getLoggedInUser(HttpContext));
                if (contactViewEntityResponse != null && contactViewEntityResponse.IsSuccess)
                {
                    contactDataResponse.IsSuccess = true;
                    contactDataResponse.Contact = contactViewEntityResponse.contactViewEntity;
                    contactDataResponse.ProgramInvitations = contactViewEntityResponse.ProgramInvitations;
                    return contactDataResponse;
                }
                else
                {
                    _logger.LogError("Contact is null");
                    contactDataResponse.Message = "Contact doesn't exist.";
                    contactDataResponse.IsSuccess = false;
                    return contactDataResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at ContactDataResponse GetContactInfoById >> ", ex);

                contactDataResponse.IsSuccess = false;
                contactDataResponse.Message = "Exception occurred while fetching Contact Info.";
                return contactDataResponse;
            }
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("DeleteContact")]
        public ContactDataResponse DeleteContact(long ContactID)
        {
            ContactDataResponse contactDataResponse = new ContactDataResponse();
            CommonResponse commonResponse = null;
            if (ContactID == 0)
            {
                _logger.LogError("Input parameter ContactID is 0 ");
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
                contactDataResponse.IsSuccess = false;
                return contactDataResponse;
            }
            commonResponse = this.contactServiceImpl.DeleteContact(ContactID, LoginUserInformation.getLoggedInUser(HttpContext));
            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {
                contactDataResponse.IsSuccess = true;
                contactDataResponse.Message = "Contact deleted successfully.";
                return contactDataResponse;
            }
            else
            {
                contactDataResponse.IsSuccess = false;
                contactDataResponse.Message = "Contact doesn't exist.";
                return contactDataResponse;
            }
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetBusinessContacts")]
        public BusinessContactResponse GetBusinessContacts(long businessID)
        {
            BusinessContactResponse contactResponse = new BusinessContactResponse();
            try
            {
                contactResponse.IsSuccess = false;
                contactResponse.recordsTotal = 0;
                contactResponse.data = null;


                ContactListResponse response = this.contactServiceImpl.GetBusinessContacts(businessID);

                if (response != null && response.IsSuccess == true)
                {
                    contactResponse.data = response.BusinessContactResultEntity.DataList;
                    contactResponse.recordsTotal = response.BusinessContactResultEntity.TotalRecordCount;
                    contactResponse.IsSuccess = true;
                    return contactResponse;
                }
                else
                {
                    _logger.LogError("returned ContactListResponse  is incorrect {0}", response);
                    contactResponse.IsSuccess = false;
                    contactResponse.Message = "There are no users assigned to this Business Entity.";
                    return contactResponse;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult AddBusinessContact(SaveContactParam saveContactEntityParam) >> ", ex);

                contactResponse.IsSuccess = false;
                contactResponse.Message = "Exception occurred while fetching Borrower Contact.";
                return contactResponse;
            }

        }
        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("ResetPassword")]
        public ContactDataResponse ResetPassword(string userName)
        {
            ContactDataResponse contactDataResponse = new ContactDataResponse();
            CommonResponse commonResponse = null;
            commonResponse = this.contactServiceImpl.ResendActivationLink(userName, LoginUserInformation.getLoggedInUser(HttpContext));
            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {
                contactDataResponse.IsSuccess = true;
                contactDataResponse.Message = "Reset Password link sent successfully.";
                return contactDataResponse;
            }
            else
            {
                contactDataResponse.IsSuccess = false;
                contactDataResponse.Message = "Failed to send Reset Password link.";
                contactDataResponse.ValidationErrors = commonResponse.ValidationErrors;
                return contactDataResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("GetAllInternalContacts")]
        public InternlContactResponse GetAllInternalContacts()
        {
            InternlContactResponse contactResponse = new InternlContactResponse();
            try
            {
                contactResponse.IsSuccess = false;
                contactResponse.recordsTotal = 0;
                contactResponse.data = null;


                ContactListResponse response = this.contactServiceImpl.GetAllInternalContacts();

                if (response != null && response.IsSuccess == true)
                {
                    contactResponse.data = response.ContactPageResultEntity.DataList;
                    contactResponse.recordsTotal = response.ContactPageResultEntity.TotalRecordCount;
                    contactResponse.IsSuccess = true;
                    return contactResponse;
                }
                else
                {
                    _logger.LogError("returned ContactListResponse  is incorrect {0}", response);
                    contactResponse.IsSuccess = false;
                    contactResponse.Message = "No Contacts found.";
                    return contactResponse;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult GetAllInternalContacts() >> ", ex);

                contactResponse.IsSuccess = false;
                contactResponse.Message = "Exception occurred while fetching all Internal Contacts.";
                return contactResponse;
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("GetAllBusinessContacts")]
        public InternlContactResponse GetAllBusinessContacts(long? businessID)
        {
            InternlContactResponse contactResponse = new InternlContactResponse();
            try
            {
                contactResponse.IsSuccess = false;
                contactResponse.recordsTotal = 0;
                contactResponse.data = null;


                ContactListResponse response = this.contactServiceImpl.GetAllBusinessContacts(businessID);

                if (response != null && response.IsSuccess == true)
                {
                    contactResponse.data = response.ContactPageResultEntity.DataList;
                    contactResponse.recordsTotal = response.ContactPageResultEntity.TotalRecordCount;
                    contactResponse.IsSuccess = true;
                    return contactResponse;
                }
                else
                {
                    _logger.LogError("returned ContactListResponse  is incorrect {0}", response);
                    contactResponse.IsSuccess = false;
                    contactResponse.Message = "No Contacts found.";
                    return contactResponse;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult GetAllBusinessContacts() >> ", ex);

                contactResponse.IsSuccess = false;
                contactResponse.Message = "Exception occurred while fetching all Business Contacts.";
                return contactResponse;
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("AddRoleForExistingContact")]
        public ContactDataResponse AddRoleForExistingContact(BusinessContactRequest contact)
        {
            ContactDataResponse contactDataResponse = new ContactDataResponse();
            CommonResponse commonResponse = null;
            commonResponse = this.contactServiceImpl.AddRoleForContact(contact, LoginUserInformation.getLoggedInUser(HttpContext));


            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {
                contactDataResponse.IsSuccess = true;
                contactDataResponse.Message = "Role added successfully.";
                return contactDataResponse;
            }
            else
            {
                contactDataResponse.IsSuccess = false;
                contactDataResponse.Message = "Failed to add Contact Role.";
                contactDataResponse.ValidationErrors = commonResponse.ValidationErrors;
                return contactDataResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveorUpdateBusinessContact")]
        public ContactDataResponse SaveorUpdateBusinessContact(ContactRequest contact)
        {
            ContactDataResponse contactDataResponse = new ContactDataResponse();
            CommonResponse commonResponse = null;
            commonResponse = this.contactServiceImpl.SaveorUpdateContact(contact, LoginUserInformation.getLoggedInUser(HttpContext));

            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {
                contactDataResponse.IsSuccess = true;
                contactDataResponse.Message = "Contact saved successfully.";
                return contactDataResponse;
            }
            else
            {
                contactDataResponse.IsSuccess = false;
                contactDataResponse.Message = "Failed to save Contact.";
                contactDataResponse.ValidationErrors = commonResponse.ValidationErrors;
                return contactDataResponse;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ChangePassword")]
        public PasswordChangeAndForgotResponse ChangePassword(PasswordChangeRequest model)
        {
            PasswordChangeAndForgotResponse passwordChangeResponse = new PasswordChangeAndForgotResponse();
            CommonResponse commonResponse = null;
            commonResponse = this.contactServiceImpl.ChangePassword(model);
            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {
                passwordChangeResponse.IsSuccess = true;
                passwordChangeResponse.ValidationErrors = commonResponse.ValidationErrors;
                passwordChangeResponse.Message = commonResponse.StatusMessage;
            }
            else
            {
                passwordChangeResponse.IsSuccess = false;
                passwordChangeResponse.Message = commonResponse.StatusMessage;
                passwordChangeResponse.ValidationErrors = commonResponse.ValidationErrors;
            }

            return passwordChangeResponse;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ForgotPassword")]
        public PasswordChangeAndForgotResponse ForgotPassword(ForgotPasswordRequest model)
        {
            PasswordChangeAndForgotResponse forgotPasswordResponse = new PasswordChangeAndForgotResponse();
            CommonResponse commonResponse = null;
            commonResponse = this.contactServiceImpl.ForgotPassword(model);
            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {
                forgotPasswordResponse.IsSuccess = true;
                forgotPasswordResponse.ValidationErrors = commonResponse.ValidationErrors;
                forgotPasswordResponse.Message = commonResponse.StatusMessage;
            }
            else
            {
                forgotPasswordResponse.IsSuccess = false;
                forgotPasswordResponse.Message = commonResponse.StatusMessage;
                forgotPasswordResponse.ValidationErrors = commonResponse.ValidationErrors;

            }

            return forgotPasswordResponse;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("ForgotPasswordActivation")]
        public PasswordChangeAndForgotResponse GetForgotPasswordInfo(string t, string c)
        {
            PasswordChangeAndForgotResponse response = new PasswordChangeAndForgotResponse();
            response.IsSuccess = false;
            if (t == string.Empty || c == string.Empty)
            {
                response.Message = "Invalid request.";
            }
            else
            {
                var activationDetails = this.contactServiceImpl.GetForgotPasswordInfo(t, c);
                if (activationDetails.IsSuccess)
                {
                    response.UserId = activationDetails.UserId;
                    response.ContactId = activationDetails.ContactId;
                    response.TokenID = activationDetails.TokenID;
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = "Password reset link  is expired.";
                }
            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("UpdatePasswordFromForgotPassword")]
        public PasswordChangeAndForgotResponse UpdatePassword(NewPasswordChangeRequest model)
        {
            PasswordChangeAndForgotResponse passwordUpdateResponse = new PasswordChangeAndForgotResponse();
            CommonResponse commonResponse = null;
            commonResponse = this.contactServiceImpl.UpdatePassword(model);
            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {
                passwordUpdateResponse.IsSuccess = true;
                passwordUpdateResponse.Message = commonResponse.StatusMessage;
                passwordUpdateResponse.ValidationErrors = commonResponse.ValidationErrors;
            }
            else
            {
                passwordUpdateResponse.IsSuccess = false;
                passwordUpdateResponse.ValidationErrors = commonResponse.ValidationErrors;
                passwordUpdateResponse.Message = commonResponse.StatusMessage;
            }
            return passwordUpdateResponse;
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("DeactivateContact")]
        public ContactDataResponse DeactivateContact(long ContactID)
        {
            ContactDataResponse contactDataResponse = new ContactDataResponse();
            CommonResponse commonResponse = null;
            if (ContactID == 0)
            {
                _logger.LogError("Input parameter ContactID is 0");
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
                contactDataResponse.IsSuccess = false;
                return contactDataResponse;
            }
            commonResponse = this.contactServiceImpl.DeactivateBusinessContact(ContactID, LoginUserInformation.getLoggedInUser(HttpContext));
            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {
                contactDataResponse.IsSuccess = true;
                contactDataResponse.Message = commonResponse.StatusMessage;
                contactDataResponse.ValidationErrors = commonResponse.ValidationErrors;
                contactDataResponse.ID = commonResponse.ID;
                return contactDataResponse;
            }
            else
            {
                contactDataResponse.IsSuccess = false;
                contactDataResponse.Message = commonResponse.StatusMessage;
                contactDataResponse.ValidationErrors = commonResponse.ValidationErrors;
                contactDataResponse.ID = commonResponse.ID;
                return contactDataResponse;
            }
        }
        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("ActivateContact")]
        public ContactDataResponse ActivateContact(long ContactID)
        {
            ContactDataResponse contactDataResponse = new ContactDataResponse();
            CommonResponse commonResponse = null;
            if (ContactID == 0)
            {
                _logger.LogError("Input parameter ContactID is 0");
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
                contactDataResponse.IsSuccess = false;
                return contactDataResponse;
            }
            commonResponse = this.contactServiceImpl.ActivateBusinessContact(ContactID, LoginUserInformation.getLoggedInUser(HttpContext));
            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {
                contactDataResponse.IsSuccess = true;
                contactDataResponse.Message = commonResponse.StatusMessage;
                contactDataResponse.ValidationErrors = commonResponse.ValidationErrors;
                contactDataResponse.ID = commonResponse.ID;
                return contactDataResponse;
            }
            else
            {
                contactDataResponse.IsSuccess = false;
                contactDataResponse.Message = commonResponse.StatusMessage;
                contactDataResponse.ValidationErrors = commonResponse.ValidationErrors;
                contactDataResponse.ID = commonResponse.ID;
                return contactDataResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("ResetContact")]
        public PasswordChangeAndForgotResponse ResetContact(ForgotPasswordRequest model)
        {
            PasswordChangeAndForgotResponse ResetContactResponse = new PasswordChangeAndForgotResponse();
            CommonResponse commonResponse = null;
            commonResponse = this.contactServiceImpl.ResetBusinessContact(model);
            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {
                ResetContactResponse.IsSuccess = true;
                ResetContactResponse.ValidationErrors = commonResponse.ValidationErrors;
                ResetContactResponse.Message = commonResponse.StatusMessage;
                ResetContactResponse.ContactId = commonResponse.ID;
            }
            else
            {
                ResetContactResponse.IsSuccess = false;
                ResetContactResponse.Message = commonResponse.StatusMessage;
                ResetContactResponse.ValidationErrors = commonResponse.ValidationErrors;

            }

            return ResetContactResponse;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("ResetContactPasswordActivation")]
        public PasswordChangeAndForgotResponse ResetContactPasswordActivation(string t, string c)
        {
            PasswordChangeAndForgotResponse response = new PasswordChangeAndForgotResponse();
            response.IsSuccess = false;
            if (t == string.Empty || c == string.Empty)
            {
                response.Message = "Invalid request.";
            }
            else
            {
                var activationDetails = this.contactServiceImpl.GetResetContactPasswordInfo(t, c);
                if (activationDetails.IsSuccess)
                {
                    response.UserId = activationDetails.UserId;
                    response.ContactId = activationDetails.ContactId;
                    response.TokenID = activationDetails.TokenID;
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = "Password reset link  is expired.";
                }
            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("UpdatePasswordResetContact")]
        public PasswordChangeAndForgotResponse UpdatePasswordResetContact(NewPasswordChangeRequest model)
        {
            PasswordChangeAndForgotResponse passwordResetContactResponse = new PasswordChangeAndForgotResponse();
            CommonResponse commonResponse = null;
            commonResponse = this.contactServiceImpl.UpdatePasswordForResetContact(model);
            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {
                passwordResetContactResponse.IsSuccess = true;
                passwordResetContactResponse.Message = commonResponse.StatusMessage;
                passwordResetContactResponse.ValidationErrors = commonResponse.ValidationErrors;
            }
            else
            {
                passwordResetContactResponse.IsSuccess = false;
                passwordResetContactResponse.ValidationErrors = commonResponse.ValidationErrors;
                passwordResetContactResponse.Message = commonResponse.StatusMessage;
            }
            return passwordResetContactResponse;
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("Unlockaccount")]
        public PasswordChangeAndForgotResponse Unlockaccount(ForgotPasswordRequest model)
        {
            PasswordChangeAndForgotResponse UnblockBusinessContactResponse = new PasswordChangeAndForgotResponse();
            CommonResponse commonResponse = null;
            commonResponse = this.contactServiceImpl.UnblockBusinessContact(model);
            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {
                UnblockBusinessContactResponse.IsSuccess = true;
                UnblockBusinessContactResponse.Message = commonResponse.StatusMessage;
                UnblockBusinessContactResponse.ValidationErrors = commonResponse.ValidationErrors;
                UnblockBusinessContactResponse.ID = commonResponse.ID;
            }
            else
            {
                UnblockBusinessContactResponse.IsSuccess = false;
                UnblockBusinessContactResponse.Message = commonResponse.StatusMessage;
                UnblockBusinessContactResponse.ValidationErrors = commonResponse.ValidationErrors;
                UnblockBusinessContactResponse.ID = commonResponse.ID;
            }

            return UnblockBusinessContactResponse;
        }

    
        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveNotificationMode")]
        public NotificationTypeResponse SaveNotificationMode(NotificationTypeRequest NotificationTypeRequest)
        {
            NotificationTypeResponse response = new NotificationTypeResponse();
            CommonResponse commonResponse = null;

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
            }
            else
            {
                try
                {
                    commonResponse = this.contactServiceImpl.SaveNotificationMode(NotificationTypeRequest, LoginUserInformation.getLoggedInUser(HttpContext));

                    if (commonResponse != null && commonResponse.ResponseStatus == ResponseStatus.Success)
                    {

                        response.IsSuccess = true;
                        response.Message = commonResponse.StatusMessage;
                        response.ValidationErrors = commonResponse.ValidationErrors;
                        response.NotificationModeTypeID = commonResponse.ID;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = commonResponse.StatusMessage;
                        response.ValidationErrors = commonResponse.ValidationErrors;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at JsonResult SaveNotificationMode() >> ", ex);
                    response.IsSuccess = false;
                    response.Message = "Exception occurred while saving NotificationType.";
                }
            }

            return response;
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetNotificationModeByUser")]
        public NotificationTypeResponse GetNotificationModeByUser(long UserId)
        {
            NotificationTypeResponse notificationTypeResponse = new NotificationTypeResponse();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                notificationTypeResponse.IsSuccess = false;
                return notificationTypeResponse;
            }
            else
            {
                try
                {
                    notificationTypeResponse = this.contactServiceImpl.GetNotificationModeByUser(UserId);

                    if (notificationTypeResponse != null && notificationTypeResponse.IsSuccess == true)
                    {
                        notificationTypeResponse.IsSuccess = true;
                    }
                    else
                    {
                        _logger.LogError("returned notification is incorrect {0}", notificationTypeResponse);
                        notificationTypeResponse.IsSuccess = false;
                        notificationTypeResponse.Message = "Exception occurred while fetching notification mode.";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at JsonResult GetNotificationModeByUser() >> ", ex);
                    notificationTypeResponse.IsSuccess = false;
                    notificationTypeResponse.Message = "Exception occurred while fetching notification mode.";
                  
                }
                  return notificationTypeResponse;
            }
        }


        [HttpPost]
        [Route("ExportBusinessContacts")]
        public async Task<IActionResult> ExportBusinessContacts(ExportBusinessContactRequest exportRequest)
        {
            var programInvitationViewmodel = new ProgramInvitationViewModel();
            try
            {
                if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
                {
                    programInvitationViewmodel.IsSuccess = false;
                }

                await Task.Yield();
                var response = this.contactServiceImpl.ExportAllBusinessContacts(exportRequest);
                if (response != null && response.ContactPageResultEntity.DataList != null && response.ContactPageResultEntity.DataList.Count() > 0)
                {
                    await Task.Yield();
                    var stream = new MemoryStream();
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    string excelSheetName = $"BusinessContacts_{DateTime.Now.ToString("MM_dd_yyyy")}";

                    using (var package = new ExcelPackage(stream))
                    {
                        var workSheet = package.Workbook.Worksheets.Add(excelSheetName);

                        // Header Fields
                        workSheet.Cells["A1"].Value = "Business Name";
                        workSheet.Cells["B1"].Value = "First Name";
                        workSheet.Cells["C1"].Value = "Last Name";
                        workSheet.Cells["D1"].Value = "Email Address";
                        workSheet.Cells["E1"].Value = "Phone Number";
                        workSheet.Cells["F1"].Value = "Account Status";

                        string tableSelection;
                        int i = 1;

                        tableSelection = "A" + i.ToString() + ":F" + i.ToString();
                        workSheet.Cells[tableSelection].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[tableSelection].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#1A5276"));
                        workSheet.Cells[tableSelection].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));

                        int tablestart = i++;

                        foreach (var item in response.ContactPageResultEntity.DataList)
                        {
                            workSheet.Cells[i, 1].Value = item.BusinessName;
                            workSheet.Cells[i, 2].Value = item.FirstName;
                            workSheet.Cells[i, 3].Value = item.LastName;
                            workSheet.Cells[i, 4].Value = item.EmailAddress;
                            workSheet.Cells[i, 5].Value = item.PhoneNumber;
                            workSheet.Cells[i, 6].Value = item.AccountStatus;
                            tableSelection = "A" + i.ToString() + ":F" + i.ToString();
                            workSheet.Cells[tableSelection].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[tableSelection].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[tableSelection].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[tableSelection].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            i++;
                        }

                        workSheet.Cells.AutoFitColumns();
                        package.Save();
                    }

                    stream.Position = 0;
                    string excelName = $"BusinessContacts_{DateTime.Now.ToString("MM_dd_yyyy")}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }


                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at ExportProgramInvitations GetExportProgramInvitation >> ", ex);

                return BadRequest();
            }
        }
        #endregion Methods

    }
}