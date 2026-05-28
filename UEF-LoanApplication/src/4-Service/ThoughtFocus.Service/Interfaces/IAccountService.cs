using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Service.Interfaces
{
  public interface IAccountService
  {
    #region Methods
    CommonResponse saveUserConsent(UserSessionEntity userSessionEntity);

    #endregion Methods

  }
}
