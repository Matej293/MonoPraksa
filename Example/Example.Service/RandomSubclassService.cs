using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Model;
using Example.Model.Common;
using Example.Repository.Common;
using Example.Service.Common;

namespace Example.Service
{
    public class RandomSubclassService : IRandomSubclassService
    {
        private readonly IRandomSubclassRepository _repository;

        public RandomSubclassService(IRandomSubclassRepository repository)
        {
            _repository = repository;
        }

        public async Task Init()
        {
            await _repository.InitializeDB();
        }

        public async Task<List<Model.Common.RandomSubclassModel>> GetAll()
        {
            List<Model.Common.RandomSubclassModel> list = await _repository.GetAll();
            return list;
        }

        public async Task<Model.Common.RandomSubclassModel> GetById(Guid id)
        {
            Model.Common.RandomSubclassModel list = await _repository.GetById(id);
            return list;
        }

        public async Task PostRandomSubclass(Model.Common.RandomSubclassModel randomSubclass)
        {
            await _repository.PostRandomSubclass(randomSubclass);
        }

        public async Task PutRandomSubclass(Guid id, Model.Common.RandomSubclassModel randomSubclass)
        {
            await _repository.PutRandomSubclass(id, randomSubclass);
        }

        public async Task DeleteRandomSubclass(Guid id)
        {
            await _repository.DeleteRandomSubclass(id);
        }
    }
}
