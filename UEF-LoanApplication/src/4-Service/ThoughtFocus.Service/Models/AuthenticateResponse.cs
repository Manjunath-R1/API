using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThoughtFocus.DataAccess.Models.User;

namespace ThoughtFocus.App.ViewModels
{
    public class AuthenticateResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string JWTToken { get; set; }
        public List<long> RoleID { get; set; }
        public List<string> RoleName { get; set; }

        public bool showConcent { get; set; }
        public long? UserID { get; set; }
        public long? ContactID { get; set; }
        public List<long> SubRoleID { get; set; }
        public List<string> SubRoleName { get; set; }
        public bool HasOwnerPermission { get; set; }
        public string DeactivateBusinessContactResponce { get; set; }

        public string UnlockBusinessContactResponce { get; set; }
        public AuthenticateResponse()
        {

        }

        public AuthenticateResponse(User user, string token)
        {
            FirstName = user?.Contact?.FirstName;
            LastName = user?.Contact?.LastName;
            UserName = user?.UserName;
            JWTToken = token;
            RoleID = user?.UserRoles.Where(x => x.IsActive == true)?.Select(a => a.RoleID).ToList();
            RoleName = user?.UserRoles.Where(x => x.IsActive == true)?.Select(a => a.Role.RoleName).ToList();
            showConcent = false;
            UserID = user?.UserID;
            ContactID = user?.ContactID;

            if (user != null && user.UserRoles.Where(x => x.RoleID == 2).Count() > 0 && user.UserConsent == null)
            {
                showConcent = true;
            }
            HasOwnerPermission = false;

        }
    }
}
