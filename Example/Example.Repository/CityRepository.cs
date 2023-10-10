using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;
using NpgsqlTypes;
using Example.Model;
using Example.Repository.Common;
using Example.Model.Common;

namespace Example.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString;

        private readonly NpgsqlConnection connection;

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

                using(var cmd = new NpgsqlCommand(CONNECTION_STRING, connection))
                {
                    cmd.CommandText = $"CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";";
                    await cmd.ExecuteNonQueryAsync();


                    cmd.CommandText = $"CREATE TABLE IF NOT EXISTS \"{TABLE_NAME_2}\" (" +
                                      $"\"Id\" UUID DEFAULT uuid_generate_v4() PRIMARY KEY, " +
                                      $"\"RandomArg1\" VARCHAR NOT NULL, " +
                                      $"\"RandomArg2\" INTEGER NOT NULL" +
                                      $")";
                    await cmd.ExecuteNonQueryAsync();


                    cmd.CommandText = $"CREATE TABLE IF NOT EXISTS \"{TABLE_NAME}\" (" +
                                      $"\"Id\" UUID DEFAULT uuid_generate_v4() PRIMARY KEY, " +
                                      $"\"Name\" VARCHAR NOT NULL, " +
                                      $"\"Country\" VARCHAR NOT NULL, " +
                                      $"\"Population\" INTEGER NOT NULL, " +
                                      $"\"RandomSubclassId\" UUID," +
                                      $"FOREIGN KEY (\"RandomSubclassId\") REFERENCES \"{TABLE_NAME_2}\"(\"Id\")" +
                                      $")";
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<ICityModel>> GetAll()
        {
            List<ICityModel> cities = new List<ICityModel>();

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
                            ICityModel city = ReadCity(reader, includeEmbeds: true);
                            cities.Add(city);
                        }
                    }
                }
            }
            return cities;
        }

        //helper function
        public ICityModel ReadCity(NpgsqlDataReader reader, bool includeEmbeds = false)
        {
            Guid id = reader.GetGuid(reader.GetOrdinal("Id"));
            string name = reader.GetString(reader.GetOrdinal("Name"));
            string country = reader.GetString(reader.GetOrdinal("Country"));
            int population = reader.GetInt32(reader.GetOrdinal("Population"));
            Guid randomsubclassid = reader.GetGuid(reader.GetOrdinal("RandomSubclassId"));

            IRandomSubclassModel rand = null;

            if (includeEmbeds)
            {
                Guid id_2 = reader.GetGuid(reader.GetOrdinal("RandomSubclassId"));
                string arg1 = reader.GetString(reader.GetOrdinal("RandomArg1"));
                int arg2 = reader.GetInt32(reader.GetOrdinal("RandomArg2"));

                rand = new RandomSubclassModel
                {
                    Id = id_2,
                    RandomArg1 = arg1,
                    RandomArg2 = arg2
                };
            }

            ICityModel city = new CityModel
            {
                Id = id,
                Name = name,
                Country = country,
                Population = population,
                RandomSubclassId = randomsubclassid,
                RandomSubclass = rand
            };

            return city;
        }

        public async Task<ICityModel> GetById(Guid id, string embeds = null)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add(new NpgsqlParameter
                    {
                        ParameterName = "@Id",
                        NpgsqlDbType = NpgsqlDbType.Uuid,
                        Value = id
                    });

                    var select = $"SELECT * FROM \"{TABLE_NAME}\" c ";
                    var where = "WHERE \"Id\" = @Id";

                    if (!string.IsNullOrWhiteSpace(embeds) && embeds == "RandomSubclass")
                    {
                        select += $"INNER JOIN \"{embeds}\" r ON c.\"{embeds}Id\" = r.\"Id\"";
                    }

                    cmd.CommandText = select + where;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return ReadCity(reader, !string.IsNullOrWhiteSpace(embeds));
                        }
                    }
                }
            }
            
            return null;
        }

        public async Task<ICityModel> PostCity(ICityModel city)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                var commandText = $"INSERT INTO \"{TABLE_NAME}\" (\"Id\", \"Name\", \"Country\", \"Population\", \"RandomSubclassId\") VALUES (@Id, @Name, @Country, @Population, @RandomSubclassId)";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.Add(new NpgsqlParameter
                    {
                        ParameterName = "@Id",
                        NpgsqlDbType = NpgsqlDbType.Uuid,
                        Value = city.Id
                    });
                    cmd.Parameters.AddWithValue("@Name", city.Name);
                    cmd.Parameters.AddWithValue("@Country", city.Country);
                    cmd.Parameters.AddWithValue("@Population", city.Population);
                    cmd.Parameters.Add(new NpgsqlParameter
                    {
                        ParameterName = "@RandomSubclassId",
                        NpgsqlDbType = NpgsqlDbType.Uuid,
                        Value = city.RandomSubclassId
                    });

                    await cmd.ExecuteNonQueryAsync();
                }

                return city;
            }
        }


        public async Task PutCity(Guid id, ICityModel city)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                var commandText = $"UPDATE \"{TABLE_NAME}\" SET \"Name\" = @Name, \"Country\" = @Country, \"Population\" = @Population, \"RandomSubclassId\" = @RandomSubclassId " +
                    $"WHERE \"Id\" = @Id";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.Add(new NpgsqlParameter
                    {
                        ParameterName = "@Id",
                        NpgsqlDbType = NpgsqlDbType.Uuid,
                        Value = city.Id
                    });
                    cmd.Parameters.AddWithValue("@Name", city.Name);
                    cmd.Parameters.AddWithValue("@Country", city.Country);
                    cmd.Parameters.AddWithValue("@Population", city.Population);
                    cmd.Parameters.Add(new NpgsqlParameter
                    {
                        ParameterName = "@RandomSubclassId",
                        NpgsqlDbType = NpgsqlDbType.Uuid,
                        Value = city.RandomSubclassId
                    });

                    await cmd.ExecuteNonQueryAsync();
                }
                
            }
        }

        public async Task DeleteCity(Guid id)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                string commandText = $"DELETE FROM \"{TABLE_NAME}\" WHERE \"Id\" = @id";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.Add(new NpgsqlParameter
                    {
                        ParameterName = "@Id",
                        NpgsqlDbType = NpgsqlDbType.Uuid,
                        Value = id
                    });
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
