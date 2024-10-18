using DLSBusinessLayer;
using DrivingLicenseSystem.Licenses;
using DrivingLicenseSystem.Licenses.internationalLicense;
using DrivingLicenseSystem.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.ManageApplications.InternationalDrivingLicenseApplications
{
    public partial class frmManageInternationalDrivingLicenseApplications : Form
    {

        DataTable _dtInternationalDrivingLicenses;
        public frmManageInternationalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void _LoadInternationalLicenseInfo()
        {
            _dtInternationalDrivingLicenses = clsInternationalLicense.GetInternationalLicenses();

            dgvInternationalDrivingLicenseApp.DataSource = _dtInternationalDrivingLicenses;
            lblRecordsCount.Text = _dtInternationalDrivingLicenses.Rows.Count.ToString();

            if (dgvInternationalDrivingLicenseApp.Rows.Count > 0)
            {
                dgvInternationalDrivingLicenseApp.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalDrivingLicenseApp.Columns[0].Width = 140;

                dgvInternationalDrivingLicenseApp.Columns[1].HeaderText = "Application ID";
                dgvInternationalDrivingLicenseApp.Columns[1].Width = 110;

                dgvInternationalDrivingLicenseApp.Columns[2].HeaderText = "Driver ID";
                dgvInternationalDrivingLicenseApp.Columns[2].Width = 170;

                dgvInternationalDrivingLicenseApp.Columns[3].HeaderText = "L.License ID";
                dgvInternationalDrivingLicenseApp.Columns[3].Width = 170;

                dgvInternationalDrivingLicenseApp.Columns[4].HeaderText = "Issue Date";
                dgvInternationalDrivingLicenseApp.Columns[4].Width = 200;

                dgvInternationalDrivingLicenseApp.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalDrivingLicenseApp.Columns[5].Width = 200;

                dgvInternationalDrivingLicenseApp.Columns[6].HeaderText = "Is Active";
                dgvInternationalDrivingLicenseApp.Columns[6].Width = 110;



            }





        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
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

                }else
                    txtFilterValue.Visible = true;



                txtFilterValue.Text = "";
                txtFilterValue.Focus();
               

            }





           
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {


            
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";



            switch (cbFilterBy.Text)
            {


                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;

                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;


                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }

          


            DataTable dtDrivers = (DataTable)dgvInternationalDrivingLicenseApp.DataSource;
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                dtDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvInternationalDrivingLicenseApp.Rows.Count.ToString();
                return;
            }

          
                //in this case we deal with numbers not string.
            _dtInternationalDrivingLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
           
            lblRecordsCount.Text = _dtInternationalDrivingLicenses.Rows.Count.ToString();


        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
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
                _dtInternationalDrivingLicenses.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtInternationalDrivingLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsCount.Text = _dtInternationalDrivingLicenses.Rows.Count.ToString();
        }

        private void frmManageInternationalDrivingLicenseApplications_Load(object sender, EventArgs e)

        {
            cbFilterBy.SelectedIndex = 0;   
            cbIsReleased.Visible = false;  
            _LoadInternationalLicenseInfo();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //int IntLicenseID = clsDriver.Find().DriverID;  

            int DriverID = (int)dgvInternationalDrivingLicenseApp.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.Find(DriverID).PersonID;
            frmShowPersonLicenseHistory frm  = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
            frmManageInternationalDrivingLicenseApplications_Load(null , null);
        }

        
        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int DriverID =(int)dgvInternationalDrivingLicenseApp.CurrentRow.Cells[2].Value;
           // int PerosonID  =clsDriver.Find(DriverID).PersonID; faster method

            int PersonID = clsInternationalLicense.GetPersonInfo((int)dgvInternationalDrivingLicenseApp.CurrentRow.Cells[2].Value);
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
            frmManageInternationalDrivingLicenseApplications_Load(null, null);
        }

        private void ShowLicenseInfoItem_Click(object sender, EventArgs e)
        {
            frmInternationalDrivingLicenseInfo frm = new frmInternationalDrivingLicenseInfo((int)dgvInternationalDrivingLicenseApp.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManageInternationalDrivingLicenseApplications_Load(null, null);
        }

        private void AddNewInternationalLicense_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalLicense frm  = new frmAddNewInternationalLicense();   
            frm.ShowDialog();
            frmManageInternationalDrivingLicenseApplications_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
