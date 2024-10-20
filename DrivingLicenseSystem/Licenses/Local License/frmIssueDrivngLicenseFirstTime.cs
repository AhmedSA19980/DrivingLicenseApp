﻿using DLSBusinessLayer;
using DrivingLicenseSystem.ClassGlobal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Licenses.Local_License
{
    public partial class frmIssueDrivngLicenseFirstTime : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplication;
     
        public frmIssueDrivngLicenseFirstTime(int localDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
        }

        private void frmIssueDrivngLicenseFirstTime_Load(object sender, EventArgs e)
        {
 

            txtNote.Focus();
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + _LocalDrivingLicenseApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (!_LocalDrivingLicenseApplication.PassedAllTests())
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int ActiveLicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();
            if(ActiveLicenseID != -1)
            {
                MessageBox.Show("Person already has License before with License ID=" + ActiveLicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);


        }

        private void ctrlLicenseApplicationInfo1_Load(object sender, EventArgs e)
        {
           




        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssuesLicense_Click(object sender, EventArgs e)
        {
  

            int LicenseID = _LocalDrivingLicenseApplication.IssueLicenseForTheFirtTime(txtNote.Text.Trim() , clsGlobal.CurrentUser.UserID);
            
            if(LicenseID != -1)
            {

                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
               
             
            }

            else
            {
                MessageBox.Show("License Was not Issued ! ",
                  "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
    }
}
