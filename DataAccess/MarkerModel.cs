using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MarkerModel : GMapMarker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public ILocationProvider LocationProvider { get; set; }

        public MarkerModel(PointLatLng pos) : base(pos)
        {
            Position = pos;
        }
    }
}
