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

namespace DrivingLicenseSystem.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string username = "", password = "";

            if (clsGlobal.GetStoredCredential(ref username, ref password))
            {
                txtUserName.Text = username;
                txtPassword.Text = password;

            }
            else
            {
                chkRememberMe.Checked = false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.FindByNameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());




            if (user != null)
            {

                if (chkRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");
                }


                if (!user.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your Account is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                clsGlobal.CurrentUser = user;
                this.Hide();
                Form frm = new Form1(this);
                frm.ShowDialog();

            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("UserName/Passwor is Invalid !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
