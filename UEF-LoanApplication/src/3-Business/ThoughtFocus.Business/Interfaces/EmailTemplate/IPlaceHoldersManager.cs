using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Master;
using ThoughtFocus.Domain.Notification;

namespace ThoughtFocus.Business.Interfaces.EmailTemplate
{
    public interface IPlaceHoldersManager
    {
        #region Methods

 

        List<SendEmailParameter> GetPlacehodersValue(PlaceHolderReplaceRequest replaceRequest);
        List<SendEmailParameter> GetPlacehodersValueExceptBorrower(PlaceHolderReplaceRequest replaceRequest,long notificationModeID);
        List<SendEmailParameter> GetPlacehodersEmailNotificationValue(ProgramInvitationEmailPlaceHolderReplaceRequest replaceRequest);


        #endregion Methods
    }
}
