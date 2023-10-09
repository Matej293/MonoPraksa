using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Example.Repository;
using Example.Model;
using Example.Model.Common;
using Example.Common;
using Npgsql;

namespace Example.Repository.Common
{
    public interface ICityRepository
    {
        Task InitializeDB();
        Task<List<CityModel>> GetAll();
        Task<CityModel> GetById(int id, string embeds = null);
        CityModel ReadCity(NpgsqlDataReader reader, bool includeEmbeds = false);
        Task<CityModel> PostCity(CityModel city);
        Task PutCity(int id, CityModel city);
        Task DeleteCity(int id);
    }
}
