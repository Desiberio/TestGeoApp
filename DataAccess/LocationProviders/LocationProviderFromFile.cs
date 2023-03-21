using GMap.NET;
using GMap.NET.WindowsForms;
using NMEAParser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class LocationProviderFromFile : ILocationProvider
    {
        public GMapMarker Marker { get; set; }
        private FileSystemWatcher _watcher;

        public event EventHandler<LocationChangedEventArgs> LocationChanged;

        public LocationProviderFromFile(string path)
        {
            string directory = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);
            if (string.IsNullOrEmpty(directory)) throw new Exception("Directory path is invalid or empty.");
            if(fileName == string.Empty)
            {
                _watcher = new FileSystemWatcher(directory);
            }
            else
            {
                _watcher = new FileSystemWatcher(directory, fileName);
            }

            _watcher.Changed += File_Changed;
        }

        private void File_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                var content = File.ReadAllText(e.FullPath);
                GPGGAMessage message = GPGGAMessage.Parse(content);
                PointLatLng newLocation = new PointLatLng(message.Latitude, message.Longitude);
                ChangeLocation(newLocation);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }
        }

        public void StartUpdatingData(GMapMarker marker)
        {
            if (marker != null)
            {
                Marker = marker;
            }
            else
            {
                throw new ArgumentNullException(nameof(marker));
            }
            _watcher.EnableRaisingEvents = true;
            
        }

        public void StopUpdatingData()
        {
            _watcher.EnableRaisingEvents = false;
            Marker = null;
        }

        public void ChangeLocation(PointLatLng newLocation)
        {
            Marker.Position = newLocation;
            LocationChanged?.Invoke(this, new LocationChangedEventArgs(Marker));
        }
    }

    public class LocationChangedEventArgs : EventArgs
    {
        public GMapMarker TrackedMarker { get; private set; }

        public LocationChangedEventArgs(GMapMarker trackedMarker)
        {
            TrackedMarker = trackedMarker;
        }
    }
}
