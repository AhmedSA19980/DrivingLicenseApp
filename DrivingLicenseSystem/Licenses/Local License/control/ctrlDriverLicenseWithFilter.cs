using DLSBusinessLayer;
using DrivingLicenseSystem.Licenses.internationalLicense;
using DrivingLicenseSystem.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Licenses.control
{
    public partial class ctrlDriverLicenseWithFilter : UserControl
    {

        public ctrlDriverLicenseWithFilter()
        {
            InitializeComponent();
        }

        // Define a custom event handler delegate with parameters
        public event Action<int> OnLicenseSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID); // Raise the event with the parameter
            }
        }


        private int _LicenseID = -1;
        public bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set { _FilterEnabled = value; gbFilters.Enabled = _FilterEnabled;  }
        
        }
        public int LicenseID
        {
            get { return ctrlDrivingLicenseInfo1.LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return ctrlDrivingLicenseInfo1.LicenseInfo; }
        }

        public void LoadDrivingLicenseInfo(int LicenseID)
        {
           // _NewLicenseID = _SelectedLicenseID; 

          
            txtLicenseID.Text  =LicenseID.ToString();

            ctrlDrivingLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID =  ctrlDrivingLicenseInfo1.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
            {
                OnLicenseSelected(_LicenseID);
            }
 
           
          
        } 

   

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadDrivingLicenseInfo(_LicenseID);

           
        
        }



        public void TxtLicenseIDFocus()
        {
            txtLicenseID.Focus();   
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtLicenseID, null);
            }
        }
    }
}
