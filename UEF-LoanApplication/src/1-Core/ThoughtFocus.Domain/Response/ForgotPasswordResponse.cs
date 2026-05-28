using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Response
{
   public class ForgotPasswordResponse : BaseResponse
 {
    public  long UserId { get; set; }
    public  long ContactId { get; set; }
    public  string TokenID { get; set; }
}
}
