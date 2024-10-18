using DLSBusinessLayer;
using DrivingLicenseSystem.Licenses.internationalLicense;
using DrivingLicenseSystem.Licenses.Local_License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Licenses.control
{
    public partial class ctrlDriverLicense : UserControl
    {
        clsDriver _Driver;
        DataTable _dtLocalDrivingLicense;
        DataTable _dtInternationalDrivingLicense;
        private int _DriverID; 
        public ctrlDriverLicense()
        {
            InitializeComponent();
        }


        private void _LoadLocalLicenseInfo()
        {
            _dtLocalDrivingLicense = clsDriver.GetLicenses(_DriverID);

            dgvLocalLicensesHistory.DataSource = _dtLocalDrivingLicense;
            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();
            
            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 170;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 110;

            

            }



            
           
        }


        private void _LoadInternationalLicenseInfo()
        {
            _dtInternationalDrivingLicense = clsDriver.GetInternationalDriverLicense(_DriverID);

            dgvInternationalLicenseHistory.DataSource = _dtInternationalDrivingLicense;
            lblInternationalLicensesRecords.Text = _dtInternationalDrivingLicense.Rows.Count.ToString();

            if (dgvInternationalLicenseHistory.Rows.Count > 0)
            {
                dgvInternationalLicenseHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenseHistory.Columns[0].Width = 140;

                dgvInternationalLicenseHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenseHistory.Columns[1].Width = 110;

                dgvInternationalLicenseHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicenseHistory.Columns[2].Width = 170;

                dgvInternationalLicenseHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicenseHistory.Columns[3].Width = 200;

                dgvInternationalLicenseHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicenseHistory.Columns[4].Width = 200;

                dgvInternationalLicenseHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicenseHistory.Columns[5].Width = 110;



            }





        }

        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.Find(_DriverID);
            if (_Driver == null)
            {
                MessageBox.Show("No Driver has found !", "Failed", MessageBoxButtons.OK);
                return;
            }
            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();

        }
        public void LoadLicensesInfoByPersonID(int PersonID)
        {
            _Driver  = clsDriver.FindDriverByPersonID(PersonID);
          
            if ( _Driver == null )
            {
                MessageBox.Show("No Driver With PersonId matched", "Failed", MessageBoxButtons.OK);
                return; 
            }
            _DriverID = _Driver.DriverID;
            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();

        }

        public void Clear()
        {
            _dtLocalDrivingLicense.Clear();
            _dtInternationalDrivingLicense.Clear(); 
        }
        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value;
            frmShowDrivingLicenseInfo frm =new frmShowDrivingLicenseInfo(LicenseID);
            frm.ShowDialog();   
        }

        private void InternationalLicenseHistorytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicenseHistory.CurrentRow.Cells[0].Value;
            frmInternationalDrivingLicenseInfo frm = new frmInternationalDrivingLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }
    }
}
