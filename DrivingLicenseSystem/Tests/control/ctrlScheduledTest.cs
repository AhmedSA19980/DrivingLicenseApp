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
using static DLSBusinessLayer.clsTestTypes;

namespace DrivingLicenseSystem.Tests.control
{
    public partial class ctrlScheduledTest : UserControl
    {
        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        private int _LocalDrivingLicenseApplicationID = -1; 
      
        clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplication;
        private clsTestTypes.enTestType _TestTypeID ;

      

        public clsTestTypes.enTestType TestType
        {


            get { return _TestTypeID; }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case clsTestTypes.enTestType.VisionTest:
                        gbScheduledTest.Text = "Vision Test";
                        pbTestType.Image = Resources.Vision_512;
                        break;


                    case clsTestTypes.enTestType.WrittenTest:
                        gbScheduledTest.Text = "WrittenTest Test";
                        pbTestType.Image = Resources.Written_Test_512;
                        break;


                    case clsTestTypes.enTestType.StreetTest:
                        gbScheduledTest.Text = "Street Test";
                        pbTestType.Image = Resources.driving_test_512;
                        break;
                }
            }

        }


        
        public int TestAppointmentID { get { return _TestAppointmentID; } }

        public int TestID { get { return _TestID; } }


        private int _TestAppointmentID = -1;
        private int _TestID = -1;
        clsTestAppointment _TestAppointment;


        public void LoadInfo(int TestAppointmentID )
        {
            //  _LoadDrivingLicenseData();
            _TestAppointmentID = TestAppointmentID;

            _TestAppointment = clsTestAppointment.FindTestAppointmentByID(TestAppointmentID);
            if(_TestAppointment == null) {
                MessageBox.Show("Test Appointment With ID=" + TestAppointmentID.ToString() + " Is not Exist" , "error" ,  MessageBoxButtons.OK , MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            
            }

            _TestID = _TestAppointment.TestID;


            // add test add contructor

             _LocalDrivingLicenseApplicationID = _TestAppointment.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            lblLocalDrivingLicenseAppID.Text =_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString() ;
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.clsLicenseClassesInfo.ClassName ;
            lblFullName.Text =_LocalDrivingLicenseApplication.PersonFullName;
            

            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();
           
            
            
            lblTestID.Text = (_TestAppointment.TestID == -1) ? "Test not yet taken":_TestAppointment.TestID.ToString() ;
            lblDate.Text =_TestAppointment.AppointmentDate.ToString() ;
            lblFees.Text =_TestAppointment.PaidFees.ToString() ;
           

        }
    
    }
}
