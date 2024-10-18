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
    public partial class ctrlUserCard : UserControl
    {

        private int _UserID = -1;
        private clsUser _User;


        public int PersonID
        {
            get { return _UserID; }

        }
        public ctrlUserCard()
        {
            InitializeComponent();
          
        }

   
     

        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.FindUserById(UserID);
            if (_User == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("This User ID Is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _FillUserInfo();
            //ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
        }


        void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;

            if (_User.IsActive)

                lblUserActivenessCase.Text = "Yes";

            else
                lblUserActivenessCase.Text = "No";

        }
        private void _ResetPersonInfo()
        {

            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblUserActivenessCase.Text = "[???]";
        }
    
    }
}
