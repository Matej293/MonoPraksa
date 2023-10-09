using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Repository.Common;
using Example.Model;
using System.Configuration;
using System.Data.Common;
using Npgsql;
using System.Net.Http;
using Example.Common;


namespace Example.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString;

        private NpgsqlConnection connection;

        private const string TABLE_NAME = "City";
        private const string TABLE_NAME_2 = "RandomSubclass";

        public CityRepository()
        {
             connection = new NpgsqlConnection(CONNECTION_STRING);
        }


        public async Task InitializeDB()
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                var sql = $"CREATE TABLE IF NOT EXISTS \"{TABLE_NAME_2}\"" +
                                $"(" +
                                $"\"Id\" serial PRIMARY KEY, " +
                                $"\"RandomArg1\" VARCHAR NOT NULL, " +
                                $"\"RandomArg2\" INTEGER NOT NULL" +
                                $")";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    await cmd.ExecuteNonQueryAsync();
                }

                sql = $"CREATE TABLE IF NOT EXISTS \"{TABLE_NAME}\"" +
                        $"(" +
                        $"\"Id\" serial PRIMARY KEY, " +
                        $"\"Name\" VARCHAR NOT NULL, " +
                        $"\"Country\" VARCHAR NOT NULL, " +
                        $"\"Population\" INTEGER NOT NULL, " +
                        $"\"RandomSubclassId\" INTEGER NOT NULL REFERENCES \"{TABLE_NAME_2}\"(\"Id\")" +
                        $")";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task<List<CityModel>> GetAll()
        {
            List<CityModel> cities = new List<CityModel>();
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                string commandText = $"SELECT * FROM \"{TABLE_NAME}\"";

                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                {

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            CityModel city = ReadCity(reader);
                            cities.Add(city);
                        }
                    }
                }
            }
            connection.Close();
            return cities;
        }

        //helper function
        public CityModel ReadCity(NpgsqlDataReader reader, bool includeEmbeds = false)
        {
            int id = reader.GetInt32(reader.GetOrdinal("Id"));
            string name = reader.GetString(reader.GetOrdinal("Name"));
            string country = reader.GetString(reader.GetOrdinal("Country"));
            int population = reader.GetInt32(reader.GetOrdinal("Population"));
            int randomsubclassid = reader.GetInt32(reader.GetOrdinal("RandomSubclassId"));


            RandomSubclassModel rand = new RandomSubclassModel();

            if (includeEmbeds)
            {
                int id_2 = reader.GetInt32((int)reader[6]);
                string arg1 = reader.GetString(reader.GetOrdinal("RandomArg1"));
                int arg2 = reader.GetInt32(reader.GetOrdinal("RandomArg2"));

                rand = new RandomSubclassModel
                {
                    Id = id_2,
                    RandomArg1 = arg1,
                    RandomArg2 = arg2
                };
            }

            CityModel city = new CityModel
            {
                Id = id,
                Name = name,
                Country = country,
                Population = population,
                RandomSubclassId = randomsubclassid
            };

            city.RandomSubclassModel = rand;

            return city;
        }

        public async Task<CityModel> GetById(int id, string embeds = null)
        {
            List<CityModel> list = new List<CityModel>();

            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                var cmd = new NpgsqlCommand();

                var test = $"SELECT * FROM \"{TABLE_NAME}\" c ";
                var where = "WHERE \"Id\" = @Id";

                if (!string.IsNullOrWhiteSpace(embeds) && embeds == "RandomSubclass")
                {
                    string commandText = $"{test}" +
                        $"INNER JOIN \"{embeds}\" r ON c.\"{embeds}Id\" = r.\"Id\"";

                    cmd.CommandText = commandText + where;
                }
                else
                {
                    cmd.CommandText = test + where;
                }

                using (cmd)
                {
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            CityModel city = ReadCity(reader, !string.IsNullOrWhiteSpace(embeds));
                            list.Add(city);
                        }
                    }
                }
            }
            connection.Close();
            return list[0];
        }

        public async Task<CityModel> PostCity(CityModel city)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                var commandText = $"INSERT INTO \"{TABLE_NAME}\" (\"Id\", \"Name\", \"Country\", \"Population\", \"RandomSubclassId\") VALUES (@Id, @Name, @Country, @Population, @RandomSubclassId)";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", city.Id);
                    cmd.Parameters.AddWithValue("@Name", city.Name);
                    cmd.Parameters.AddWithValue("@Country", city.Country);
                    cmd.Parameters.AddWithValue("@Population", city.Population);
                    cmd.Parameters.AddWithValue("@RandomSubclassId", city.RandomSubclassId);

                    await cmd.ExecuteNonQueryAsync();
                }

                connection.Close();

                return city;
            }
        }


        public async Task PutCity(int id, CityModel city)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                var commandText = $"UPDATE \"{TABLE_NAME}\" SET \"Name\" = @Name, \"Country\" = @Country, \"Population\" = @Population, \"RandomSubclassId\" = @RandomSubclassId " +
                    $"WHERE \"Id\" = @Id";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", city.Id);
                    cmd.Parameters.AddWithValue("@Name", city.Name);
                    cmd.Parameters.AddWithValue("@Country", city.Country);
                    cmd.Parameters.AddWithValue("@Population", city.Population);
                    cmd.Parameters.AddWithValue("@RandomSubclassId", city.RandomSubclassId);

                    await cmd.ExecuteNonQueryAsync();
                }
                connection.Close();
            }
        }

        public async Task DeleteCity(int id)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                string commandText = $"DELETE FROM \"{TABLE_NAME}\" WHERE \"Id\" = @id";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
                connection.Close();
            }
        }
    }
}
