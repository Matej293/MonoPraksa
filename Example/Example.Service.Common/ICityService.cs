using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Model;

namespace Example.Service.Common
{
    public interface ICityService
    {
        Task Init();
        Task<List<CityModel>> GetAll();
        Task<CityModel> GetById(Guid id);
        Task PostCity(CityModel city);
        Task PutCity(Guid id, CityModel city);
        Task DeleteCity(Guid id);
    }
}
