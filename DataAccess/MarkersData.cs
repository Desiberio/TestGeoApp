using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MarkersData
    {
        //delete, add, get single
        public static async Task<IEnumerable<MarkerModel>> GetMarkers(SqlDapperDataAccess db) => await db.LoadMarkers("dbo.spMarkers_GetAll");
        public static async Task UpdateMarkerCoordinates(MarkerModel marker, SqlDapperDataAccess db) => await db.SaveData("dbo.spMarkers_UpdateCoordinates", new { marker.Id, Latitude = marker.Position.Lat, Longitude = marker.Position.Lng });

        public static async Task<IEnumerable<MarkerModel>> GetMarkers(TSQLDataAccess db) => await db.LoadMarkers("dbo.spMarkers_GetAll");
        public static async Task UpdateMarkerCoordinates(MarkerModel marker, TSQLDataAccess db) => await db.SaveData("dbo.spMarkers_UpdateCoordinates",
            new SqlParameter[] 
            {
                new SqlParameter("@Id", marker.Id),
                new SqlParameter("@Latitude", marker.Position.Lat),
                new SqlParameter("@Longitude", marker.Position.Lng) 
            });
    }
}
