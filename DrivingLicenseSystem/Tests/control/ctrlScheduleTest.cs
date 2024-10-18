using DLSBusinessLayer;
using DLSDATA;
using DrivingLicenseSystem.ClassGlobal;
using DrivingLicenseSystem.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Tests.control
{
    public partial class ctrlScheduleTest : UserControl
    {


        clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplication;
        clsTestAppointment _TestAppointments;

        private int _LDLAppID=-1;
        private int _TestAppointmentID = -1;
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;


        public enum enCreatTionMode { FirstTimeSchedule = 0  , RetakeScheduleTest  = 1 }

        enCreatTionMode _CreationMode = enCreatTionMode.FirstTimeSchedule;

        private  clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;

        public ctrlScheduleTest()
        {
            InitializeComponent();


        }

        // any property has get and set will be shown in a control porperites in misc section 
        public clsTestTypes.enTestType  TestTypeID {


            get {  return _TestTypeID; }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID) {

                    case clsTestTypes.enTestType.VisionTest:
                        gbTestType.Text = "Vision Test";
                        pbTestType.Image = Resources.Vision_512;

                        break;


                    case clsTestTypes.enTestType.WrittenTest:
                        gbTestType.Text = "WrittenTest Test";
                        pbTestType.Image = Resources.Written_Test_512;

                        break;
                    

                    case clsTestTypes.enTestType.StreetTest:
                        gbTestType.Text = "Street Test";
                        pbTestType.Image = Resources.driving_test_512 ;
                        break; 
                }
            }
        
        }


        public void _LoadInfo(int LocalDrivingLicenseApplicationID, int TestAppointmentID = -1)
        {
            //  _LoadDrivingLicenseData();

            if (TestAppointmentID != -1)

                _Mode = enMode.Update;

            else
                _Mode = enMode.AddNew;

            _LDLAppID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = TestAppointmentID;
            _LDLAppID = LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseAppID(_LDLAppID);
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("LocalDrivingLicense with" + _LDLAppID.ToString() + " is Not Exist !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;

            }

            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestTypeID))

                _CreationMode = enCreatTionMode.RetakeScheduleTest;

            else
                _CreationMode = enCreatTionMode.FirstTimeSchedule;


            if (_CreationMode == enCreatTionMode.RetakeScheduleTest)
            {
                lblRetakeTestFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString();
                gbTestRetakeTest.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestID.Text = "0";
            }
            else
            {
                gbTestRetakeTest.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeTestFees.Text = "0";
                lblRetakeTestID.Text = "N/A";
            }


            lblLDLAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDClassType.Text = _LocalDrivingLicenseApplication.clsLicenseClassesInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.FullName;
            //lblLocalFees.Text = clsTestTypes.Find(_TestType).TestTypeFees.ToString();


            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();


            //   _LoadDrivingLicenseData();

            // _LoadTestAppointmentData(TestAppointmentID);



            if (_Mode == enMode.AddNew)
            {

                lblMsg.Visible = false;
                lblLocalFees.Text = clsTestTypes.Find(_TestTypeID).TestTypeFees.ToString();
                dateTimePicker1.MinDate = DateTime.Now;
                lblRetakeTestID.Text = "N/A";
                _TestAppointments = new clsTestAppointment();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }


            //gbTestRetakeTest.Enabled = true;
            // lblRetakeTestFees.Text = "5";

            lblTotalFees.Text = (Convert.ToSingle(lblLocalFees.Text) + Convert.ToSingle(lblRetakeTestFees.Text)).ToString();


            if (!_HandleActiveIslockedContraint())
                return;
            if (!HandlePreviousTestConstraint())
                return;
        }
        public bool lblErrorMessage
        {
            set { lblMsg.Visible = value; }    
        }

        public bool _LoadTestAppointmentData()
        {
            _TestAppointments = clsTestAppointment.FindTestAppointmentByID(_TestAppointmentID);
            if (_TestAppointments == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;

            }

            lblLocalFees.Text = _TestAppointments.PaidFees.ToString();
            if( DateTime.Compare(DateTime.Now  , _TestAppointments.AppointmentDate) <  0)
            
                dateTimePicker1.MinDate = DateTime.Now;
            else
                dateTimePicker1.MinDate  = _TestAppointments.AppointmentDate;   
            
            dateTimePicker1.Value =  _TestAppointments.AppointmentDate  ;
            
            if(_TestAppointments.RetakeTestApplicationID == -1)
            {
                lblRetakeTestFees.Text = "0";
                lblRetakeTestID.Text = "N/A";
            }
            else
            {
                lblRetakeTestFees.Text = _TestAppointments.RetakeTestAppInfo.PaidFees.ToString();
                gbTestRetakeTest.Enabled = true;
                lblTitle.Text = "Schedule Retake Test!";
                lblRetakeTestID.Text = _TestAppointments.RetakeTestApplicationID.ToString();    
            }
            
            //_TestAppointments.TestAppointmentID = _TestAppointmentID;
          //  _TestAppointments.AppointmentDate = dateTimePicker1.Value;

            return true;  // check this line before run the application
        }


        public void _LoadDrivingLicenseData()
        {
            //
           



         
        }

        private bool _HandleActiveIslockedContraint()
        {

            if (_TestAppointments.IsLocked)
            {
                btnSave.Enabled  =false;
                dateTimePicker1.Enabled =false;
                return false;
            }


            return true;
     
        }


        private bool _HandleRetakeApplication()
        {
            // seperate an application for retaking test , when test appointment is failed than candidate need to reimpliment test
            //  create another application for faield test 

            if (_Mode == enMode.AddNew && _CreationMode == enCreatTionMode.RetakeScheduleTest)
            {

                clsApplication Application = new clsApplication();
                Application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees =clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees; // clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;



                if (!Application.Save())
                {
                    _TestAppointments.RetakeTestApplicationID = -1;
                    MessageBox.Show("Failed to create an application !","Failed" ,MessageBoxButtons.OK ,MessageBoxIcon.Error);
                    return false;   
                }

                _TestAppointments.RetakeTestApplicationID = Application.ApplicationID; 
               

            }
            return true;


          


           
        }



        private bool HandlePreviousTestConstraint()
        {

            switch (TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    lblMsg.Visible = false;
                    return true;

                case clsTestTypes.enTestType.WrittenTest:
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.VisionTest))
                    {
                        lblMsg.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblMsg.Visible = true;
                        btnSave.Visible = false;    
                        dateTimePicker1.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblMsg.Visible = false;
                        btnSave.Enabled = true;
                        dateTimePicker1.Enabled = true;
                    }    
                    return true;

                case clsTestTypes.enTestType.StreetTest:
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.WrittenTest))
                    {
                        lblMsg.Text = "Cannot Sechule, Written Test should be passed first";
                        lblMsg.Visible = true;
                        btnSave.Visible = false;
                        dateTimePicker1.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblMsg.Visible = false;
                        btnSave.Enabled = true;
                        dateTimePicker1.Enabled = true;
                    }
                    return true;

            }

            return true; 
        }
        private void ctrlScheduleTest_Load(object sender, EventArgs e)
        {
      
        }

        private void btnSave_Click(object sender, EventArgs e)

        {

            if (!_HandleRetakeApplication())
                return;

            _TestAppointments.TestTypeID = _TestTypeID;// check this line
            _TestAppointments.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointments.AppointmentDate = dateTimePicker1.Value;
            _TestAppointments.PaidFees = Convert.ToSingle(lblLocalFees.Text);
            //_TestAppointments.IsLocked = false;
            _TestAppointments.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if (_TestAppointments.Save())
            {

                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not saved successfuly" , "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);    
                return;
            }
       
        }

        private void 
            gbTestType_Enter(object sender, EventArgs e)
        {

        }
    }
}
