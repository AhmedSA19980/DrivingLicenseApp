using DLSBusinessLayer;
using DLSDATA;
using DrivingLicenseSystem.ClassGlobal;
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

namespace DrivingLicenseSystem.ManageApplications.LocalDrivinglicenseApplications.control
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        clsApplication _Application;
        private int _ApplicationID = -1;
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        string Status(short status)
        {
            if (status == 1) return "New";
            else if (status == 2) return "Cancelled";
            else if (status == 3) return "Compeleted";

            return "0/3";

        }

        void _FillApplicationData()
        {


            lblAppID.Text = _Application.ApplicationID.ToString();
            lblAppStatus.Text = _Application.StatusText;
            lblAppFees.Text = _Application.PaidFees.ToString();
            lblAppType.Text = clsApplicationType.Find(_Application.ApplicationTypeID).ApplicationTypeTitle;
            lblAppApplicant.Text = _Application.FullName;
            lblAppDate.Text = clsFormat.DateToShort(_Application.ApplicationDate);
            lblAppStatusDate.Text = clsFormat.DateToShort(_Application.LastStatusDate);
            lblAppCreatedBy.Text = clsUser.FindUserById(_Application.CreatedByUserID).UserName;
        }


        public void LoadApplicationData(int ApplicationID)
        {
          
            _Application = clsApplication.FindBaseApplication(ApplicationID);
            _ApplicationID = ApplicationID;
            if (  _Application == null)
            {
                MessageBox.Show("Application is Not Exist !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _FillApplicationData();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_Application.ApplicantPersonID);
            frm.ShowDialog();
            LoadApplicationData(_ApplicationID);

        }
    }
}
