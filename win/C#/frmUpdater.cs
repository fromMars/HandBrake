/*  frmUpdater.cs $
 	
 	   This file is part of the HandBrake source code.
 	   Homepage: <http://handbrake.fr>.
 	   It may be used under the terms of the GNU General Public License. */

using System;
using System.Windows.Forms;
using Handbrake.Functions;

namespace Handbrake
{
    public partial class frmUpdater : Form
    {
        readonly AppcastReader _appcast;
        public frmUpdater(AppcastReader reader)
        {
            InitializeComponent();

            _appcast = reader;
            GetRss();
            SetVersions();
        }

        private void GetRss()
        {
            wBrowser.Url = _appcast.descriptionUrl;
        }

        private void SetVersions()
        {
            string old = "(You have: " + Properties.Settings.Default.hb_version.Trim() + " / " + Properties.Settings.Default.hb_build.ToString().Trim() + ")";
            string newBuild = _appcast.version.Trim() + " (" + _appcast.build + ")";
            lbl_update_text.Text = "HandBrake " + newBuild + " is now available. " + old;
        }

        private void btn_installUpdate_Click(object sender, EventArgs e)
        {
            frmDownload download = new frmDownload(_appcast.downloadFile);
            download.ShowDialog();
            this.Close();
        }

        private void btn_remindLater_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_skip_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.skipversion = int.Parse(_appcast.build);
            Properties.Settings.Default.Save();

            this.Close();
        }

    }
}