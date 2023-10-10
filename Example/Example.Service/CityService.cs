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
        private readonly ICityRepository _repository;

        public CityService(ICityRepository repository) 
        {
            _repository = repository;
        }


        public async Task Init()
        {
            await _repository.InitializeDB();
        }

        public async Task<List<CityModel>> GetAll()
        {
            List<CityModel> cities = await _repository.GetAll();
            return cities;
        }

        public async Task<CityModel> GetById(Guid id)
        {
            CityModel cities = await _repository.GetById(id);
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
