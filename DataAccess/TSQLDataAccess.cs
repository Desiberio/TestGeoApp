using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;

namespace DataAccess
{
    public class TSQLDataAccess : ISqlDataAccess
    {
        private readonly string _connectionString;

        public TSQLDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<MarkerModel>> LoadMarkers(string storedProcedure)
        {
            List<MarkerModel> markers = new List<MarkerModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection) { CommandType = CommandType.StoredProcedure })
                {
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            markers.Add(new MarkerModel(new GMap.NET.PointLatLng(Convert.ToDouble(reader["Latitude"]), Convert.ToDouble(reader["Longitude"])))
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Type = Convert.ToInt32(reader["Type"])
                            });
                        }
                    }
                }
            }

            return markers;
        }

        public async Task SaveData(string storedProcedure, object parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection) { CommandType = CommandType.StoredProcedure })
                {
                    if (parameters is SqlParameter[] sqlParameters)
                    {
                        command.Parameters.Add(sqlParameters);
                    }
                    else
                    {
                        try
                        {
                            dynamic dynamicParameters = (dynamic)parameters;

                            command.Parameters.AddWithValue("@Id", dynamicParameters.Id);
                            command.Parameters.AddWithValue("@Latitude", dynamicParameters.Latitude);
                            command.Parameters.AddWithValue("@Longitude", dynamicParameters.Longitude);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Error while reading parameters has occured: {ex.Message}", ex);
                        }
                    }

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
