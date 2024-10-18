using DLSBusinessLayer;
using DLSDATA;
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

namespace DrivingLicenseSystem.ManageApplications
{
    public partial class ctrlLicenseApplicationInfo : UserControl
    {

        private int _ApplicationID;
        private int _LicenseID;
        clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplication;
        clsApplication _clsApplication;
        public ctrlLicenseApplicationInfo()
        {
            InitializeComponent();
        }

  
       


        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
               // _ResetLocalDrivingLicenseApplicationInfo();


                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
          
            _FillLocalDrivingLicenseApplicationInfo();
        }


       private void _FillLocalDrivingLicenseApplicationInfo()
        {
           _LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();

            lblShowLicenseInfo.Enabled = (_LicenseID != -1);
            lblLDLAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = clsLicenseClasses.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;
            lblNumberOfTestPassed.Text = _LocalDrivingLicenseApplication.GetPassedTestCount().ToString() + "/3";

            ctrlApplicationBasicInfo1.LoadApplicationData(_LocalDrivingLicenseApplication.ApplicationID);
                
        }

        private void lblShowLicenseInfo_Click(object sender, EventArgs e)
        {
            frmShowDrivingLicenseInfo formShowDrivingLicenseInfo = new frmShowDrivingLicenseInfo(_LicenseID);
            formShowDrivingLicenseInfo.ShowDialog();
        }
    }
}
