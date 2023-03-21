using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using System.Threading;
using System.Diagnostics;
using System.IO;
using DataAccess;

namespace GeoAppUI
{
    public partial class MainWindow : Form
    {
        private ContextMenuStrip _markersMenu;
        private ContextMenuStrip _polygonsMenu;
        private bool contextMenuStripWasClosed = false;
        private GMapMarker _currentMarker = null;
        private GMapPolygon _currentPolygon = null;
        private GMapPolygon _testPolygon;
        private GMapInteractionHelper _helper;
        bool IsHovering = false;

        public MainWindow()
        {
            InitializeComponent();
            var overlay = new GMapOverlay("Test");

            _testPolygon = new GMapPolygon(new List<PointLatLng>
            {
                new PointLatLng(68.720440570, 81.738281250),
                new PointLatLng(70.988349224, 103.535156250),
                new PointLatLng(64.848937264, 100.195312500)
            }, "Test Polygon");
            _testPolygon.Tag = PolygonEnterAction.ShowMessageBox;
            _testPolygon.IsHitTestVisible = true;
            overlay.Polygons.Add(_testPolygon);

            googleMap.Overlays.Add(overlay);

            _helper = new GMapInteractionHelper(googleMap);
            ConfigureContextMenuStrip();
        }

        private void ConfigureContextMenuStrip()
        {
            _markersMenu = new ContextMenuStrip();

            ToolStripMenuItem assignMenuItem = new ToolStripMenuItem("Начать автоматическое отслеживание...");
            assignMenuItem.Name = "Assign";            
            ToolStripMenuItem clearMenuItem = new ToolStripMenuItem("Прекратить отслеживание");
            clearMenuItem.Name = "Clear";

            _markersMenu.Items.Add(assignMenuItem);
            _markersMenu.Items.Add(clearMenuItem);

            _markersMenu.Closed += ContextMenuStrip_Closed;
            assignMenuItem.Click += AssignMenuItem_Click;
            clearMenuItem.Click += ClearMenuItem_Click;

            _polygonsMenu = new ContextMenuStrip();
            _polygonsMenu.Closed += ContextMenuStrip_Closed;
            ToolStripMenuItem configureMenuItem = new ToolStripMenuItem("Действие при вхождении маркера...");
            configureMenuItem.Name = "Configure";

            _polygonsMenu.Items.Add(configureMenuItem);

            configureMenuItem.Click += ConfigureMenuItem_Click;
        }

        private void ConfigureMenuItem_Click(object sender, EventArgs e)
        {
            using (PolygonActionConfigurationWindow configurationWindow = new PolygonActionConfigurationWindow(_currentPolygon))
            {
                if (configurationWindow.ShowDialog() == DialogResult.OK) return;
            }
        }

        private void ClearMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Вы точно хотите прекратить отслеживание?", "Прекратить отслеживание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                (_currentMarker.Tag as MarkerModel).LocationProvider.StopUpdatingData();
                (_currentMarker.Tag as MarkerModel).LocationProvider = null;
            }
        }

        private void ContextMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            googleMap.ContextMenuStrip = null;
            contextMenuStripWasClosed = true;
        }

        private void AssignMenuItem_Click(object sender, EventArgs e)
        {
            using (AutoTrackingConfigurationWindow configurationWindow = new AutoTrackingConfigurationWindow(_currentMarker))
            {
                if(configurationWindow.ShowDialog() == DialogResult.OK)
                {
                    _helper.ChangeConfiguration(configurationWindow, _currentMarker);
                }
            }
        }

        private void googleMapControl_Load(object sender, EventArgs e)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            googleMap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            googleMap.MinZoom = 2;
            googleMap.MaxZoom = 16;
            googleMap.Zoom = 4;
            googleMap.Position = new GMap.NET.PointLatLng(66.4169575018027, 94.25025752215694);
            googleMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            googleMap.CanDragMap = true; 
            googleMap.DragButton = MouseButtons.Left;
            googleMap.ShowCenter = false;
            googleMap.ShowTileGridLines = false;

            _helper.LoadMarkers(googleMap);
        }

        private void googleMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (contextMenuStripWasClosed)
            {
                _currentMarker = null;
                contextMenuStripWasClosed = false;
                return;
            }
            if (IsHovering && _currentMarker != null && _currentMarker.Tag != null)
            {
                PointLatLng point = googleMap.FromLocalToLatLng(e.X, e.Y);
                _currentMarker.Position = point;
                (_currentMarker.Tag as MarkerModel).Position = point;
                _currentMarker.ToolTipMode = MarkerTooltipMode.Never;
            }
        }

        private void googleMap_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left) IsHovering = false;
            if (e.Button == MouseButtons.Left && _currentMarker != null)
            {
                _currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                if (_currentMarker.Tag == null) return;
                _helper.SaveMarkerPostion(_currentMarker.Tag as MarkerModel);
                _helper.CheckIfInsidePolygon(_currentMarker, googleMap.Overlays[0]);
            }
        }

        private void googleMap_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left) IsHovering = true;
        }

        private void googleMap_OnMarkerEnter(GMapMarker item)
        {
            if(!IsHovering) _currentMarker = item;
        }

        private void googleMap_OnMarkerLeave(GMapMarker item)
        {
            if(!IsHovering) _currentMarker = null;
        }

        private void googleMap_OnMapDrag()
        {
            IsHovering = false;
        }

        private void googleMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right) 
            {
                if ((item.Tag as MarkerModel) == null) return;
                if(googleMap.ContextMenuStrip == null) googleMap.ContextMenuStrip = _markersMenu;
                if((item.Tag as MarkerModel).LocationProvider != null)
                {
                    googleMap.ContextMenuStrip.Items.Find("Clear", false).First().Visible = true;
                    googleMap.ContextMenuStrip.Items.Find("Assign", false).First().Visible = false;
                }
                else
                {
                    googleMap.ContextMenuStrip.Items.Find("Clear", false).First().Visible = false;
                    googleMap.ContextMenuStrip.Items.Find("Assign", false).First().Visible = true;
                }
                googleMap.ContextMenuStrip.Show(googleMap, e.Location);
            }
        }

        private void googleMap_OnPolygonClick(GMapPolygon polygon, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                if(googleMap.ContextMenuStrip == null) googleMap.ContextMenuStrip = _polygonsMenu;
                googleMap.ContextMenuStrip.Show(googleMap, e.Location);
            }
        }

        private void googleMap_OnPolygonEnter(GMapPolygon item)
        {
            _currentPolygon = item;
        }

        private void googleMap_OnPolygonLeave(GMapPolygon item)
        {
            _currentPolygon = null;
        }
    }
}
