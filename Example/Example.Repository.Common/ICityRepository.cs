using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Model;
using Npgsql;

namespace Example.Repository.Common
{
    public interface ICityRepository
    {
        Task InitializeDB();
        Task<List<CityModel>> GetAll();
        Task<CityModel> GetById(Guid id, string embeds = null);
        CityModel ReadCity(NpgsqlDataReader reader, bool includeEmbeds = false);
        Task<CityModel> PostCity(CityModel city);
        Task PutCity(Guid id, CityModel city);
        Task DeleteCity(Guid id);
    }
}
