using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.User
{
    [Table("UserSecurityQuestionInfo", Schema = "User")]
    public partial class UserSecurityQuestionInfo : AuditBase
    {
        [Key]
        public long UserID { get; set; }
        public long SecurityQuestionID { get; set; }
        public string SecurityAnswer { get; set; }
    }
}
