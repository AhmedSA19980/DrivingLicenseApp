using DLSBusinessLayer;
using DLSDATA;
using DrivingLicenseSystem.DrivingLicense.NewLocalDrivingLicense;
using DrivingLicenseSystem.Licenses;
using DrivingLicenseSystem.Licenses.Local_License;
using DrivingLicenseSystem.Tests.DrivingTests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.ManageApplications.LocalDrivinglicenseApplications
{
    public partial class frmManageLocalDrivingLicenseApplications : Form
    {

        clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplication;
        private int LocalDrivingLicenseID = -1;
        private DataTable _dtAllLocalApplications;
        public frmManageLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        
        }


       
        private void frmManageLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
           
            _dtAllLocalApplications= clsLocalDrivingLicenseApplications.GetLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApp.DataSource = _dtAllLocalApplications; 

           
            lblRecordsCount.Text = dgvLocalDrivingLicenseApp.Rows.Count.ToString();
            if (dgvLocalDrivingLicenseApp.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApp.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApp.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApp.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApp.Columns[1].Width = 300;
                    
                dgvLocalDrivingLicenseApp.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApp.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApp.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApp.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApp.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApp.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApp.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApp.Columns[5].Width = 150;
            }
            cbFilterBy.SelectedIndex = 0;
        }


       


        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";


            switch(cbFilterBy.Text) { 
              case "L.D.L.AppID":
                FilterColumn = "LocalDrivingLicenseApplicationID";
                break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApp.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                //in this case we deal with integer not string.
                _dtAllLocalApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllLocalApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text =  dgvLocalDrivingLicenseApp.Rows.Count.ToString();





        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            _dtAllLocalApplications.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvLocalDrivingLicenseApp.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = (int)dgvLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;
            frmAddUpdateDrivingLicense frm = new frmAddUpdateDrivingLicense(ApplicationID);

            frm.ShowDialog();   
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete this Application ", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;


            clsLocalDrivingLicenseApplications LocalApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
            if (LocalApplication != null)

            {

                //Perform Delele and refresh
                if (LocalApplication.CancelApplication())
                {
                    MessageBox.Show("Application cancelled Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmManageLocalDrivingLicenseApplications_Load(null, null);
                }

                else
                    MessageBox.Show("Could not cancel applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

           
        }

        private void _ScheduleTest(clsTestTypes.enTestType TestTypeID)
        {
            int LDAppID = (int)dgvLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LDAppID, TestTypeID);
            frm.ShowDialog();
            frmManageLocalDrivingLicenseApplications_Load(null, null);
        }
        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* int LDAppID = (int)dgvLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;


             clsTestAppointment Appointment = clsTestAppointment.GetLastTestAppointment(LDAppID, clsTestTypes.enTestType.VisionTest);
             if (Appointment == null)
             {
                 MessageBox.Show("No vision Test Appointment Found!", "Set Appointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }

             frmVisiontestAppointment frm = new frmVisiontestAppointment(LDAppID ,clsTestTypes.enTestType.VisionTest);
             frm.ShowDialog();
            */

            _ScheduleTest(clsTestTypes.enTestType.VisionTest);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmAddUpdateDrivingLicense frm = new frmAddUpdateDrivingLicense();
            frm.ShowDialog();
            frmManageLocalDrivingLicenseApplications_Load(null, null);
        }



        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*int LDAppID = (int)dgvLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;
            if (!clsLocalDrivingLicenseApplications.DoesPassTestType( LDAppID,clsTestTypes.enTestType.VisionTest))// try to change instance object to static obj
            {
                MessageBox.Show("Person Should Pass the Vision Test First!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsTestAppointment Appointment = clsTestAppointment.GetLastTestAppointment(LDAppID, clsTestTypes.enTestType.WrittenTest);

            if (Appointment == null)
            {
                MessageBox.Show("No Written Test Appointment Found!", "Set Appointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmVisiontestAppointment frm = new frmVisiontestAppointment(LDAppID, clsTestTypes.enTestType.WrittenTest);
            frm.ShowDialog();*/
            _ScheduleTest(clsTestTypes.enTestType.WrittenTest);

        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* int LDAppID = (int)dgvLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;
             if (!clsLocalDrivingLicenseApplications.DoesPassTestType(LDAppID,clsTestTypes.enTestType.StreetTest))
             {

                 scheduleStreetTestToolStripMenuItem.Enabled = false;
             }
             clsTestAppointment Appointment = clsTestAppointment.GetLastTestAppointment(LDAppID, clsTestTypes.enTestType.StreetTest);

             if (Appointment == null)
             {
                 MessageBox.Show("No Street Test Appointment Found!", "Set Appointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }

             frmVisiontestAppointment frm = new frmVisiontestAppointment(LDAppID, clsTestTypes.enTestType.StreetTest);
             frm.ShowDialog();*/

            _ScheduleTest(clsTestTypes.enTestType.StreetTest);

        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LDAppID = (int)dgvLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseAppID(LDAppID);

            scheduleVisionTestToolStripMenuItem.Enabled = true;
            scheduleWrittenTestToolStripMenuItem.Enabled = false;
            scheduleStreetTestToolStripMenuItem.Enabled = false;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled=false;

            cancelApplicationToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = true;
            deleteApplicationToolStripMenuItem.Enabled = true;
            showLicenseToolStripMenuItem.Enabled = false;



            if (_LocalDrivingLicenseApplication.GetPassedTestCount() == 1)
            {
                scheduleVisionTestToolStripMenuItem.Enabled=false;
                scheduleWrittenTestToolStripMenuItem.Enabled = true;

            }else if (_LocalDrivingLicenseApplication.GetPassedTestCount() == 2)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = true;
            }
            else
            {
                scheduleStreetTestToolStripMenuItem.Enabled = false;
            }


            if (clsLocalDrivingLicenseApplications.PassedAllTests(LDAppID))
            {
                ScheduleTestsMenuItem.Enabled  =false;  
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled=true;
            }
            else
            {
                ScheduleTestsMenuItem.Enabled = true;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            }


            string LDAppStatus = (string)dgvLocalDrivingLicenseApp.CurrentRow.Cells[6].Value;

            if (LDAppStatus == "Compeleted" || LDAppStatus =="Cancelled")
            {
                cancelApplicationToolStripMenuItem.Enabled = false;
                editToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;

                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = true;
            }
           

            //_RefreshLocalDrivingLicenseApplications();




        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;
            frmIssueDrivngLicenseFirstTime frm = new frmIssueDrivngLicenseFirstTime(LocalDrivingLicenseApplicationID);
            

            frm.ShowDialog();
            frmManageLocalDrivingLicenseApplications_Load(null, null);


        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;
            int LicenseID = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID).GetActiveLicenseID();
            if (LicenseID != -1)
            {

                frmShowDrivingLicenseInfo frm = new frmShowDrivingLicenseInfo(LicenseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)

        { 
            

          

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplications localDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
       

                frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_LocalDrivingLicenseApplication.ApplicantPersonID);
                frm.ShowDialog();
            
          
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete this Application ", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;


            clsLocalDrivingLicenseApplications LocalApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
            if (LocalApplication != null)

                {

                //Perform Delele and refresh
                if (LocalApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmManageLocalDrivingLicenseApplications_Load(null, null);
                }

                else
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApp.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManageLocalDrivingLicenseApplications_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
