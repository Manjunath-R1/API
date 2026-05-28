using System;
using System.Collections.Generic;
using System.Text;
using ThoughtFocus.DataAccess.Models.User;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Business.Interfaces.User
{
  public interface IUserAccount
  {
    void UpdateUserOnLogin(DataAccess.Models.User.User User);
  }
}
