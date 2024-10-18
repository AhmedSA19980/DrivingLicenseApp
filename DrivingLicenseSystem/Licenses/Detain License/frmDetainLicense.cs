using DLSBusinessLayer;
using DrivingLicenseSystem.ClassGlobal;
using DrivingLicenseSystem.Licenses.Local_License;
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
using static DLSBusinessLayer.clsLicense;

namespace DrivingLicenseSystem.Licenses.Detain_License
{
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private int _DetainID=-1; 
        private int _SelectedLicenseID;  
        clsDetainLicense _DetainLicense;

        clsLicense _license;
        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName ;
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);   

        }

        private void ctrlDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {

            _SelectedLicenseID = obj;
            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1 || ctrlDriverLicenseWithFilter1.SelectedLicenseInfo == null)
            {
                lblLicenseID.Text = "[???]";
                return;
            }

            if (ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("License is Already Detained, you can not detain it twice", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlDriverLicenseWithFilter1.TxtLicenseIDFocus();
                return;
            }
          

     
            txtFineFees.Focus();    
            btnDetain.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to detain this license ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            float FineFees = Convert.ToSingle(txtFineFees.Text.Trim());

            int DetainLicense = ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DetainLicense( FineFees,clsGlobal.CurrentUser.UserID);

            if (DetainLicense == -1)
            {
                MessageBox.Show("Failed to Detain a license", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("License has Detained Successfully with id =" + DetainLicense, "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _DetainID = DetainLicense;

               
                lblDetainID.Text = _DetainID.ToString();
              //  _SelectedLicenseID = ctrlDriverLicenseWithFilter1.LicenseID;

                llShowLicenseInfo.Enabled = true;

                ctrlDriverLicenseWithFilter1.FilterEnabled = false;
                btnDetain.Enabled = false;
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();   
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDrivingLicenseInfo frm = new frmShowDrivingLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();   
        }

        private void frmDetainLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseWithFilter1.TxtLicenseIDFocus();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            };


            if (!clsValidations.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            };
        }
    }
}
