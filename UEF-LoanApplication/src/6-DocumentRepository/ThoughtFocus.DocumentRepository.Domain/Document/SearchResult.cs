namespace ThoughtFocus.DocumentRepository.Domain
{
    public class SearchResult
    {
        public string SearchResponseID { get; set; }
        public string content { get; set; }
        public string content_encoding { get; set; }
        public string content_type { get; set; }
        public string resourcename { get; set; }
        public string Highlight { get; set; }
    }
}
