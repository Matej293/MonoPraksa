using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Model;
using Example.Model.Common;

namespace Example.Service.Common
{
    public interface ICityService
    {
        Task<List<CityModel>> GetAll();
        Task<CityModel> GetById(int id);
        Task PostCity(CityModel city);
        Task PutCity(int id, CityModel city);
        Task DeleteCity(int id);
    }
}
