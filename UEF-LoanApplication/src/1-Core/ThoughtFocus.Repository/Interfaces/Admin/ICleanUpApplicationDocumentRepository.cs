using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.Domain.Params;

namespace ThoughtFocus.Repository.Interfaces.Admin
{
    public interface ICleanUpApplicationDocumentRepository
    {
        Task<long> UpdateApplicationDocument(GrantApplication grantApplication);
    }
}
