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
        /*public Checksum.Hash SelectedHash {
            get {
                return (Checksum.Hash)Enum.Parse(typeof(Checksum.Hash), hashComboBox.SelectedValue.ToString());
            }
        }*/

        /**
         * <summary>Gets the currently selected hash.</summary>
         */
        public string SelectedHash {
            get {
                return hashComboBox.SelectedItem.ToString();
            }
        }

        /**
         * <summary>Gets the previously selected hash. Used to determine whether to clear the results or not.</summary>
         */
        public string PreviousHash { get; private set; }

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            hashComboBox.SelectedIndex = 0;
            PreviousHash = SelectedHash;
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
                    case Checksum.MD5:
                        resultTextBox.Text = Checksum.ComputeChecksum(Checksum.Hash.MD5, openFileDialog.FileName);
                        resultTextBox.BackColor = Color.White;
                        if(Height > 124) ShrinkWindow();
                        break;
                    case Checksum.SHA1:
                        resultTextBox.Text = Checksum.ComputeChecksum(Checksum.Hash.SHA1, openFileDialog.FileName);
                        resultTextBox.BackColor = Color.White;
                        if(Height > 124) ShrinkWindow();
                        break;
                    case Checksum.SHA256:
                        resultTextBox.Text = Checksum.ComputeChecksum(Checksum.Hash.SHA256, openFileDialog.FileName);
                        resultTextBox.BackColor = Color.White;
                        if(Height > 124) ShrinkWindow();
                        break;
                    case Checksum.SHA512:
                        resultTextBox.Text = Checksum.ComputeChecksum(Checksum.Hash.SHA512, openFileDialog.FileName);
                        resultTextBox.BackColor = Color.White;
                        if(Height < 140) ExpandWindow();
                        break;
                }

                resetButton.Enabled = true;

            } else {
                MessageBox.Show("You do not have read permission for the given file.", "Read Permission", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void hashComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if(SelectedHash != PreviousHash) {
                resultTextBox.BackColor = Control.DefaultBackColor;
                resultTextBox.Text = String.Empty;
                PreviousHash = SelectedHash;
            }
        }

        private void reset_Click(object sender, EventArgs e) {
            openFileDialog.Reset();
            computeButton.Enabled = false;
            fileTextBox.Text = String.Empty;
            resultTextBox.Text = String.Empty;
            ShrinkWindow();
            hashComboBox.SelectedIndex = 0;
            resetButton.Enabled = false;
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

        private void ShrinkWindow() {
            while(Height >= 124) {
                Height--;
                Refresh();
            }
        }

        private void ExpandWindow() {
            while(Height <= 140) {
                Height++;
                Refresh();
            }
        }
    }
}
