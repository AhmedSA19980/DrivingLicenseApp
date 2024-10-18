using DLSBusinessLayer;
using DrivingLicenseSystem.ClassGlobal;
using DrivingLicenseSystem.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Licenses.internationalLicense.control
{
    public partial class ctrlInternationalDrivingLicenseInfo : UserControl
    {
        public ctrlInternationalDrivingLicenseInfo()
        {
            InitializeComponent();
        }

   
        clsInternationalLicense _InternationalLicense;
        private int _InternationalLicenseID = -1;
        public clsInternationalLicense LicenseInfo
        {
            get { return _InternationalLicense; }
        }
        public int IntenationalLicenseID
        {
            get { return _InternationalLicenseID; }
        }

        private void _LoadPersonImage()
        {
            if (_InternationalLicense.DriverInfo.PersonInfo.Gender == "Male")
                pbPersonalImage.Image = Resources.Male_512;
            else
                pbPersonalImage.ImageLocation = "D:/user_female.png";

            string ImagePath = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonalImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


        public void LoadInternationalInfo(int LicenseID)
        {
            _InternationalLicenseID = LicenseID;
            _InternationalLicense = clsInternationalLicense.Find(_InternationalLicenseID);
            if (_InternationalLicense == null)
            {
                MessageBox.Show("License Info Does not Exist !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

          
            lblFullName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = _InternationalLicense.DriverInfo.PersonInfo.Gender;
            lblIssueDate.Text = clsFormat.DateToShort(_InternationalLicense.IssueDate);
         
           lblApplicationId.Text = _InternationalLicense.ApplicationID.ToString();  
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _InternationalLicense.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(_InternationalLicense.ExpirationDate);
        

            _LoadPersonImage();
        }

        private void ctrlInternationalDrivingLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
