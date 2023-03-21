using DataAccess;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace GeoAppUI
{
    public partial class AutoTrackingConfigurationWindow : Form
    {
        private readonly GMapMarker _marker;
        public ILocationProvider SelectedLocationProvider { get; private set; }
        public bool SaveDataToDb { get; set; } = true;
        private string _argument;

        public AutoTrackingConfigurationWindow(GMapMarker marker)
        {
            InitializeComponent();
            _marker = marker;
            LoadLocationProviders();
        }

        private void LoadLocationProviders()
        {
            locationDataSourceComboBox.Items.Add("Данные из файла");
            locationDataSourceComboBox.Items.Add("GPS");
            locationDataSourceComboBox.Items.Add("API");
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            switch (locationDataSourceComboBox.SelectedIndex)
            {
                case 0:
                    LocationProviderFromFile providerFromFile = new LocationProviderFromFile(_argument);
                    providerFromFile.StartUpdatingData(_marker);
                    SelectedLocationProvider = providerFromFile;
                    break;
                case 1:
                    if(_argument.Length > 5 || !_argument.StartsWith("COM") || !int.TryParse(_argument.Substring(3), out _))
                    {
                        MessageBox.Show("Неправильно введённый COM порт.");
                        return;
                    }
                    LocationProviderFromGPS gpsLocationProvider = new LocationProviderFromGPS(_argument, 1000);
                    gpsLocationProvider.StartUpdatingData(_marker);
                    SelectedLocationProvider = gpsLocationProvider;
                    break;
                case 2:
                    bool result = Uri.TryCreate(_argument, UriKind.Absolute, out Uri uriResult)
                           && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                    if (result == false)
                    {
                        MessageBox.Show("Неправильно введённый URL.");
                        return;
                    }
                    LocationProviderFromAPI apiLocationProvider = new LocationProviderFromAPI(_argument,1000);
                    apiLocationProvider.StartUpdatingData(_marker);
                    SelectedLocationProvider = apiLocationProvider;
                    break;
                default:
                    throw new Exception("Unsupported location data source was selected.");
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void dataSourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (locationDataSourceComboBox.SelectedIndex)
            {
                case 0:
                    openFileDialogButton.Visible = true;
                    argumentNameLabel.Visible = false;
                    break;
                case 1:
                    argumentNameLabel.Visible = true;
                    argumentNameLabel.Text = "COM порт:";
                    openFileDialogButton.Visible = false;
                    break;
                case 2:
                    argumentNameLabel.Visible = true;
                    argumentNameLabel.Text = "URL:";
                    openFileDialogButton.Visible = false;
                    break;
                default:
                    throw new Exception("Unsupported location data source was selected.");
            }
        }

        private void saveToDbCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SaveDataToDb = saveToDbCheckBox.Checked;
        }

        private void openFileDialogButton_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.CheckFileExists = true;
                fileDialog.CheckPathExists = true;
                if(fileDialog.ShowDialog() == DialogResult.OK)
                {
                    _argument = fileDialog.FileName;
                    argumentTextBox.Text = _argument;
                }
            }
        }

        private void argumentTextBox_TextChanged(object sender, EventArgs e)
        {
            _argument = argumentTextBox.Text;
        }
    }
}
