using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface ITagTypeRepository : IBaseRepository<TagType>
    {
        void SaveTagTypes(List<TagType> tagTypes);

        void SaveTagType(TagType tagType);
    }
}
