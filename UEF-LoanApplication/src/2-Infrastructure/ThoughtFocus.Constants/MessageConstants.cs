namespace ThoughtFocus.Constants
{
    public static class MessageConstants
    {
        #region Fields

        public const string Code = "Code doesn't match.";
        public const string ContactExits = "Contact already Exists.";
        public const string Deactive = "User account deactivated successfully.";
        public const string Active = "User account successfully activated.";
        public const string DeanExits = "Dean already exists for selected school.";
        public const string EditFailure = "Edit Failed";
        public const string SaveFailure = "Save Failed";
        public const string EmailExists = "Email Address is already registered with same contact or with different contact.";
        public const string EmailExistsMany = "One of the newly entered emailAddress is already registered with same contact or with different contact.";
        public const string ExceptionError = " An error has occurred. Please contact the administrator.";
        public const string exists = "User doesn't exist.";
        public const string Failure = "Failed";
        public const string InvalidCaptcha = "Invalid captcha.";
        public const string InvalidCredentials = "Invalid Username or Password. Please contact the administrator.";
        public const string InvalidLoginAttempt = "Invalid login attempt.";
        public const string InvalidUserName = "Invalid Username.";
        public const string LastThreePassword = "New password shouldn't be same as last 3 passwords. Please enter different password.";
        public const string LinkExpired = "The Reset link is expired.";
        public const string LockedOrInactiveUserAccount = "Account is either locked or inactive. Please contact the administrator.";
        public const string NullException = "Exception occurred. Please contact the administrator.";
        public const string NewPasswordDoesnotMatch = "New Password and Confirm Password aren't matching.";
        public const string OldToken = "This token is expired. Please use the new mail.";
        public const string OrganizationExists = "Organization already exists.";
        public const string PasswordDoesnotMatch = "Old Password doesn't match.";
        public const string PasswordExpiry = "Password is expired.";
        public const string ProspectExits = "Prospect already exists.";
        public const string SchoolExits = "School already Exists.";
        public const string SecurityQuestionAndAnswerIsNotSetup = "Security question and answer is not setup. Please contact the administrator.";
        public const string SecurityQuestionAndOrAnswerDoesnotMatch = "Security question and answer doesn't match.";
        public const string SiteVisitExists = "SiteVisit already created.";
        public const string Success = "Success";
        public const string UIExceptionError = "An error has occurred. Please contact the administrator with this error code {0}";
        public const string UnauthorizedUser = "Unauthorized user: Please enter valid credentials. Account will be locked after 3 consecutive wrong attempts.";
        public const string UnmatchSecurity = "Entered username/security question & answer do not match.";
        public const string UserCredentialLoginAttemptFailed1 = "You have made ";
        public const string UserCredentialLoginAttemptFailed2 = " unsuccessful attempt(s). Maximum retry attempt(s) allowed for login are 3.";
        public const string UserCredentialPasswordAttemptFailed1 = "You have made ";
        public const string UserCredentialPasswordAttemptFailed2 = " unsuccessful attempt(s). Maximum retry attempt(s) allowed for reseting password are 3.";
        public const string UserDoesnotExists = "Invalid Username or Password. Please try again.";
        public const string UserExits = "User already Exists.";
        public const string UserRoleExists = "Email already has been sent. Please check your inbox.";
        public const string UserLoginError = "An error has occurred. Please try again after some time.";

        public const string SiteVisitScheduled = "Site visit is scheduled for {0} school.";
        public const string DueDateToUploadMemberReport = "Due date to upload member reports for {0} school.";
        public const string DueDateToUploadPreparationDocuments = "Due date to upload the preparation documents for {0} school.";
        public const string DueDateToProvideFeedbackOnSiteVisitReport = "Due date to provide the feedback on site visit report for {0} school.";

        public const string NotesOnConflictOfInterest = "<b>Rejected Sitevisit</b><br/><b>School: </b>{0}</br><b>From: </b>{1}</br><b>To: </b>{2}</br>" +
            "<b>Reason for Rejection:</b>Conflict of Interest-{3}</br><b>Description:</b>{4}</br>";

        public const string NotesOnNonAvailability = "<b>Rejected Sitevisit</b><br/><b>School: </b>{0}</br><b>From: </b>{1}</br><b>To: </b>{2}</br>" +
            "<b>Reason for Rejection: </b> Non Availability</br><b>Description: </b>{3}</br>";

        public const string NotesOnSitevisitMemberInvitationAccept = "<b>Accepted Sitevisit</b><br/><b>School: </b>{0}</br><b>From: </b>{1}</br><b>To: </b>{2}</br>" +
            "<b>Accept Type:</b> -{3}</br><b>Description:</b>{4}</br>";

        public const string NotesOnSitevisitMemberInvitationReject = "<b>Rejected Sitevisit</b><br/><b>School: </b>{0}</br><b>From: </b>{1}</br><b>To: </b>{2}</br>" +
            "<b>Reject Type:</b> -{3}</br><b>Description:</b>{4}</br>";

        public const string DeanDuplicate = "Contact is already a Dean of other School.";
        public const string DateValidation = "Enter SiteVisit From Date and To Date To continue.";
        public const string RemovalUponRequestAccepted = "Staff has Accepted the request for removal.";
        public const string WithdrawalRequested = "Requested for Withdrawal.";
        public const string WithdrawalAccepted = "Withdrawal Request accepted.";
        public const string WithdrawalRejected = "Withdrawal Request Rejected.";
        public const string MemberRemoved = "Removed Site Visit Member.";
        public const string DocumentUpload = "Sitevisit preparation documents are not uploaded yet.";
        public const string MemberNotAvailable = "Member is not available during given site visit date range.";
        public const string MemberNonAvailabilityInitial = "Following Member(s) are not available during given site visit date range:";
        public const string ChairAccetanceOnInvitation = "Member Invitation can't be sent until an Accepted Chair is part of SiteVisit. ";
        public const string DateOnSendInvitation = "Site Visit should be saved with dates to send invitation.";

        public const string ContactDoesntExists = "Contact doesn't exists. Please contact administrator";
        public const string DailyEmailLimitExceeded = "Daily notification limit exceeded. Please try sending emails tommorrow";
        public const string UserMessageForNotAbletoSaveData = "Unable to save data, please try again.";
        public const string AlertHeaderFormat = "{0} days to go";

        public const string InboundEmailConfigurationKey = "ReplyTo";

        public const string Deleted = "Successfully deleted the record";

        public const string invalidInputs = "Invalid inputs.";

        #endregion Fields
    }
}