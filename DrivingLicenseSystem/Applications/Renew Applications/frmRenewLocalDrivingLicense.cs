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

namespace DrivingLicenseSystem.Applications.Renew_Applications
{
    public partial class frmRenewLocalDrivingLicense : Form
    {
        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
        }
       
        private int _NewLicenseID;
        private void ctrlDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            int LicenseID = obj;
            lblOldLocalLicenseID.Text = LicenseID.ToString();
            llShowLicenseHistory.Enabled = (LicenseID != -1);
            if (LicenseID == -1)
            {
               // MessageBox.Show("License ID  is not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
              
            }
            int DefaultValidityLength = ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.LicenseClassesInfo.DefaultValidityLength;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(DefaultValidityLength));
            lblAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();
            lblLicenseFees.Text = clsLicenseClasses.Find(ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.LicenseClassID).ClassFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblTotalFees.Text = (Convert.ToSingle(lblAppFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();



            bool ExpDate = ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.IsLicenseExpired();
            
            if (!ExpDate)
            {
                MessageBox.Show("License is Active,It Will expire on " + ExpDate, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not yet Active, Choose another one !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnRenewLicense.Enabled = true;

        }


        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
           
            lblApplicationDate.Text =clsFormat.DateToShort( DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = "???";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void RenewLicense_Click(object sender, EventArgs e)
        {
            string note = (txtNote.Text != "" ? txtNote.Text : "No Note").Trim();
          
            
            clsLicense NewLicense = ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.RenewLicense(note, clsGlobal.CurrentUser.UserID);

            if (MessageBox.Show("Are you sure you want to renew this license " , "Confirm" , MessageBoxButtons.YesNo ,MessageBoxIcon.Question) == DialogResult.No) {

                return;

            }
            else
            {

                if (NewLicense != null)
                {
                    MessageBox.Show("License Renewed Successfully with id =" + NewLicense.LicenseID, "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _NewLicenseID = NewLicense.LicenseID;

                    lblApplicationID.Text = NewLicense.ApplicationID.ToString();
                    lblRenewedLicenseID.Text = _NewLicenseID.ToString();

                    llShowLicenseInfo.Enabled = true;

                    ctrlDriverLicenseWithFilter1.FilterEnabled = false;
                    btnRenewLicense.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }




        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDrivingLicenseInfo frm = new frmShowDrivingLicenseInfo(_NewLicenseID);
            frm.ShowDialog();   
        }

        private void frmRenewLocalDrivingLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseWithFilter1.TxtLicenseIDFocus();
        }
    }
}
