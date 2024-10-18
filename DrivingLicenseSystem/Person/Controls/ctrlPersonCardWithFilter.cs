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

namespace DrivingLicenseSystem.Person.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {

        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;


        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelect(int personId)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
                handler(personId);
        }



        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }


        public int PersonID
        {

            get { return ctrlPersonCard1.PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }


        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }


   
 
        public void EnableFilter(bool enableFilterUser)
        {
            gbFilters.Enabled = enableFilterUser;
        }


      

        public void LoadPersonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            FindPerson();
        }


       
        private void FindPerson()
        {




             switch (cbFilterBy.Text)
             {
                 case "Person ID":
                    if (int.TryParse(txtFilterValue.Text, out int personId))
                    {
                        ctrlPersonCard1.LoadPersonInfo(personId);
                    }else
                    {
                        ctrlPersonCard1.LoadPersonInfo(-1);
                        return;
                    }
                   
                     

                     break;
                 case "National No.":
                        ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                     break;

                 default:
                     break;
             }

        

            if (OnPersonSelected != null)
            {
                // Raise the event with a parameter
                OnPersonSelected(ctrlPersonCard1.PersonID);

            }

        }


        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }



  

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren() )
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            FindPerson();

        }


        private void cbFilterByUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();

        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();

        }

        private void DataBackEvent(object sender, int PersonID)
        {

            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);  
        }



        private void txtFilterBy_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
            }
            else
            {
               
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

     
 
    }
}

