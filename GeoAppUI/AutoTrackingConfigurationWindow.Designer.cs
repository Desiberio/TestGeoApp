namespace GeoAppUI
{
    partial class AutoTrackingConfigurationWindow
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveToDbCheckBox = new System.Windows.Forms.CheckBox();
            this.locationDataSourceComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialogButton = new System.Windows.Forms.Button();
            this.argumentTextBox = new System.Windows.Forms.TextBox();
            this.argumentNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.okButton.Location = new System.Drawing.Point(242, 48);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelButton.Location = new System.Drawing.Point(323, 48);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveToDbCheckBox
            // 
            this.saveToDbCheckBox.AutoSize = true;
            this.saveToDbCheckBox.Checked = true;
            this.saveToDbCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveToDbCheckBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveToDbCheckBox.Location = new System.Drawing.Point(243, 13);
            this.saveToDbCheckBox.Name = "saveToDbCheckBox";
            this.saveToDbCheckBox.Size = new System.Drawing.Size(154, 30);
            this.saveToDbCheckBox.TabIndex = 2;
            this.saveToDbCheckBox.Text = "Сохранять полученные \r\nданные в базу данных";
            this.saveToDbCheckBox.UseVisualStyleBackColor = true;
            this.saveToDbCheckBox.CheckedChanged += new System.EventHandler(this.saveToDbCheckBox_CheckedChanged);
            // 
            // locationDataSourceComboBox
            // 
            this.locationDataSourceComboBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.locationDataSourceComboBox.FormattingEnabled = true;
            this.locationDataSourceComboBox.Location = new System.Drawing.Point(67, 18);
            this.locationDataSourceComboBox.Name = "locationDataSourceComboBox";
            this.locationDataSourceComboBox.Size = new System.Drawing.Size(170, 21);
            this.locationDataSourceComboBox.TabIndex = 3;
            this.locationDataSourceComboBox.SelectedIndexChanged += new System.EventHandler(this.dataSourceComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(4, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Источник:";
            // 
            // openFileDialogButton
            // 
            this.openFileDialogButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.openFileDialogButton.Location = new System.Drawing.Point(4, 48);
            this.openFileDialogButton.Name = "openFileDialogButton";
            this.openFileDialogButton.Size = new System.Drawing.Size(59, 23);
            this.openFileDialogButton.TabIndex = 6;
            this.openFileDialogButton.Text = "Файл...";
            this.openFileDialogButton.UseVisualStyleBackColor = true;
            this.openFileDialogButton.Visible = false;
            this.openFileDialogButton.Click += new System.EventHandler(this.openFileDialogButton_Click);
            // 
            // argumentTextBox
            // 
            this.argumentTextBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.argumentTextBox.Location = new System.Drawing.Point(67, 49);
            this.argumentTextBox.Name = "argumentTextBox";
            this.argumentTextBox.Size = new System.Drawing.Size(170, 22);
            this.argumentTextBox.TabIndex = 7;
            this.argumentTextBox.TextChanged += new System.EventHandler(this.argumentTextBox_TextChanged);
            // 
            // argumentNameLabel
            // 
            this.argumentNameLabel.AutoSize = true;
            this.argumentNameLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.argumentNameLabel.Location = new System.Drawing.Point(-1, 53);
            this.argumentNameLabel.Name = "argumentNameLabel";
            this.argumentNameLabel.Size = new System.Drawing.Size(62, 13);
            this.argumentNameLabel.TabIndex = 8;
            this.argumentNameLabel.Text = "Источник:";
            this.argumentNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.argumentNameLabel.Visible = false;
            // 
            // AutoTrackingConfigurationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 81);
            this.Controls.Add(this.argumentNameLabel);
            this.Controls.Add(this.argumentTextBox);
            this.Controls.Add(this.openFileDialogButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.locationDataSourceComboBox);
            this.Controls.Add(this.saveToDbCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Name = "AutoTrackingConfigurationWindow";
            this.Text = "Автоматическое отслеживание";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox saveToDbCheckBox;
        private System.Windows.Forms.ComboBox locationDataSourceComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openFileDialogButton;
        private System.Windows.Forms.TextBox argumentTextBox;
        private System.Windows.Forms.Label argumentNameLabel;
    }
}