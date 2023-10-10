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
        Task<List<IRandomSubclassModel>> GetAll();
        Task<IRandomSubclassModel> GetById(Guid id);
        IRandomSubclassModel ReadFunc(NpgsqlDataReader reader, bool includeEmbeds = false);
        Task<IRandomSubclassModel> PostRandomSubclass(IRandomSubclassModel randomSubclass);
        Task PutRandomSubclass(Guid id, IRandomSubclassModel randomSubclass);
        Task DeleteRandomSubclass(Guid id);
    }
}
