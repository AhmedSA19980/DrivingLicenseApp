using DLSBusinessLayer;
using DrivingLicenseSystem.ClassGlobal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Person
{
    public partial class frmAddUpdatePerson : Form
    {
       
        clsPerson _Person;
        int _PersonID = -1;


        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;


        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;


        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();


            _Mode = enMode.Update;
            _PersonID = PersonID;

        }




        private void _FillInCountriesWithComboBox1()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow Row in dtCountries.Rows)
            {
                cbCountries.Items.Add(Row["CountryName"]);
            }
        }

        private void _ResetToDefaultValues()
        {
            _FillInCountriesWithComboBox1();
            cbCountries.SelectedIndex = 0;

            if (_Mode == enMode.AddNew)
            {

                lbTitleMode.Text = "Add New Person";
                _Person = new clsPerson();


            }
            else
            {
                lbTitleMode.Text = "Update Person";
            }

            llRemoveImage.Visible = (pbPersonalImage.ImageLocation != null);

            if (rbMale.Checked)
            {
                _Person.Gender = "Male";
            }

            if (rbFemale.Checked)
            {
                _Person.Gender = "Female";
            }

            txtNationalNo.Text = "";

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            rbMale.Checked = true;
            

        }
        //  int PersonID = int.Parse(tx);
        private  void _LoadData()
        {

            //_Country = clsCountries.FindBaseApplication(_CountryID);
            _Person = clsPerson.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            //  lbTitleMode.Text = "Update Person ";
            lbPersonID.Text = _PersonID.ToString();
            txtNationalNo.Text = _Person.NationalNo;
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;

            if (_Person.Gender == "Male")
            {

                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }

            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;
            dateTimePicker1.Value = _Person.DateOfBirth;

            cbCountries.SelectedIndex = cbCountries.FindString(_Person.CountryInfo.CountryName);

            if (_Person.ImagePath != "")
            {
                pbPersonalImage.ImageLocation = _Person.ImagePath;


            }

            //hide/show the remove linke incase there is no image for the person.
            llRemoveImage.Visible = (_Person.ImagePath != "");


        }



        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetToDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();

        }






        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (!_HandlePersonImage()) return;

            int CountryID = clsCountry.Find(cbCountries.Text).CountryID;

            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.DateOfBirth = dateTimePicker1.Value;
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();

            _Person.Address = txtAddress.Text.Trim();
            _Person.NationalityCountryID = CountryID;

            if (rbMale.Checked == true)
            {
                _Person.Gender = "Male";
            }

            if (rbFemale.Checked == true)
            {
                _Person.Gender = "Female";
            }

            if (pbPersonalImage.ImageLocation != null)
            {
                _Person.ImagePath = pbPersonalImage.ImageLocation;
            }else
            {
                _Person.ImagePath = "";
            }

            if (_Person.Save())
            {

                lbPersonID.Text = _Person.PersonID.ToString();

                _Mode = enMode.Update;
                lbTitleMode.Text = "Update Person";

                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Person.PersonID);

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else

                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

          //  frmManagePeople list = new frmManagePeople();
           // list._RefreshPoepleData();

        }

      

   



 
        // private string ExistingImage;


        private bool _HandlePersonImage()
        {
            if (_Person.ImagePath != pbPersonalImage.ImageLocation)
            {
                if (!string.IsNullOrEmpty(_Person.ImagePath) && File.Exists(_Person.ImagePath))
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);


                    }
                    catch (IOException) {
                        // We could not delete the file.
                        //log it later
                    }
                }
            
                    if (pbPersonalImage.ImageLocation != null)
                    {
                    string SoruceFileImage = pbPersonalImage.ImageLocation.ToString();
                    if (clsUtil.CopyImageToProjectImagesFolder(ref SoruceFileImage))
                    {
                        pbPersonalImage.ImageLocation = SoruceFileImage;
                        return true;
                    }

                }
                else
                {
                    MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }


            return true;

        }
        private void llopenFileDialog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                string selectedFilePath = openFileDialog1.FileName;

                
               
                pbPersonalImage.Load(selectedFilePath);

                llRemoveImage.Visible = true;
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked == true)
            {
                pbPersonalImage.ImageLocation = "D:/Person_Man_2.png";

            };
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked == true)
            {
                pbPersonalImage.ImageLocation = "D:/user_female.png";

            };
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonalImage.ImageLocation = null;

            if (rbMale.Checked)
            {

                pbPersonalImage.ImageLocation = "D:/Person_Man_2.png";

            }

            if (rbFemale.Checked)
            {
                pbPersonalImage.ImageLocation = "D:/user_female.png";
            }


            llRemoveImage.Visible = false;

        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string email = txtEmail.Text;
              
            bool isValid = clsValidations.IsValidEmail(email);
            if (!isValid)
            {
                e.Cancel = true;    
                errorProvider1.SetError(txtEmail, "Please , insert A valid Email !");
            }
            else if (clsPerson.GetPersonByEmail(email) &&  _Mode == enMode.AddNew)
            {
                errorProvider1.SetError(txtEmail, "An Email is Exist , Choose anoter one !");
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            string NationalNo = txtNationalNo.Text;

            //DataTable dt = clsPeople.GetAllPeople();

            if (string.IsNullOrEmpty(NationalNo.Trim()))
            {
                e.Cancel = true;    
                errorProvider1.SetError(txtNationalNo, "This Field cannot be empty !");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            if (clsPerson.isPersonExist(txtNationalNo.Text) && _Mode == enMode.AddNew)
            {
                e.Cancel=true;  
                errorProvider1.SetError(txtNationalNo, "NationalNo is already taken by another person, type another national no");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void txtValidationEmptyTextBo_Validating(object sender, CancelEventArgs e)
        {
            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }
        }

     
    }
}
