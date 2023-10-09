using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Example.Model;

namespace Example.Repository
{
    public class RandomSubclassRepository
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString;

        private NpgsqlConnection connection;

        private const string TABLE_NAME = "City";
        private const string TABLE_NAME_2 = "RandomSubclass";

        public RandomSubclassRepository()
        {
            connection = new NpgsqlConnection(CONNECTION_STRING);
        }

        public async Task<List<RandomSubclassModel>> GetAll()
        {
            List<RandomSubclassModel> list = new List<RandomSubclassModel>();
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                string commandText = $"SELECT * FROM \"{TABLE_NAME_2}\"";

                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            RandomSubclassModel RandomSubclass = ReadFunc(reader);
                            list.Add(RandomSubclass);
                        }
                    }
                }
            }
            connection.Close();
            return list;
        }

        //helper function
        public RandomSubclassModel ReadFunc(NpgsqlDataReader reader, bool includeEmbeds = false)
        {
            int id = reader.GetInt32(reader.GetOrdinal("Id"));
            string arg1 = reader.GetString(reader.GetOrdinal("RandomArg1"));
            int arg2 = reader.GetInt32(reader.GetOrdinal("RandomArg2"));

            RandomSubclassModel rand = new RandomSubclassModel();

            rand = new RandomSubclassModel
            {
                Id = id,
                RandomArg1 = arg1,
                RandomArg2 = arg2
            };

            return rand;
        }

        public async Task<RandomSubclassModel> GetById(int id)
        {
            List<RandomSubclassModel> list = new List<RandomSubclassModel>();

            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                var cmd = new NpgsqlCommand($"SELECT * FROM \"{TABLE_NAME_2}\" WHERE \"Id\" = @Id", connection);

                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            RandomSubclassModel RandomSubclass = ReadFunc(reader);
                            list.Add(RandomSubclass);
                        }
                    }
                }
            }
            connection.Close();
            return list[0];
        }


        public async Task<RandomSubclassModel> PostRandomSubclass(RandomSubclassModel RandomSubclass)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                var commandText = $"INSERT INTO \"{TABLE_NAME_2}\" (\"Id\", \"RandomArg1\", \"RandomArg2\") VALUES (@Id, @RandomArg1, @RandomArg2)";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", RandomSubclass.Id);
                    cmd.Parameters.AddWithValue("@RandomArg1", RandomSubclass.RandomArg1);
                    cmd.Parameters.AddWithValue("@RandomArg2", RandomSubclass.RandomArg2);

                    await cmd.ExecuteNonQueryAsync();
                }

                connection.Close();

                return RandomSubclass;
            }
        }

        public async Task PutRandomSubclass(int id, RandomSubclassModel RandomSubclass)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                var commandText = $"UPDATE \"{TABLE_NAME_2}\" SET \"RandomArg1\" = @RandomArg1, \"RandomArg2\" = @RandomArg2 " +
                    $"WHERE \"Id\" = @Id";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", RandomSubclass.Id);
                    cmd.Parameters.AddWithValue("@RandomArg1", RandomSubclass.RandomArg1);
                    cmd.Parameters.AddWithValue("@RandomArg2", RandomSubclass.RandomArg2);

                    await cmd.ExecuteNonQueryAsync();
                }
                connection.Close();
            }
        }

        public async Task DeleteRandomSubclass(int id)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                string commandText = $"DELETE FROM \"{TABLE_NAME_2}\" WHERE \"Id\" = @id";

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

