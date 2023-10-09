using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Service.Common;
using Example.Repository;
using Example.Model;

namespace Example.Service
{
    public class CityService : ICityService
    {
        public CityService() 
        {
            repository = new CityRepository();
        }

        private readonly CityRepository repository;

        public async Task Init()
        {
            await repository.InitializeDB();
        }

        public async Task<List<CityModel>> GetAll()
        {
            List<CityModel> cities = await repository.GetAll();
            return cities;
        }

        public async Task<CityModel> GetById(Guid id)
        {
            CityModel cities = await repository.GetById(id);
            return cities;
        }

        public async Task PostCity(CityModel city)
        {
            await repository.PostCity(city);
        }

        public async Task PutCity(Guid id, CityModel city)
        {
            await repository.PutCity(id, city);
        }

        public async Task DeleteCity(Guid id)
        {
            await repository.DeleteCity(id);
        }
    }
}
