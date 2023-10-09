using Example.Model;
using Example.Repository;
using System;
using System.Collections.Generic;
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

        public async Task Init()
        {
            await repository.InitializeDB();
        }

        public async Task<List<RandomSubclassModel>> GetAll()
        {
            List<RandomSubclassModel> list = await repository.GetAll();
            return list;
        }

        public async Task<RandomSubclassModel> GetById(Guid id)
        {
            RandomSubclassModel list = await repository.GetById(id);
            return list;
        }

        public async Task PostRandomSubclass(RandomSubclassModel RandomSubclass)
        {
            await repository.PostRandomSubclass(RandomSubclass);
        }

        public async Task PutRandomSubclass(Guid id, RandomSubclassModel RandomSubclass)
        {
            await repository.PutRandomSubclass(id, RandomSubclass);
        }

        public async Task DeleteRandomSubclass(Guid id)
        {
            await repository.DeleteRandomSubclass(id);
        }
    }
}
