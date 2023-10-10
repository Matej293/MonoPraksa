using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Model;
using Example.Model.Common;

namespace Example.Service.Common
{
    public interface ICityService
    {
        Task Init();
        Task<List<CityModel>> GetAll();
        Task<CityModel> GetById(Guid id);
        Task PostCity(ICityModel city);
        Task PutCity(Guid id, ICityModel city);
        Task DeleteCity(Guid id);
    }
}
