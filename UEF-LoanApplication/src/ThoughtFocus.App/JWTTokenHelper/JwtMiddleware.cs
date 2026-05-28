using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Repository.Interfaces.User;
using ThoughtFocus.Service.Interfaces;

namespace ThoughtFocus.App.JWTTokenHelper
{
  public class JwtMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
      _next = next;
      _appSettings = appSettings.Value;
    }

    /**
      This method will trigger when user tries to open the web application or
      when tries to access the authorized resources
    */
    public async Task Invoke(HttpContext context, IUserLoginService userService)
    {
      var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

      if (token != null)
        attachUserToContext(context, userService, token);   //if user login attach user to context

      await _next(context);
    }

    private void attachUserToContext(HttpContext context, IUserLoginService userService, string token)
    {
      try
      {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.JWTSecretKey);
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false,
          // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
          ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var userName = (jwtToken.Claims.First(x => x.Type == "username").Value).ToString();

        // attach user to context on successful jwt validation
        context.Items["User"] = userService.GetUserDetailsByUserName(userName);
      }
      catch
      {
        // do nothing if jwt validation fails
        // user is not attached to context so request won't have access to secure routes
      }
    }
  }
}