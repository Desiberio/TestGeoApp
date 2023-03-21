using GMap.NET.WindowsForms;
using System;

namespace DataAccess
{
    public interface ILocationProvider
    {
        event EventHandler<LocationChangedEventArgs> LocationChanged;

        void StartUpdatingData(GMapMarker marker);
        void StopUpdatingData();
    }
}