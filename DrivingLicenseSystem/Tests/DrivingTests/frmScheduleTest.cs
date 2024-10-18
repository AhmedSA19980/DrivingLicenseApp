using DLSBusinessLayer;
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
    public partial class frmScheduleTest : Form
    {
        private int _TestAppointmentID = -1;

        private int _LDLAppID = -1;
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;


        public frmScheduleTest(int LocalApplicationID ,clsTestTypes.enTestType TestTypeID, int TestAppointment= -1) // i sat the second argument val to -1 ,prev was nothing
        {
            InitializeComponent();
            _LDLAppID= LocalApplicationID;
            _TestTypeID= TestTypeID;    
            _TestAppointmentID= TestAppointment;    

        }

     



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void ctrlScheduleTest1_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestTypeID = _TestTypeID;
            ctrlScheduleTest1._LoadInfo(_LDLAppID , _TestAppointmentID);
        }

       
    }
}
