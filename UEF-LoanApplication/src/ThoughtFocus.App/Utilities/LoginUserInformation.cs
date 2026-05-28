

using Microsoft.AspNetCore.Http; 
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.App.Utilities
{
    public static class LoginUserInformation
    {

        public static Domain.User.UserSessionEntity getLoggedInUser(HttpContext loggedInContext)
        {             
            if(loggedInContext !=null && loggedInContext.Items!=null )
            {
                 return (UserSessionEntity)loggedInContext.Items["User"];
            }
            return null;
        }


    }
}