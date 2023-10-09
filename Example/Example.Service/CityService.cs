using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Repository.Common;
using Example.Service.Common;
using Example.Repository;
using Example.Model;
using System.Net.NetworkInformation;

namespace Example.Service
{
    public class CityService : ICityService
    {
        public CityService() 
        {
            repository = new CityRepository();
            //repository.InitializeDB();
        }

        private CityRepository repository;

        public async Task<List<CityModel>> GetAll()
        {
            List<CityModel> cities = await repository.GetAll();
            return cities;
        }

        public async Task<CityModel> GetById(int id)
        {
            CityModel cities = await repository.GetById(id);
            return cities;
        }

        public async Task PostCity(CityModel city)
        {
            await repository.PostCity(city);
        }

        public async Task PutCity(int id, CityModel city)
        {
            await repository.PutCity(id, city);
        }

        public async Task DeleteCity(int id)
        {
            await repository.DeleteCity(id);
        }
    }
}
