using DLSBusinessLayer;
using DrivingLicenseSystem.Licenses;
using DrivingLicenseSystem.Licenses.Detain_License;
using DrivingLicenseSystem.Licenses.Local_License;
using DrivingLicenseSystem.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Applications.Release_Licnese
{
    public partial class frmListDetainLicenses : Form
    {
        DataTable _AllDetainedLicenses;
        public frmListDetainLicenses()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListDetainLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _AllDetainedLicenses = clsDetainLicense.GetDetainedLicenses();
            dgvListDetainedLicenses.DataSource = _AllDetainedLicenses;
            lblRecordsCount.Text = dgvListDetainedLicenses.Rows.Count.ToString();
            if (dgvListDetainedLicenses.Rows.Count > 0)
            {
                dgvListDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvListDetainedLicenses.Columns[0].Width = 70;

                dgvListDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvListDetainedLicenses.Columns[1].Width = 70;

                dgvListDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvListDetainedLicenses.Columns[2].Width = 130;

                dgvListDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvListDetainedLicenses.Columns[3].Width = 70;

                dgvListDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvListDetainedLicenses.Columns[4].Width = 100;

                dgvListDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvListDetainedLicenses.Columns[5].Width = 120;

                dgvListDetainedLicenses.Columns[6].HeaderText = "N.No";
                dgvListDetainedLicenses.Columns[6].Width = 80;


                dgvListDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvListDetainedLicenses.Columns[7].Width = 250;

                dgvListDetainedLicenses.Columns[8].HeaderText = "Release.AppID";
                dgvListDetainedLicenses.Columns[8].Width = 100;

            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {


                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;

                case "Is Released":
                    FilterColumn = "IsReleased";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;


                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }

            DataTable dtDetainedLicenses = (DataTable)dgvListDetainedLicenses.DataSource;
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                dtDetainedLicenses.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvListDetainedLicenses.Rows.Count.ToString();
                return;
            }

            if (FilterColumn != "FullName" && FilterColumn != "NationalNo")
                //in this case we deal with numbers not string.
                _AllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _AllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _AllDetainedLicenses.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Enabled = false;
                cbIsReleased.SelectedIndex = 0;
                cbIsReleased.Focus();
                cbIsReleased.Visible = true;

            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Visible = false;

                }
                else
                    txtFilterValue.Visible = true;



                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Is Released" || cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIsReleased.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _AllDetainedLicenses.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _AllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsCount.Text = _AllDetainedLicenses.Rows.Count.ToString();
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNo = (string)dgvListDetainedLicenses.CurrentRow.Cells[6].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(NationalNo);    
            frm.ShowDialog();   
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvListDetainedLicenses.CurrentRow.Cells[1].Value ;
            frmShowDrivingLicenseInfo frm = new frmShowDrivingLicenseInfo(PersonID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsPerson.Find((string)dgvListDetainedLicenses.CurrentRow.Cells[6].Value).PersonID; ;
          
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            bool IsReleased = (bool)dgvListDetainedLicenses.CurrentRow.Cells[3].Value;
            releaseDetainedLicenseToolStripMenuItem.Enabled = (!IsReleased);
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int LicenseID = (int)dgvListDetainedLicenses.CurrentRow.Cells[1].Value;
            frmReleaseDetainLicense frm = new frmReleaseDetainLicense(LicenseID);    
            frm.ShowDialog();
            frmListDetainLicenses_Load(null, null);
        }

        private void DetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            frmListDetainLicenses_Load(null, null);
        }


        private void ReleaseLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainLicense frm = new frmReleaseDetainLicense();
            frm.ShowDialog();
            frmListDetainLicenses_Load(null, null);
        }


    }
}
