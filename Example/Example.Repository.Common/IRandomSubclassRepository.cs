using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Model;

namespace Example.Repository.Common
{
    public interface IRandomSubclassRepository
    {
        Task<List<RandomSubclassModel>> GetAll();
        Task<RandomSubclassModel> GetById(int id);
        Task<RandomSubclassModel> PostRandomSubclass(RandomSubclassModel RandomSubclass);
        Task PutRandomSubclass(int id, RandomSubclassModel RandomSubclass);
        Task DeleteRandomSubclass(int id);
    }
}
