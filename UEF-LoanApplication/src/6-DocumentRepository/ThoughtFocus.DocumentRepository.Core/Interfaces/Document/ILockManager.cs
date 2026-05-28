using System;
using ThoughtFocus.DocumentRepository.Domain;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface ILockManager
    {
        DocumentBaseResponse LockDocument(long ImpersonatedUser, long userID, Guid documentID);

        DocumentBaseResponse UnlockDocument(long ImpersonatedUser, long userID, Guid documentID);
    }
}
