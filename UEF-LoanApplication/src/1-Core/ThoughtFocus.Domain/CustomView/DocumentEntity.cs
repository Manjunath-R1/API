using System;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain.CustomView
{
    public abstract class DocumentEntity
    {
        public long EntityId { get; set; }
        public int EntityType { get; set; }
        public Guid DocumentID { get; set; }
        public long UserID { get; set; }
        public FileEntity FileEntity { get; set; }
        public Guid ProjectID { get; set; }

        public int DocumentTypeID { get; set; }

        public string DocumentName { get; set; }

        public abstract DocumentEntityTypeEnumeration DocumentEntityType { get; }
        public string StorageKey { get; set; }
        public string ParentFolderName { get; set; }
        public long ProgramInvitationID { get; set; }
        public abstract DocumentResponse AcceptDocument(IDocumentVisitor visitor);
    }

    public class ApplicationDocumentEntity : DocumentEntity
    {
        public override DocumentEntityTypeEnumeration DocumentEntityType
        {
            get { return DocumentEntityTypeEnumeration.ApplicationDocument; }
        }

        public override DocumentResponse AcceptDocument(IDocumentVisitor visitor)
        {
            return visitor.HandleApplicationDocument(this);
        }
    } 
       
}
