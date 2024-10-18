using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Licenses.internationalLicense
{
    public partial class frmInternationalDrivingLicenseInfo : Form
    {
        private int _IntLicenseID; 
        public frmInternationalDrivingLicenseInfo(int IntLicenseID)
        {
            InitializeComponent();
            _IntLicenseID = IntLicenseID;       
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void frmInternationalDrivingLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlInternationalDrivingLicenseInfo1.LoadInternationalInfo(_IntLicenseID);
        }
    }
}
