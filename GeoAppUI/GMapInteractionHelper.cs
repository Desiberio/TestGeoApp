using DataAccess;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoAppUI
{
    public class GMapInteractionHelper
    {
        private GMapControl _googleMap;
        private MarkersData _markersData;

        public GMapInteractionHelper(GMapControl googleMap)
        {
            //_markersData = new MarkersData(new SqlDapperDataAccess("Server=localhost\\SQLEXPRESS;Database=TestGeoAppMarkersDB;Trusted_Connection=True;"));
            _markersData = new MarkersData(new TSQLDataAccess("Server=localhost\\SQLEXPRESS;Database=TestGeoAppMarkersDB;Trusted_Connection=True;"));
            this._googleMap = googleMap;
        }

        public async void LoadMarkers(GMapControl googleMap)
        {
            var markers = (List<MarkerModel>) await _markersData.GetMarkers();
            foreach (var marker in markers)
            {
                GMarkerGoogle gMarker = CreateMarker(marker);
                googleMap.Overlays.First(x => x.Id == "Test").Markers.Add(gMarker);
            }
        }

        public GMarkerGoogle CreateMarker(MarkerModel data = null)
        {
            string description = data.Description;
            PointLatLng position = data.Position;
            GMarkerGoogleType type = (GMarkerGoogleType)data.Type;         

            GMarkerGoogle mapMarker = new GMarkerGoogle(position, type);
            mapMarker.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapBaloonToolTip(mapMarker);
            mapMarker.ToolTipText = description; // string.Format("{0},{1}", data.Position.Lat, data.Position.Lng);
            mapMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            mapMarker.Tag = data;
            
            return mapMarker;
        }

        public GMarkerGoogle CreateMarker(RectLatLng viewArea)
        {
            string description = "Случайно сгенерированный маркер.";
            double randomLat = new Random().NextDouble() * (viewArea.Lat - (viewArea.Lat - viewArea.HeightLat)) + (viewArea.Lat - viewArea.HeightLat);
            double randomLng = new Random().NextDouble() * (viewArea.Lng + viewArea.WidthLng - viewArea.Lng) + viewArea.Lng;
            PointLatLng position = new PointLatLng(randomLat, randomLng);
            
            GMarkerGoogleType type = (GMarkerGoogleType)new Random().Next(1, 38);

            GMarkerGoogle mapMarker = new GMarkerGoogle(position, type);
            mapMarker.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapBaloonToolTip(mapMarker);
            mapMarker.ToolTipText = description; // string.Format("{0},{1}", data.Position.Lat, data.Position.Lng);
            mapMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            mapMarker.Tag = null;

            return mapMarker;
        }

        public async void SaveMarkerPostion(MarkerModel marker)
        {
            await _markersData.UpdateMarkerCoordinates(marker);
        }

        public void ChangeMarkerColor(GMarkerGoogle gMarker)
        {
            Color randomColor = Color.FromArgb(255, Color.FromArgb(new Random().Next()));
            Bitmap coloredMarker = new Bitmap(gMarker.Bitmap);

            for (int i = 0; i < coloredMarker.Width; i++)
            {
                for (int j = 0; j < coloredMarker.Height; j++)
                {
                    int color = coloredMarker.GetPixel(i, j).ToArgb();
                    if (color == 0 || coloredMarker.GetPixel(i, j).GetBrightness() < 0.3) continue;
                    coloredMarker.SetPixel(i, j, randomColor);
                }
            }

            gMarker.Bitmap = coloredMarker;
        }

        public void ChangeConfiguration(AutoTrackingConfigurationWindow configurationWindow, GMapMarker marker)
        {
            ILocationProvider selectedLocationProvider = configurationWindow.SelectedLocationProvider;
            (marker.Tag as MarkerModel).LocationProvider = selectedLocationProvider;
            selectedLocationProvider.LocationChanged += CheckOnLocationChanged;
            if (configurationWindow.SaveDataToDb == true)
            {
                selectedLocationProvider.LocationChanged += SaveOnLocationChanged;
            }
        }

        private void SaveOnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            var marker = e.TrackedMarker as GMarkerGoogle;
            if (marker != null && marker.Tag != null)
            {
                var markerData = marker.Tag as MarkerModel;
                markerData.Position = marker.Position;
                SaveMarkerPostion(markerData);
            }
        }

        private void CheckOnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            var marker = e.TrackedMarker as GMarkerGoogle;
            if (marker != null && marker.Tag != null)
                CheckIfInsidePolygon(marker, _googleMap.Overlays[0]);
        }

        public void CheckIfInsidePolygon(GMapMarker marker, GMapOverlay overlay)
        {
            foreach (var polygon in overlay.Polygons)
            {
                if (polygon.IsInside(marker.Position))
                {
                    PolygonEnterActionHandler(polygon, marker);
                    return;
                }
            }
        }

        private void PolygonEnterActionHandler(GMapPolygon polygon, GMapMarker marker)
        {
            PolygonEnterAction action = (PolygonEnterAction)polygon.Tag;
            GMarkerGoogle gMarker = marker as GMarkerGoogle;
            if (gMarker == null) throw new Exception("Map marker is not a GMarkerGoogle type.");
            MarkerModel markerData = gMarker.Tag as MarkerModel;

            switch (action)
            {
                case PolygonEnterAction.ShowMessageBox:
                    MessageBox.Show($"Маркер \"{markerData.Name}\" находится в особой зоне ({polygon.Name})!");
                    break;
                case PolygonEnterAction.ChangeMarkerColor:
                    ChangeMarkerColor(gMarker);
                    break;
                case PolygonEnterAction.CreateNewRandomMarkerInViewArea:
                    _googleMap.Overlays.First(x => x.Id == "Test").Markers.Add(CreateMarker(_googleMap.ViewArea));
                    break;
            }
        }
    }
}
