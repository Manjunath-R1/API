using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface IFileExtensionCategoryRepository //: IBaseRepository<FileExtensionCategory>
    {
        List<FileExtensionCategory> GetAll();
        void SaveFileExtensionCategory(FileExtensionCategory fileExtensionCategory);
    }
}
