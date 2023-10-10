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
        Task<List<Model.Common.RandomSubclassModel>> GetAll();
        Task<Model.Common.RandomSubclassModel> GetById(Guid id);
        Model.Common.RandomSubclassModel ReadFunc(NpgsqlDataReader reader, bool includeEmbeds = false);
        Task<Model.Common.RandomSubclassModel> PostRandomSubclass(Model.Common.RandomSubclassModel randomSubclass);
        Task PutRandomSubclass(Guid id, Model.Common.RandomSubclassModel randomSubclass);
        Task DeleteRandomSubclass(Guid id);
    }
}
