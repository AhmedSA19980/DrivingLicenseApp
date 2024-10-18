using DrivingLicenseSystem.Applications;
using DrivingLicenseSystem.Applications.Release_Licnese;
using DrivingLicenseSystem.Applications.Renew_Applications;
using DrivingLicenseSystem.Applications.Replace_License;
using DrivingLicenseSystem.ClassGlobal;
using DrivingLicenseSystem.Driver;
using DrivingLicenseSystem.DrivingLicense.NewLocalDrivingLicense;
using DrivingLicenseSystem.Licenses.Detain_License;
using DrivingLicenseSystem.Licenses.internationalLicense;
using DrivingLicenseSystem.Login;
using DrivingLicenseSystem.ManageApplications.InternationalDrivingLicenseApplications;
using DrivingLicenseSystem.ManageApplications.LocalDrivinglicenseApplications;
using DrivingLicenseSystem.Person;
using DrivingLicenseSystem.Tests.TestTypes;
using DrivingLicenseSystem.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem
{
    public partial class Form1 : Form
    {
        frmLogin _frmLogin;
        public Form1(frmLogin Login)
        {
            InitializeComponent();
            _frmLogin = Login;
        }

        private void signOutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePeople frmManagePeople = new frmManagePeople();
            frmManagePeople.ShowDialog();   
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers frmListUsers = new frmListUsers();
            frmListUsers.ShowDialog();   

        }

        private void uToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserInfo frmShowUserInfo = new frmShowUserInfo(clsGlobal.CurrentUser.UserID);
            frmShowUserInfo.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangeUserPassword frmChangeUserPassword = new frmChangeUserPassword(clsGlobal.CurrentUser.UserID);
            frmChangeUserPassword.ShowDialog();   

        }

        private void manageApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApplicationTypes frmApplicationTypes = new frmApplicationTypes();
            frmApplicationTypes.ShowDialog();   
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestTypes frmTestTypes = new frmTestTypes();
            frmTestTypes.ShowDialog();   
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateDrivingLicense frmNewDrivingLicense = new frmAddUpdateDrivingLicense();
            frmNewDrivingLicense.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageLocalDrivingLicenseApplications frmManageLocalDrivingLicenseApplications = new frmManageLocalDrivingLicenseApplications();
            frmManageLocalDrivingLicenseApplications.ShowDialog();   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        
            this.Refresh();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frmListDrivers = new frmListDrivers();
            frmListDrivers.ShowDialog();   

        }

        private void internationalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalLicense frmAddNewInternationalLicense = new frmAddNewInternationalLicense();
            frmAddNewInternationalLicense.ShowDialog(this);  
        }

        private void internationalDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageInternationalDrivingLicenseApplications frmManageInternationalDrivingLicenseApplications = new frmManageInternationalDrivingLicenseApplications();
            frmManageInternationalDrivingLicenseApplications.ShowDialog();   
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicense frmRenewLocalDrivingLicense = new frmRenewLocalDrivingLicense();
            frmRenewLocalDrivingLicense.ShowDialog();   
        }

        private void replaceForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceForDamageOrLostLicense frmReplaceForDamageOrLostLicense = new frmReplaceForDamageOrLostLicense();
            frmReplaceForDamageOrLostLicense.ShowDialog();   
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frmDetainLicense = new frmDetainLicense();
            frmDetainLicense.ShowDialog();   
        }

        private void releaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainLicense frm= new frmReleaseDetainLicense(); 
            frm.ShowDialog();   
        }

        private void manageDetainLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainLicenses frmListDetainLicenses = new frmListDetainLicenses();
            frmListDetainLicenses.ShowDialog();   
        }

        private void releasedDetainedLicnenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainLicense frmReleaseDetainLicense = new frmReleaseDetainLicense();
            frmReleaseDetainLicense.ShowDialog();   
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageLocalDrivingLicenseApplications frmManageLocalDrivingLicenseApplications = new frmManageLocalDrivingLicenseApplications();
            frmManageLocalDrivingLicenseApplications.ShowDialog();  


        }
    }
}
