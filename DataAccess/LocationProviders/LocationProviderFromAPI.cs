using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DataAccess
{
    public class LocationProviderFromAPI : ILocationProvider
    {
        private Timer _timer;
        private GMapMarker _marker = null;
        private HttpClient _httpClient;
        private bool alreadyProcessing;

        public event EventHandler<LocationChangedEventArgs> LocationChanged;

        private string _url = @"http://localhost/gpsfeed";

        public LocationProviderFromAPI(string endPointUrl = null, double updateInterval = 1000)
        {
            if(endPointUrl != null) _url = endPointUrl;

            _timer = new Timer();
            _timer.Elapsed += Timer_Elapsed;
            _timer.Interval = updateInterval;
            _timer.Enabled = false;

            _httpClient = new HttpClient();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_marker == null) StopUpdatingData();
            if (alreadyProcessing) return;

            try
            {
                alreadyProcessing = true;
                HttpResponseMessage response = null;
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri(_url);
                    request.Method = HttpMethod.Get;
                    response = _httpClient.SendAsync(request).Result;
                }

                if (response.IsSuccessStatusCode == false) throw new Exception($"API call was unsuccesfull. Status code: {response.StatusCode}.");

                string content = response.Content.ReadAsStringAsync().Result;

                var data = content.Split(';');

                double longitude = Convert.ToDouble(data[0].Substring(4));
                double latitude = Convert.ToDouble(data[1].Substring(4));

                ChangeLocation(new PointLatLng(latitude, longitude));
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }
            finally { alreadyProcessing = false; }
            
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
            _timer.Start();
        }

        public void StopUpdatingData()
        {
            _timer.Stop();
            _marker = null;
        }

        public void ChangeLocation(PointLatLng newLocation)
        {
            _marker.Position = newLocation;
            LocationChanged?.Invoke(this, new LocationChangedEventArgs(_marker));
        }
    }
}
