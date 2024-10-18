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

namespace DrivingLicenseSystem.Applications
{
    public partial class frmEditApplicationType : Form
    {
        private int _AppTypeID; 
        clsApplicationType _AppTypes;
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _AppTypeID = ApplicationTypeID; 
        }

        private void _LoadApplicationType()
        {
            _AppTypes = clsApplicationType.Find(_AppTypeID);
            if (_AppTypes == null)
            {
                MessageBox.Show($"Application Type {_AppTypeID} not Fond", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }


            lblAppTypesID.Text = _AppTypeID.ToString();
            txtTitle.Text = _AppTypes.ApplicationTypeTitle;
            txtFees.Text = _AppTypes.ApplicationFees.ToString();

        }
        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _LoadApplicationType(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {

                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _AppTypes.ApplicationTypeTitle = txtTitle.Text;
            _AppTypes.ApplicationFees = Convert.ToSingle(txtFees.Text);

            if (_AppTypes.Save())
            {
//                lblAppTypesID.Text = _AppTypes.ApplicationTypeID.ToString();



                MessageBox.Show("Data Update Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data is not Update Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            string TxtAppTypeTitle = txtTitle.Text.Trim();

            //DataTable dt = clsPeople.GetAllPeople();

            if (TxtAppTypeTitle == "")
            {
                errorProvider1.SetError(txtTitle, "Title Field cannot be empty !");
            }

            else
            {
                errorProvider1.SetError(txtTitle, "");
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (txtFees.Text.Trim()!= null)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);



        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            };


            if (!clsValidations.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            };
        }
    }
}
