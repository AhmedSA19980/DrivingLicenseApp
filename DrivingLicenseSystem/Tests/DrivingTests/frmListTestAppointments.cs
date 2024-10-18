using DLSBusinessLayer;
using DrivingLicenseSystem.Properties;
using DrivingLicenseSystem.Tests.control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Tests.DrivingTests
{
    public partial class frmListTestAppointments : Form
    {

        DataTable _dtLicenseTestAppointment;
        private int _LocalDrivingLicenseApplicationID ;
      
        
        private clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;
        
        public frmListTestAppointments(int LDLAppID ,clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            this._LocalDrivingLicenseApplicationID = LDLAppID;
           _TestType = TestTypeID;  
   
            
        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {

                case clsTestTypes.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    }

                case clsTestTypes.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }
                case clsTestTypes.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }

  

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();
            ctrlLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);// note we need to add issues license function

            _dtLicenseTestAppointment = clsTestAppointment.GetApplicationTestAppointmentPerTestType(_LocalDrivingLicenseApplicationID, _TestType);
            dgvTestAppointments.DataSource = _dtLicenseTestAppointment;
            lblRecordsCount.Text = dgvTestAppointments.Rows.Count.ToString();

            if (dgvTestAppointments.Rows.Count > 0)
            {
                dgvTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvTestAppointments.Columns[0].Width = 150;

                dgvTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvTestAppointments.Columns[1].Width = 200;

                dgvTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvTestAppointments.Columns[2].Width = 150;

                dgvTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvTestAppointments.Columns[3].Width = 100;
            }


        }



        private void AddNewTestAppointment_Click(object sender, EventArgs e)
        {
           clsLocalDrivingLicenseApplications LocalApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);
        
           
            int isActive =clsTestAppointment.CheckIsActiveTestAppointmentIDTestAppointment(_TestType , LocalApplication.LocalDrivingLicenseApplicationID);
            if(isActive != -1) {
                MessageBox.Show("Person Already have an active appointment for this test, you cannot add new appointment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            clsTest LastTest = LocalApplication.GetLastTestPerTestType(_TestType);
        
            if(LastTest == null)
            {
                frmScheduleTest frm1 = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
                frm1.ShowDialog();
                frmListTestAppointments_Load(null , null);
                return;
            }
            if(LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                frmScheduleTest frm = new frmScheduleTest(LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID,_TestType);
                frm.ShowDialog();
            frmListTestAppointments_Load(null ,null);
      
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int   TestAppointmentID = (int)dgvTestAppointments.CurrentRow.Cells[0].Value; 

            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID ,_TestType ,TestAppointmentID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
           int  TestAppointmentID = (int)dgvTestAppointments.CurrentRow.Cells[0].Value;
            
            frmTakeTest frm = new frmTakeTest(TestAppointmentID , _TestType);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
