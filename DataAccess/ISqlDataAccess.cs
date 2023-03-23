using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<MarkerModel>> LoadMarkers(string storedProcedure);
        Task SaveData(string storedProcedure, object parameters);
    }
}