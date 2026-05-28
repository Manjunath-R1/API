using System;
using System.Collections.Generic;
using System.Text;
using ThoughtFocus.App.ViewModels;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Service.Interfaces
{
  public interface IUserLoginService
  {
    AuthenticateResponse Authenticate(AuthenticateRequest model, string requestInfo, string IPAddress);

    UserSessionEntity GetUserDetailsByUserName(string UserName);

    
  }
}
