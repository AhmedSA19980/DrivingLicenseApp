using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Licenses.Local_License
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        private int _ApplicationID = -1;
        public frmLocalDrivingLicenseApplicationInfo(int ApplicattionID)
        {
            InitializeComponent();
            _ApplicationID = ApplicattionID;    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();   
        
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_ApplicationID);
        }
    }
}
