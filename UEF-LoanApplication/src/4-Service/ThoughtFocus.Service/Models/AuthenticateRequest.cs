using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThoughtFocus.App.ViewModels
{
  public class AuthenticateRequest 
    {

    [Required(ErrorMessage="Please enter your Username for username field")]
    public string Username { get; set; }

    [Required(ErrorMessage="Please enter your Password for password field")]
    public string Password { get; set; }
    
  }

    
}
