using System.Collections.Generic;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.AccrediO.Accreditation.Domain.Response
{
    public class DocumentSearchResponse:BaseResponse
    {
       public  List<DocumentViewModel> DocumentViewModels { get; set; }
       public DocumentSearchRequest DocumentSearchRequest { get; set; }
       public List<File> Files { get; set; }
       public long TotalRecord { get; set; }
    }
}
