using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Service.Common;
using Example.Model;
using Example.Repository.Common;
using Example.Model.Common;

namespace Example.Service
{
    public class CityService : ICityService
    {
        private ICityRepository _repository;

        public CityService(ICityRepository repository) 
        {
            _repository = repository;
        }


        public async Task Init()
        {
            await _repository.InitializeDB();
        }

        public async Task<List<ICityModel>> GetAll()
        {
            List<ICityModel> cities = await _repository.GetAll();
            return cities;
        }

        public async Task<ICityModel> GetById(Guid id)
        {
            ICityModel cities = await _repository.GetById(id);
            return cities;
        }

        public async Task PostCity(ICityModel city)
        {
            await _repository.PostCity(city);
        }

        public async Task PutCity(Guid id, ICityModel city)
        {
            await _repository.PutCity(id, city);
        }

        public async Task DeleteCity(Guid id)
        {
            await _repository.DeleteCity(id);
        }
    }
}
