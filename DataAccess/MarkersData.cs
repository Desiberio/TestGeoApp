using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MarkersData
    {
        private readonly ISqlDataAccess _db;

        public MarkersData(ISqlDataAccess dataAccess)
        {
            _db = dataAccess;
        }

        //delete, add, get single
        public Task<IEnumerable<MarkerModel>> GetMarkers() => _db.LoadData<MarkerModel>("dbo.spMarkers_GetAll");
        public Task UpdateMarkerCoordinates(MarkerModel marker) => _db.SaveData("dbo.spMarkers_UpdateCoordinates", new { marker.Id, Latitude = marker.Position.Lat, Longtitude = marker.Position.Lng });
    }
}
