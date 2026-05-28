 
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Domain.CustomView
{
    public class EmailTemplateLogoRequest
    {
        
        public long userID
        {
            get;
            set;
        }
        public long logoID
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
