using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models;

namespace ThoughtFocus.Repository.Interfaces.Master
{
    public interface IDocumentTypeRepository : IEFApplicationBaseRepository<DocumentType>
    {
        List<DocumentType> GetDocumentTypeByCategoryID(int DocumentCategoryID);

        /// This method is used to get documents using documentTypeID
        /// </summary>
        /// <param name="documentTypeID">documentTypeID</param> 
        /// <returns>Questions response</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        DocumentType GetDocumentTypeByID(int documentTypeID);


        /// This method is used to save and update document types
        /// </summary>
        /// <param name="documentType">documentType</param> 
        /// <param name="userID">userID</param> 
        /// <returns>common response</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateDocumentTypes(DocumentType documentType, long? userID);

        /// This method is used to get document name
        /// </summary>
        /// <param name="documentName">documentName</param> 
        /// <returns>Questions response</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        DocumentType GetDocumentName(string documentName);
    }

}