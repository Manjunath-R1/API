using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using ThoughtFocus.RoleProvider.Interfaces;
using System.Linq;
using ThoughtFocus.Domain.User;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    #region Fields
    private readonly IListRole _listRole;

    #endregion Fields

    public AuthorizeAttribute(IListRole listRole)
    {
        _listRole = listRole;
    }


    public void OnAuthorization(AuthorizationFilterContext context)
    {
        //check if token is expired
        var tokenArray = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ");
        if (tokenArray != null && tokenArray.Length > 1)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault().Split(" ")[1];
            if(token=="null" || token==null)
            {
                context.Result = new JsonResult(new { message = "Token Expired" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDetails = tokenHandler.ReadJwtToken(token);
            if (DateTime.Compare(tokenDetails.ValidTo, DateTime.UtcNow) < 0)
            {
                context.Result = new JsonResult(new { message = "Token Expired" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
        }
        
        // Check if user is logged in for accessing the secure data
        var user = (UserSessionEntity)context.HttpContext.Items["User"];
        if (user == null)
        {
            // not logged in
            context.Result = new JsonResult(new { message = "Token Expired" }) { StatusCode = StatusCodes.Status401Unauthorized };

        }
            
        else
        {
            
            var roleFunctions = _listRole.GetGlobalRolePermissions(user.UserID);
            bool hasPermission;

            var permissions = roleFunctions.Where(rolePermission =>
            rolePermission.Subject.ToLower().Equals(context.RouteData.Values["controller"].ToString().ToLower()) &&
            rolePermission.Action != null && rolePermission.Action.Name.ToLower().Equals(context.RouteData.Values["action"].ToString().ToLower())).ToList();

            if (permissions.Any()) // There is an entry in DB for Subject and Action
            {
                hasPermission = permissions.Any(rolePermission => rolePermission.IsAllowed); // Atleast one permission to allow Subject and Action
            }
            else
            {
                hasPermission = roleFunctions.Any(rolepermission => rolepermission.Subject.ToLower().Equals(context.RouteData.Values["controller"].ToString())
                 && rolepermission.Action == null && rolepermission.IsAllowed.Equals(true));
            }

            if(context.RouteData.Values["action"].ToString().ToLower()=="checkauthentication"){
                hasPermission = true;
            }

            if (!hasPermission)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

        }
    }
}