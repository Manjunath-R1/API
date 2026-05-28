using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain
{
    public interface IDocumentVisitor
    {
        DocumentResponse HandleApplicationDocument(Domain.CustomView.DocumentEntity documentEntity);
    }
}
