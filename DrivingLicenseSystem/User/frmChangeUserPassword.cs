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

namespace DrivingLicenseSystem.User
{
   
    public partial class frmChangeUserPassword : Form
    {
        clsUser _User;
        private int _UserID;
        public frmChangeUserPassword(int userID)
        {
            InitializeComponent();
            _UserID = userID;
        }


        private void _ResetDefualtValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


           
            _User.Password = txtNewPassword.Text;
         

            if (_User.Save())
            {

                MessageBox.Show("Password Changed Successfully.",
                   "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefualtValues();
            }
            else
            {
                MessageBox.Show("An Erro Occured, Password did not change.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtBoxCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            string Password = txtCurrentPassword.Text.Trim();

            //DataTable dt = clsPeople.GetAllPeople();

            if (string.IsNullOrEmpty(Password))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "This Field cannot be empty !");
                return;
            }
            else if (_User.Password != txtCurrentPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Wrong Password, Enter the right Password !");

            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            string Password = txtNewPassword.Text.Trim();

            //DataTable dt = clsPeople.GetAllPeople();

            if (string.IsNullOrEmpty(Password))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "This Field cannot be empty !");
                return;
            }

            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }

        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            string ConPassword = txtConfirmPassword.Text.Trim();
       
            if (ConPassword != txtNewPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirm Passowrd Filed does Not Match Password Field !");
                return;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void frmChangeUserPassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            _User = clsUser.FindUserById(_UserID);
            if (_User == null)
            {
                MessageBox.Show("This User ID Is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlUserCard1.LoadUserInfo(_UserID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
