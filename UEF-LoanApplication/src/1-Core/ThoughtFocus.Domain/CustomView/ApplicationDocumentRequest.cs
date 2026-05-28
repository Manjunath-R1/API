using System;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Domain.CustomView
{
    public class ApplicationDocumentRequest
    {
        public long ApplicationID
        {
            get;
            set;
        }
        public int documentCategoryID
        {
            get;
            set;
        }
        public long roleID
        {
            get;
            set;
        }
        public long userID
        {
            get;
            set;
        }
        public Guid documentID
        {
            get;
            set;
        }
        public string DocumentVersionHistoryKey
        {
            get;
            set;
        }
        public UserSessionEntity UserSessionEntity
        {
            get;
            set;
        }
    }
}
