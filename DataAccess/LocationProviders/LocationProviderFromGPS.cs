using GMap.NET;
using GMap.NET.WindowsForms;
using NMEAParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace DataAccess
{
    public class LocationProviderFromGPS : ILocationProvider
    {
        private Timer _timer;
        private GMapMarker _marker = null;
        private SerialPort _gpsSerialPort;
        private bool alreadyProcessing = false;


        public event EventHandler<LocationChangedEventArgs> LocationChanged;

        public LocationProviderFromGPS(string portName, double updateInterval)
        {
            _timer = new Timer();
            _timer.Elapsed += Timer_Elapsed;
            _timer.Interval = updateInterval;
            _timer.Enabled = false;

            _gpsSerialPort = new SerialPort();
            _gpsSerialPort.PortName = portName;
            _gpsSerialPort.Parity = Parity.None;
            _gpsSerialPort.StopBits = StopBits.One;
            _gpsSerialPort.DataBits = 8;

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(_marker == null) StopUpdatingData();
            if(alreadyProcessing) return;
            if(_gpsSerialPort.IsOpen)
            {
                try
                {
                    alreadyProcessing = true;
                    string data = _gpsSerialPort.ReadExisting();
                    string[] messages = Regex.Split(data, @"(?=\$)");
                    string lastInfo = messages.Last(x => x.Contains("$GPGGA"));
                    GPGGAMessage message = GPGGAMessage.Parse(lastInfo);
                    ChangeLocation(new PointLatLng(message.Latitude, message.Longitude));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally { alreadyProcessing = false; }
                
            }
        }

        private void ChangeLocation(PointLatLng newLocation)
        {
            _marker.Position = newLocation;
            LocationChanged?.Invoke(this, new LocationChangedEventArgs(_marker));
        }

        public void StartUpdatingData(GMapMarker marker)
        {
            if (marker != null)
            {
                _marker = marker;
            }
            else
            {
                throw new ArgumentNullException(nameof(marker));
            }
            _gpsSerialPort.Open();
            _timer.Start();
        }

        public void StopUpdatingData()
        {
            _timer.Stop();
            _gpsSerialPort.Close();
            _marker = null;
        }
    }
}
