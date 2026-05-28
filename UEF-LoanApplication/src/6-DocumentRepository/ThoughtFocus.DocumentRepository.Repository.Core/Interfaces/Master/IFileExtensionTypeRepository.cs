using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface IFileExtensionTypeRepository //: IBaseRepository<FileExtensionType>
    {
        List<FileExtensionType> GetAll();
        FileExtensionType GetFileExtensionByValue(string value);
        FileExtensionType GetExtensionTypeByID(Guid typeID);

        void SaveFileExtensionType(FileExtensionType fileExtensionCategory);
    }
}
