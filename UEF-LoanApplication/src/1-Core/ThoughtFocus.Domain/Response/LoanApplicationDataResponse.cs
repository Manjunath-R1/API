using ThoughtFocus.Domain.Params;

namespace ThoughtFocus.Domain.Response
{
    public class LoanApplicationDataResponse :BaseResponse 
    {
        public LoanApplicationRequest loanApplicationRequest { get; set; }
    }
}
