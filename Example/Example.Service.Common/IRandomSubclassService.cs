using Example.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Service;

namespace Example.Service.Common
{
    public interface IRandomSubclassService
    {
        Task<List<RandomSubclassModel>> GetAll();
        Task<RandomSubclassModel> GetById(int id);
        Task PostCity(RandomSubclassModel city);
        Task PutCity(int id, RandomSubclassModel city);
        Task DeleteCity(int id);
    }
}
