using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace ThoughtFocus.DataAccess.Models
{
    [Table("ApplicationDocumentRequestLog", Schema = "Application")]
    public partial class ApplicationDocumentRequestLog
    {
        #region Properties

        [Key]
        public long RequestID { get; set; }
        public long LoanApplicationID { get; set; }
        public int DocumentTypeID { get; set; }
        public long SchoolID { get; set; }
        public string RequestContentType { get; set; }
        public string RequestUri { get; set; }
        public string RequestMethod { get; set; }
        public DateTime? RequestTimestamp { get; set; }
        public string ResponseContentType { get; set; }
        public HttpStatusCode ResponseStatusCode { get; set; }
        public DateTime? ResponseTimestamp { get; set; }

        #endregion Properties
    }
}
