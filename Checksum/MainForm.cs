/*
 * Checksum - Verify the integrity of files.
 * Copyright (C) 2011 John Bird <https://github.com/jbird/Checksum>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Security;

namespace Checksum {
    public partial class MainForm : Form {
        /**
         * <summary>Gets the currently selected hash in the combo box.</summary>
         */
        private Checksum.Hash GetSelectedHash() {
            return (Checksum.Hash)Enum.Parse(typeof(Checksum.Hash), hashComboBox.SelectedValue.ToString());
        }

        /**
         * <summary>Gets the currently selected hash in the combo box.</summary>
         */
        public string SelectedHash {
            get {
                return hashComboBox.SelectedItem.ToString();
            }
        }

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            /*hashComboBox.DataSource = new BindingSource(Checksum.HashAlgorithms(), null);
            hashComboBox.DisplayMember = "Value";
            hashComboBox.ValueMember = "Key";*/
            hashComboBox.SelectedIndex = 0;
        }

        private void selectButton_Click(object sender, EventArgs e) {
            OpenFile();
        }

        private void fileTextBox_Click(object sender, EventArgs e) {
            if(String.IsNullOrEmpty(fileTextBox.Text)) OpenFile();
        }

        private void computeButton_Click(object sender, EventArgs e) {
            if(ReadPermission(openFileDialog.FileName)) {

                switch(SelectedHash) {
                    case "MD5":
                        resultTextBox.Text = Checksum.ComputeChecksum(Checksum.Hash.MD5, openFileDialog.FileName);
                        break;
                    case "SHA-1":
                        resultTextBox.Text = Checksum.ComputeChecksum(Checksum.Hash.SHA1, openFileDialog.FileName);
                        break;
                    case "SHA-256":
                        resultTextBox.Text = Checksum.ComputeChecksum(Checksum.Hash.SHA256, openFileDialog.FileName);
                        break;
                    case "SHA-512":
                        resultTextBox.Text = Checksum.ComputeChecksum(Checksum.Hash.SHA512, openFileDialog.FileName);
                        break;
                }

            } else {
                MessageBox.Show("You do not have read permission for the given file.", "Read Permission", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool ReadPermission(string path) {
            FileIOPermission fp = new FileIOPermission(FileIOPermissionAccess.Read, path);
            try {
                fp.Demand();
            } catch(SecurityException) { return false; }

            return true;
        }

        private void OpenFile() {
            if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                fileTextBox.Text = openFileDialog.FileName;
                computeButton.Enabled = true;
            }
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e) {
            MessageBox.Show(this.Width.ToString());
        }

        private void hashComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            switch(SelectedHash) {
                case "MD5":
                    Height = 134;
                    break;
                case "SHA-1":
                    Height = 134;
                    break;
                case "SHA-256":
                    Height = 134;
                    break;
                case "SHA-512":
                    Height = 154;
                    break;
            }
        }
    }
}
