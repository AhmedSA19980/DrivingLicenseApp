using DLSBusinessLayer;
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

namespace DrivingLicenseSystem.Tests.DrivingTests
{
    public partial class frmTakeTest : Form
    {
        clsTestAppointment _TestAppointment;
        clsTest _Test;

        private int _TestAppointmentID ;

        private clsTestTypes.enTestType _TestType; 
        public frmTakeTest(int TestAppointmentID , clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestType = TestTypeID;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {


            if(MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?." ,"Confirm",
                MessageBoxButtons.YesNo , MessageBoxIcon.Warning) == DialogResult.No){

                return;
            }

           
            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestResult =  rbPassed.Checked ;
            _Test.Notes = txtNote.Text.Trim() ;
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;   



            if(_Test.Save()){

              //  _TestAppointment.IsLocked = true;
                MessageBox.Show("Data is Saved Successfully" , "success" , MessageBoxButtons.OK , MessageBoxIcon.Information );
                btnSave.Enabled = false;

            }
            else
                MessageBox.Show("Error: Saving Data", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        private void frmTakeTest_Load(object sender, EventArgs e)
        {

            ctrlScheduledTest1.TestType = _TestType ;
            ctrlScheduledTest1.LoadInfo(_TestAppointmentID);
  

            if (ctrlScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;


            int TestID = ctrlScheduledTest1.TestID;
            if (TestID != -1)
            {
                _Test = clsTest.Find(TestID);

                if (_Test.TestResult)
                    rbPassed.Checked= true;
                else
                    rbFailed.Checked = true;
                    txtNote.Text = _Test.Notes;


                lblUserMessage.Visible = true;
                rbFailed.Enabled = false;
                rbPassed.Enabled = false;
               
               btnSave.Enabled= false; 
            }
            else
                _Test = new clsTest();
        }

  
    }
}
