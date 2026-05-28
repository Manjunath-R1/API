
namespace ThoughtFocus.Domain.Response
{
    public class DocumentViewEntity : BaseResponse
    {
        #region Properties
        //public long DocumentTypeID { get; set; }
        public string DocumentName { get; set; }
        public bool IsActive { get; set; }

        #endregion Properties
    }
}
