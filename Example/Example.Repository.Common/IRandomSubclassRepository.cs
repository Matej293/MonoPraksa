using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Model;
using Example.Model.Common;
using Npgsql;

namespace Example.Repository.Common
{
    public interface IRandomSubclassRepository
    {
        Task InitializeDB();
        Task<List<RandomSubclassModel>> GetAll();
        Task<RandomSubclassModel> GetById(Guid id);
        RandomSubclassModel ReadFunc(NpgsqlDataReader reader);
        Task<RandomSubclassModel> PostRandomSubclass(RandomSubclassModel randomSubclass);
        Task PutRandomSubclass(Guid id, RandomSubclassModel randomSubclass);
        Task DeleteRandomSubclass(Guid id);
    }
}
