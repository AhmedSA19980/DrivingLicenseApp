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

namespace DrivingLicenseSystem.Applications.Replace_License
{
    
    
    public partial class frmReplaceForDamageOrLostLicense : Form
    {
        public frmReplaceForDamageOrLostLicense()
        {
            InitializeComponent();
        }
       
      
      

        private clsLicense.enIssueReason IssueReason = clsLicense.enIssueReason.DamagedReplacement;
        private int _NewLicenseID=-1; 
        private void frmReplaceForDamageOrLostLicense_Load(object sender, EventArgs e)
        {

            rbDamage.Checked = true;    
           
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);




              

        }

        private void ctrlDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblOldLocalLicenseID.Text = SelectedLicenseID.ToString();   
            llShowLicenseHistory.Enabled = (SelectedLicenseID !=-1);

            if(SelectedLicenseID == -1)
            {
                return;
            }

          

            if (!ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not yet Active, Choose another one !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnReplaceLicense.Enabled = false;
                return;
            }

            btnReplaceLicense.Enabled = true;


        }


        private void rbDamage_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDamage.Checked)
            {
                lblTitle.Text = "Replacement For Damaged License !";
                this.Text  = lblTitle.Text;
                lblAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).ApplicationFees.ToString();
                IssueReason = clsLicense.enIssueReason.DamagedReplacement;
            }
        }
        
        private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
            if(rbLost.Checked)
            {
               // rbLost.Checked = true;
                lblTitle.Text = "Replacement For Lost License !";
                this.Text = lblTitle.Text;
                lblAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).ApplicationFees.ToString();
                IssueReason = clsLicense.enIssueReason.Lost ;
            }
        
        }

        private void frmReplaceForDamageOrLostLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseWithFilter1.TxtLicenseIDFocus();
        }

        private void btnIssueReplaceLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to renew this license ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsLicense NewLicense = ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.ReplaceLicense(IssueReason , clsGlobal.CurrentUser.UserID);

            if (NewLicense == null) {
                MessageBox.Show("Failed to save the data", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }else
            {
                MessageBox.Show("License Renewed Successfully with id =" + NewLicense.LicenseID, "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _NewLicenseID = NewLicense.LicenseID;

                lblApplicationID.Text = NewLicense.ApplicationID.ToString();
                lblReplaceLicenseID.Text = _NewLicenseID.ToString();

                llShowLicenseInfo.Enabled = true;

                ctrlDriverLicenseWithFilter1.FilterEnabled = false;
                btnReplaceLicense.Enabled = false;
                gbReplacementFor.Enabled =false;
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDrivingLicenseInfo frm = new frmShowDrivingLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
