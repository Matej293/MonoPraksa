using Example.Model;
using Example.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service
{
    public class RandomSubclassService
    {
        public RandomSubclassService()
        {
            repository = new RandomSubclassRepository();
        }

        private RandomSubclassRepository repository;

        public async Task<List<RandomSubclassModel>> GetAll()
        {
            List<RandomSubclassModel> list = await repository.GetAll();
            return list;
        }

        public async Task<RandomSubclassModel> GetById(int id)
        {
            RandomSubclassModel list = await repository.GetById(id);
            return list;
        }

        public async Task PostRandomSubclass(RandomSubclassModel RandomSubclass)
        {
            await repository.PostRandomSubclass(RandomSubclass);
        }

        public async Task PutRandomSubclass(int id, RandomSubclassModel RandomSubclass)
        {
            await repository.PutRandomSubclass(id, RandomSubclass);
        }

        public async Task DeleteRandomSubclass(int id)
        {
            await repository.DeleteRandomSubclass(id);
        }
    }
}
