using DLSBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Person
{
    public partial class frmManagePeople : Form
    {
        private static DataTable _dtPeopleList = clsPerson.GetAllPeople();

       
        private DataTable _dtPeople = _dtPeopleList.DefaultView.ToTable(false ,
             "PersonID", "NationalNo",
              "FirstName", "SecondName", "ThirdName", "LastName",
              "Gender", "DateOfBirth", "CountryName",
              "Phone", "Email"
            );

        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
          //  _RefreshPoepleData();
            dgvPeople.DataSource = _dtPeople;   
            
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;

            if (dgvPeople.Rows.Count > 0)
            {

                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 120;


                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 140;


                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gender";
                dgvPeople.Columns[6].Width = 120;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 140;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;


                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;


                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 170;
            }
        }






        public void _RefreshPoepleData()
        {
               _dtPeopleList = clsPerson.GetAllPeople();
               _dtPeople = _dtPeopleList.DefaultView.ToTable(false,
             "PersonID", "NationalNo",
              "FirstName", "SecondName", "ThirdName", "LastName",
              "Gender", "DateOfBirth", "CountryName",
              "Phone", "Email"
            );

            dgvPeople.DataSource = _dtPeople;
            lblRecordsCount.Text  =dgvPeople.Rows.Count.ToString();    
    }
   

        private void dtvPoeple_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _RefreshPoepleData();
        }

    
     
        private void dgvPoeple_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(dgvPeople.CurrentRow.Cells[0].Value.ToString());
        }

      



        private void SwitchTxtBoxTypeBasedOnDataColumn(string Column)
        {
           
           
         
        }



        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string selectIndex = cbFilterBy.Text;

            string FilterColumn = "";

            switch (selectIndex)
            {
                case "Person ID":

                  
                    FilterColumn = "PersonID";
                    break;
                case "National No,":
                  
                    FilterColumn = "NationalNo";
                    break;
                case "First Name":
                    
                    FilterColumn = "FirstName";
                    break;
                case "Second Name":
                    
                    FilterColumn = "SecondName";
                    break;
                case "third Name":
                   
                    FilterColumn = "thirdName";
                    break;
                case "LastName":
                 
                    FilterColumn = "LastName";
                    break;
                case "Gender":
                   
                    FilterColumn = "Gender";
                    break;
                case "Nationality":
                   
                    FilterColumn = "CountryName";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    
                    break;

                case "Email":
                    FilterColumn = "Email";
            
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }



            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.

                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());



           




        }



 

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void editToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;    
            frmAddUpdatePerson frm = new frmAddUpdatePerson(PersonID);
            frm.ShowDialog();
            _RefreshPoepleData();
        }

        private void addNewPersonToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm  = new frmAddUpdatePerson();
            frm.ShowDialog();   
            _RefreshPoepleData();   
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "PersonID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPoepleData();
        }

        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete contact [" + dgvPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel ,MessageBoxIcon.Warning) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsPerson.DeletePerson((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully.","Delete Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPoepleData();
                }

                else
                    MessageBox.Show("Contact is not deleted.");

            }
        }

        private void ShowPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;    

            frmShowPersonInfo frm  = new frmShowPersonInfo(PersonID);   
            frm.ShowDialog();   

            _RefreshPoepleData();   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void callToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    
}
