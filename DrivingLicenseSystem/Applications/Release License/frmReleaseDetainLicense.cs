using DLSBusinessLayer;
using DLSDATA;
using DrivingLicenseSystem.ClassGlobal;
using DrivingLicenseSystem.Licenses;
using DrivingLicenseSystem.Licenses.Local_License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Applications.Release_Licnese

{
    public partial class frmReleaseDetainLicense : Form
    {

        clsLicense _License;
     
       

        private int _SelectedLicenseID;
        public frmReleaseDetainLicense()
        {
            InitializeComponent();
        }


        public frmReleaseDetainLicense(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;

            ctrlDriverLicenseWithFilter1.LoadDrivingLicenseInfo(LicenseID);
            ctrlDriverLicenseWithFilter1.FilterEnabled = false;
        
        }



        private void ctrlDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;
            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1 || ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DetainedInfo == null)
            {
                return;
            }

          

          
            if (!ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("License is not Detained !, Check for another License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlDriverLicenseWithFilter1.TxtLicenseIDFocus();
                return;
            }

            lblDetainDate.Text = clsFormat.DateToShort(ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainDate);
            lblDetainID.Text = ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblCreatedByUser.Text = ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DetainedInfo.CreatedByUserID.ToString();
            lblReleaseApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();
            lblFineFees.Text = ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblReleaseApplicationFees.Text)).ToString();





            btnReleaseDetain.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReleaseDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Release this license ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
           

            bool IsRelealsed = ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID);

            if (!IsRelealsed)
            {
                MessageBox.Show("Failed to Release Detain a license", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Detained license has Released Successfully with id =" + IsRelealsed, "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblApplicationID.Text = IsRelealsed.ToString();   

           
                llShowLicenseInfo.Enabled = true;

                ctrlDriverLicenseWithFilter1.FilterEnabled = false;
                btnReleaseDetain.Enabled = false;
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDrivingLicenseInfo frm = new frmShowDrivingLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm  = new frmShowPersonLicenseHistory(ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();   
        }

        private void frmReleaseDetainLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseWithFilter1.TxtLicenseIDFocus();
        }
    }
}
