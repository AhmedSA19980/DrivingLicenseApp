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
    public partial class frmShowDrivingLicenseInfo : Form
    {
        private int _LicenseID;


        public frmShowDrivingLicenseInfo(int licenseID)
        {
            InitializeComponent();
            _LicenseID = licenseID;
        }

        private void frmShowDrivingLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseInfo1.LoadInfo(_LicenseID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
