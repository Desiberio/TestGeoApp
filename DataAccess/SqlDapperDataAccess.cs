using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.Entity.Infrastructure;
using GMap.NET;

namespace DataAccess
{
    public class SqlDapperDataAccess
    {
        private readonly string _connectionString;

        public SqlDapperDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
                return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<MarkerModel>> LoadMarkers(string storedProcedure)
        {
            using (IDbConnection connection = new SqlConnectionFactory().CreateConnection(_connectionString))
            {
                var data = await connection.QueryAsync<dynamic>(storedProcedure, commandType: CommandType.StoredProcedure);
                List<MarkerModel> markers = data.Select(row => new MarkerModel(new GMap.NET.PointLatLng(Convert.ToDouble(row.Latitude), Convert.ToDouble(row.Longtitude)))
                    {
                        Id = row.Id,
                        Name = row.Name,
                        Description = row.Description,
                        Type = row.Type
                    }).AsList();
                return markers;
            }
        }

        public async Task SaveData<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
