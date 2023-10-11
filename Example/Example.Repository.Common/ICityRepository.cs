using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using Example.Model;
using Example.Model.Common;

namespace Example.Repository.Common
{
    public interface ICityRepository
    {
        Task InitializeDB();
        Task<List<CityModel>> GetAll();
        Task<CityModel> GetById(Guid id, string embeds = null);
        CityModel ReadCity(NpgsqlDataReader reader);
        Task<ICityModel> PostCity(ICityModel city);
        Task PutCity(Guid id, ICityModel city);
        Task DeleteCity(Guid id);
    }
}
