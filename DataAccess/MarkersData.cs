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
        private readonly ISqlDataAccess _db;

        public MarkersData(ISqlDataAccess db)
        {
            _db = db;
        }
        //delete, add, get single
        public async Task<IEnumerable<MarkerModel>> GetMarkers() => await _db.LoadMarkers("dbo.spMarkers_GetAll");
        public async Task UpdateMarkerCoordinates(MarkerModel marker) => await _db.SaveData("dbo.spMarkers_UpdateCoordinates", new { marker.Id, Latitude = marker.Position.Lat, Longitude = marker.Position.Lng });
    }
}
