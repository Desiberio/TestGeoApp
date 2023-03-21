using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters);
        Task<IEnumerable<MarkerModel>> LoadData<T>(string storedProcedure);
        Task SaveData<T>(string storedProcedure, T parameters);
    }
}