using DLSBusinessLayer;
using DLSDATA;
using DrivingLicenseSystem.ClassGlobal;
using DrivingLicenseSystem.Licenses.control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Licenses.internationalLicense
{
    public partial class frmAddNewInternationalLicense : Form
    {
        // class 3 - licese is active  - expiration date is valid 


      
        private int _InternationalLicenseID;

        public frmAddNewInternationalLicense()
        {
            InitializeComponent();
        }
      

        


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {


            clsInternationalLicense InternationalLicense  =new clsInternationalLicense();  
          
              lblLocalLicenseID.Text =  ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.LicenseID.ToString();

            //Application
            InternationalLicense.ApplicantPersonID = ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
           

            InternationalLicense.DriverID = ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DriverID;            
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate =DateTime.Now  ;
            InternationalLicense.ExpirationDate =DateTime.Now.AddYears(1) ;


            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (InternationalLicense.Save())
            {
                MessageBox.Show("Data Created Successfully ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
                lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
                lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
               
                ctrlDriverLicenseWithFilter1.FilterEnabled = false;
                llShowLicenseInfo.Enabled = true;
                return;
            }
            else
            {
                MessageBox.Show("Erorr Data does not Saved", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



        }

        private void frmAddNewInternationalLicense_Load(object sender, EventArgs e)
        {

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblIssueDate.Text = lblApplicationDate.Text;
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }

        private void ctrlDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblLocalLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);
      
            if(SelectedLicenseID == -1 ) {

                return; 
            }

      

        
              lblLocalLicenseID.Text=ctrlDriverLicenseWithFilter1.LicenseID.ToString();
          

             if (ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.LicenseClassID != 3)
             {

               MessageBox.Show("License Does not match License type of 3 , class 3 must be selected ", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
              return;

              }

            int ActiveInternaionalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseByDriverId(ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DriverID);

            if (ActiveInternaionalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueLicense.Enabled = false;
                llShowLicenseInfo.Enabled = true;
               
                _InternationalLicenseID = ActiveInternaionalLicenseID;
                return;
            }
            else
            {

                btnIssueLicense.Enabled = true;
             
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalDrivingLicenseInfo frm = new frmInternationalDrivingLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();   
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();   
        }

        private void frmAddNewInternationalLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseWithFilter1.TxtLicenseIDFocus();
        }

      
    }
}
