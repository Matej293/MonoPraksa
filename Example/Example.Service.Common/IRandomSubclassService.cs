using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Model.Common;

namespace Example.Service.Common
{
    public interface IRandomSubclassService
    {
        Task Init();
        Task<List<IRandomSubclassModel>> GetAll();
        Task<IRandomSubclassModel> GetById(Guid id);
        Task PostRandomSubclass(IRandomSubclassModel city);
        Task PutRandomSubclass(Guid id, IRandomSubclassModel city);
        Task DeleteRandomSubclass(Guid id);
    }
}
