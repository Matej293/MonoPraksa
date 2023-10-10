using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Model.Common;

namespace Example.Service.Common
{
    public interface IRandomSubclassService
    {
        Task Init();
        Task<List<RandomSubclassModel>> GetAll();
        Task<RandomSubclassModel> GetById(Guid id);
        Task PostRandomSubclass(RandomSubclassModel city);
        Task PutRandomSubclass(Guid id, RandomSubclassModel city);
        Task DeleteRandomSubclass(Guid id);
    }
}
