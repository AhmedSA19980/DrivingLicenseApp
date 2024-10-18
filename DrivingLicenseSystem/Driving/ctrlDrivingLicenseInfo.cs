using DLSBusinessLayer;
using DrivingLicenseSystem.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace DrivingLicenseSystem.Licenses.Local_License.control
{
    public partial class ctrlDrivingLicenseInfo : UserControl
    {

        clsLicense _License;
        private int _LicenseID;
        public ctrlDrivingLicenseInfo()
        {
            InitializeComponent();
        }


        public clsLicense LicenseInfo
        {
            get { return _License; }    
        }
        public int LicenseID
        {
            get { return _LicenseID; }
        }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.Gender == "Male")
                pbPersonalImage.Image = Resources.Male_512;
            else
                pbPersonalImage.ImageLocation = "D:/user_female.png";

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonalImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void ctrlDrivingLicenseInfo_Load(object sender, EventArgs e)
        {
            _License = clsLicense.Find(_LicenseID);
            if (_License == null)
            {
                MessageBox.Show("License Info Does not Exist !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            lblClassName.Text = _License.LicenseClassesInfo.ClassName;
            lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _LicenseID.ToString();
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = _License.DriverInfo.PersonInfo.Gender;
            lblIssueDate.Text = _License.IssueDate.ToString();
            //lblIssuesReason.Text = _License.IssueReason.ToString();

            lblNote.Text = _License.Notes;
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _License.DriverInfo.PersonInfo.DateOfBirth.ToString();
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToString();
            lblIsDetaiend.Text = "No"; // this result must be improved 

            _LoadPersonImage();
        }
    }
}
