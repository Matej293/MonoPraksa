using System;
using Example.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example.Service.Common
{
    public interface IRandomSubclassService
    {
        Task Init();
        Task<List<RandomSubclassModel>> GetAll();
        Task<RandomSubclassModel> GetById(Guid id);
        Task PostCity(RandomSubclassModel city);
        Task PutCity(Guid id, RandomSubclassModel city);
        Task DeleteCity(Guid id);
    }
}
