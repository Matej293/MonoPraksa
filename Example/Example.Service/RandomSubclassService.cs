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
        private IRandomSubclassRepository _repository;

        public RandomSubclassService(IRandomSubclassRepository repository)
        {
            _repository = repository;
        }


        public async Task Init()
        {
            await _repository.InitializeDB();
        }

        public async Task<List<IRandomSubclassModel>> GetAll()
        {
            List<IRandomSubclassModel> list = await _repository.GetAll();
            return list;
        }

        public async Task<IRandomSubclassModel> GetById(Guid id)
        {
            IRandomSubclassModel list = await _repository.GetById(id);
            return list;
        }

        public async Task PostRandomSubclass(IRandomSubclassModel randomSubclass)
        {
            await _repository.PostRandomSubclass(randomSubclass);
        }

        public async Task PutRandomSubclass(Guid id, IRandomSubclassModel randomSubclass)
        {
            await _repository.PutRandomSubclass(id, randomSubclass);
        }

        public async Task DeleteRandomSubclass(Guid id)
        {
            await _repository.DeleteRandomSubclass(id);
        }
    }
}
