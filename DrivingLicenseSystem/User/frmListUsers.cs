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
    public partial class frmListUsers : Form
    {

        private DataTable _dtUsers;
        public frmListUsers()
        {
            InitializeComponent();
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtUsers = clsUser.GetAllUsers(); ;
            dgvUsers.DataSource = _dtUsers;

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;

            if (dgvUsers.Rows.Count > 0)
            {
       

                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 110;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 120;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 350;

                dgvUsers.Columns[3].HeaderText = "UserName";
                dgvUsers.Columns[3].Width = 120;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 120;
            }
        }


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Is Active");
            cbIsActive.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text == "Is Active");
            if (txtFilterValue.Visible)
            {

                cbIsActive.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            else if (cbIsActive.Visible)
            {
                txtFilterValue.Visible = false;
                cbIsActive.SelectedIndex = 0;
            }


        }


    



        private void txtFilterBy_TextChanged_1(object sender, EventArgs e)
        {
            string selectIndex = cbFilterBy.Text;

            string FilterColumn = "";


            switch (selectIndex)
            {
                case "User ID":
                  
                    FilterColumn = "UserID";
                    break;
                case "Person ID":

                    FilterColumn = "PersonID";
                    break;
                case "User Name":
                    FilterColumn = "UserName";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }


            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblRecords.Text = dgvUsers.Rows.Count.ToString();
                return;
            }



            if (FilterColumn != "FullName" && FilterColumn != "UserName")
                //in this case we deal with numbers not string.
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtUsers.Rows.Count.ToString();


        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable _dtAllUsers = (DataTable)dgvUsers.DataSource;
            string selectedColumn = "IsActive";
            string selectedIsActiveIndex = cbIsActive.Text.Trim();


            switch (selectedIsActiveIndex)
            {
                case "All":

                    break;
                case "Yes":
                    selectedIsActiveIndex = "1";
                    break;
                case "No":
                    selectedIsActiveIndex = "0";
                    break;

            }

            if (selectedIsActiveIndex == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", selectedColumn, selectedIsActiveIndex);
            lblRecords.Text = _dtAllUsers.Rows.Count.ToString();
        }


        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            //we allow number incase person id is selected.
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


        }





        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete contact [" + dgvUsers.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsUser.DeleteUser((int)dgvUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully.");
                    frmListUsers_Load(null, null);
                }

                else
                    MessageBox.Show("Contact is not deleted.");

            }
        }

        private void addNewUserToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateUser();
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            Form frm = new frmShowUserInfo(UserID);
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void editToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            Form frm = new frmAddUpdateUser(UserID);
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void changePasswordToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            frmChangeUserPassword frm = new frmChangeUserPassword(UserID);
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void AddUser_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateUser();
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
