using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using Example.Model;
using Example.Model.Common;
using Example.Repository.Common;

namespace Example.Repository
{
    public class RandomSubclassRepository : IRandomSubclassRepository
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString;

        private readonly NpgsqlConnection connection;

        private const string TABLE_NAME = "City";
        private const string TABLE_NAME_2 = "RandomSubclass";

        public RandomSubclassRepository()
        {
            connection = new NpgsqlConnection(CONNECTION_STRING);
        }

        public async Task InitializeDB()
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand(CONNECTION_STRING, connection))
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

        public async Task<List<Model.Common.RandomSubclassModel>> GetAll()
        {
            List<Model.Common.RandomSubclassModel> list = new List<Model.Common.RandomSubclassModel>();

            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                string commandText = $"SELECT * FROM \"{TABLE_NAME_2}\"";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Model.Common.RandomSubclassModel randomSubclass = ReadFunc(reader);
                            list.Add(randomSubclass);
                        }
                    }
                }
            }
            connection.Close();
            return list;
        }

        //helper function
        public Model.Common.RandomSubclassModel ReadFunc(NpgsqlDataReader reader, bool includeEmbeds = false)
        {
            var id = reader.GetGuid(reader.GetOrdinal("Id"));
            string arg1 = reader.GetString(reader.GetOrdinal("RandomArg1"));
            int arg2 = reader.GetInt32(reader.GetOrdinal("RandomArg2"));

            Model.Common.RandomSubclassModel rand = new Model.RandomSubclassModel
            {
                Id = id,
                RandomArg1 = arg1,
                RandomArg2 = arg2
            };

            return rand;
        }

        public async Task<Model.Common.RandomSubclassModel> GetById(Guid id)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                var commandText = $"SELECT * FROM \"{TABLE_NAME_2}\" WHERE \"Id\" = @Id";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return ReadFunc(reader);
                        }
                    }
                }
            }
            return null;
        }



        public async Task<Model.Common.RandomSubclassModel> PostRandomSubclass(Model.Common.RandomSubclassModel randomSubclass)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                var commandText = $"INSERT INTO \"{TABLE_NAME_2}\" (\"Id\", \"RandomArg1\", \"RandomArg2\") VALUES (@Id, @RandomArg1, @RandomArg2)";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.Add(new NpgsqlParameter
                    {
                        ParameterName = "@Id",
                        NpgsqlDbType = NpgsqlDbType.Uuid,
                        Value = randomSubclass.Id
                    });

                    cmd.Parameters.AddWithValue("@RandomArg1", randomSubclass.RandomArg1);
                    cmd.Parameters.AddWithValue("@RandomArg2", randomSubclass.RandomArg2);

                    await cmd.ExecuteNonQueryAsync();
                }
                
                return randomSubclass;
            }
        }

        public async Task PutRandomSubclass(Guid id, Model.Common.RandomSubclassModel randomSubclass)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                var commandText = $"UPDATE \"{TABLE_NAME_2}\" SET \"RandomArg1\" = @RandomArg1, \"RandomArg2\" = @RandomArg2 " +
                    $"WHERE \"Id\" = @Id";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.Add(new NpgsqlParameter
                    {
                        ParameterName = "@Id",
                        NpgsqlDbType = NpgsqlDbType.Uuid,
                        Value = randomSubclass.Id
                    });
                    cmd.Parameters.AddWithValue("@RandomArg1", randomSubclass.RandomArg1);
                    cmd.Parameters.AddWithValue("@RandomArg2", randomSubclass.RandomArg2);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteRandomSubclass(Guid id)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                string commandText = $"DELETE FROM \"{TABLE_NAME_2}\" WHERE \"Id\" = @id";

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

