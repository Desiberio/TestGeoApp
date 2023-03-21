namespace GeoAppUI
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.googleMap = new GMap.NET.WindowsForms.GMapControl();
            this.SuspendLayout();
            // 
            // googleMap
            // 
            this.googleMap.Bearing = 0F;
            this.googleMap.CanDragMap = true;
            this.googleMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.googleMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.googleMap.GrayScaleMode = false;
            this.googleMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.googleMap.LevelsKeepInMemory = 5;
            this.googleMap.Location = new System.Drawing.Point(0, 0);
            this.googleMap.MarkersEnabled = true;
            this.googleMap.MaxZoom = 2;
            this.googleMap.MinZoom = 2;
            this.googleMap.MouseWheelZoomEnabled = true;
            this.googleMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.googleMap.Name = "googleMap";
            this.googleMap.NegativeMode = false;
            this.googleMap.PolygonsEnabled = true;
            this.googleMap.RetryLoadTile = 0;
            this.googleMap.RoutesEnabled = true;
            this.googleMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.googleMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.googleMap.ShowTileGridLines = false;
            this.googleMap.Size = new System.Drawing.Size(800, 450);
            this.googleMap.TabIndex = 0;
            this.googleMap.Zoom = 0D;
            this.googleMap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.googleMap_OnMarkerClick);
            this.googleMap.OnPolygonClick += new GMap.NET.WindowsForms.PolygonClick(this.googleMap_OnPolygonClick);
            this.googleMap.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.googleMap_OnMarkerEnter);
            this.googleMap.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.googleMap_OnMarkerLeave);
            this.googleMap.OnPolygonEnter += new GMap.NET.WindowsForms.PolygonEnter(this.googleMap_OnPolygonEnter);
            this.googleMap.OnPolygonLeave += new GMap.NET.WindowsForms.PolygonLeave(this.googleMap_OnPolygonLeave);
            this.googleMap.OnMapDrag += new GMap.NET.MapDrag(this.googleMap_OnMapDrag);
            this.googleMap.Load += new System.EventHandler(this.googleMapControl_Load);
            this.googleMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.googleMap_MouseDown);
            this.googleMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.googleMap_MouseMove);
            this.googleMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.googleMap_MouseUp);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.googleMap);
            this.Name = "MainWindow";
            this.Text = "TestGeoApp";
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl googleMap;
    }
}

