namespace ThoughtFocus.DocumentRepository.Domain.Request
{
    public class TextSearchRequest
    {
        public string SearchText { get; set; }

        public string HighLighter { get; set; }
    }
}
