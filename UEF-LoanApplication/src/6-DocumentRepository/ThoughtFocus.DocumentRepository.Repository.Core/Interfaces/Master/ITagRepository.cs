using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface ITagRepository //: IBaseRepository<Tag>
    {
        void Save(Tag tag);
        List<Tag> GetAll();
        Tag GetTagByName(string tagName);
        IList<Tag> SearchByName(string tagSearchText);
    }
}
