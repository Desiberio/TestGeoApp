using DataAccess;
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

namespace GeoAppUI
{
    public partial class PolygonActionConfigurationWindow : Form
    {
        private GMapPolygon _selectedPolygon;

        public PolygonActionConfigurationWindow(GMapPolygon selectedPolygon)
        {
            InitializeComponent();

            _selectedPolygon = selectedPolygon;

            LoadAvailableActions();
        }

        private void LoadAvailableActions()
        {
            actionsComboBox.Items.Add("Показать диалоговое окно");
            actionsComboBox.Items.Add("Изменение цвета маркера");
            actionsComboBox.Items.Add("Новый маркер в видимой области");
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            _selectedPolygon.Tag = (PolygonEnterAction)actionsComboBox.SelectedIndex;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
