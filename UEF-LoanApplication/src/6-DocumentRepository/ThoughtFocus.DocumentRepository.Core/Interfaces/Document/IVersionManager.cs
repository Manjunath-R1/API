using System;
using ThoughtFocus.DocumentRepository.Domain;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IVersionManager
    {
        VersionEntity GetNextVersion(Guid documentID);

    }
}
