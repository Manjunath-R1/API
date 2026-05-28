
using aThoughtFocus.DataAccess.Models.Audit;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 


namespace ThoughtFocus.DataAccess.Models.Audit
{
    [Table("AuditSessionDetails", Schema = "Audit")]
    public class AuditSessionDetails
    {
        [Key]
        public int Id { get; set; }
        public long UserID { get; set; }       
        public string IPAddress { get; set; }
        public string BrowserType { get; set; }
        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        public string SystemPlatform { get; set; }
        public Nullable<System.DateTime> LoginDateTime { get; set; }
        public Nullable<System.DateTime> LogoutDateTime { get; set; }
       
    }
}
