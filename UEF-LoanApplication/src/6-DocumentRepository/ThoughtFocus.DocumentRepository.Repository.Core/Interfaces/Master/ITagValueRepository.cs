using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface ITagValueRepository : IBaseRepository<TagValue>
    {
        void SaveTagValue(List<TagValue> tagValues);

        void SaveTagValue(TagValue tagValue);

        List<TagValue> GetTagValueListById(Guid TagID);

        TagValue GetTagValueById(Guid TagID, Guid TagValueID);
    }
}
