using DLSBusinessLayer;
using DLSDATA;
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

namespace DrivingLicenseSystem.DrivingLicense.NewLocalDrivingLicense
{
    public partial class frmAddUpdateDrivingLicense : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _LocalDrivingLicenseApplicationID  = -1;
        private int  _OnSelectedPersonID = -1 ; 

     
        //clsUser _User;
        clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplication;
        //clsApplication _Application;    

        public frmAddUpdateDrivingLicense()
        {
            InitializeComponent();
           
            _Mode = enMode.AddNew;  
        }

        public frmAddUpdateDrivingLicense(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }


      


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
        private void _FillInCountriesWithComboBox1()
        {
           
            DataTable Classes = clsLicenseClasses.GetLicenseClasses();

            foreach (DataRow Row in Classes.Rows)
            {
              cbLicenseClasses.Items.Add(Row["ClassName"]).ToString();
            }
        }


       private void _GetCurrentTime()
        {
            DateTime dateTime = DateTime.Today;
            lblAppDate.Text = dateTime.ToString("dd/MM/yyyy");
        }
        private void _DefaultLicenseCalssData()
        {
            _FillInCountriesWithComboBox1();
         

            if(_Mode == enMode.AddNew) {
                lblTitle.Text = "New Local Driving License Application";
                this.Text  = lblTitle.Text; 
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplications();
                ctrlPersonCardWithFilter1.FilterFocus();
                cbLicenseClasses.SelectedIndex = 2;
                tpAppInfo.Enabled = false;

                _GetCurrentTime();
        
                lblClassFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                lblCreatedUser.Text = clsGlobal.CurrentUser.UserName;

            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
                tpAppInfo.Enabled = true;
                btnSave.Enabled = true;    
            
            }

           


        }
        private void _LoadData()
        {

            //cbLicenseClasses.SelectedIndex = 1;
            ctrlPersonCardWithFilter1.EnableFilter(false);
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);
         

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            
            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            cbLicenseClasses.SelectedIndex = cbLicenseClasses.FindString(clsLicenseClasses.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblCreatedUser.Text = clsUser.FindUserById(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;
            lblAppDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            lblClassFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
          
        
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpAppInfo.Enabled = true;
                tcDrivingLicense.SelectedTab = tcDrivingLicense.TabPages["tpAppInfo"];
                return;
            }

            //incase of add new mode.
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {

                btnSave.Enabled = true;
                tpAppInfo.Enabled = true;
                tcDrivingLicense.SelectedTab = tcDrivingLicense.TabPages["tpAppInfo"];
                ctrlPersonCardWithFilter1.FilterFocus();

            }

           else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();

            }

        }

        private void frmAddUpdateDrivingLicense_Load(object sender, EventArgs e)
        {
            
        
            _DefaultLicenseCalssData();
            if(_Mode == enMode.Update)  
                _LoadData();

          
        }
      
        private void btnSave_Click(object sender, EventArgs e)
        {
            //_DefaultLicenseCalssData();
            int LicenseClassID = clsLicenseClasses.Find(cbLicenseClasses.Text).LicenseClassID;
            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(ctrlPersonCardWithFilter1.PersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID) ;


            
            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClasses.Focus();
                return;
            }

            if (clsLicense.IsLicenseExistByPersonID(ctrlPersonCardWithFilter1.PersonID, LicenseClassID))
            {
                MessageBox.Show("Person already have a license with same applied class ,Choose another One", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };

            

            _LocalDrivingLicenseApplication.ApplicantPersonID =ctrlPersonCardWithFilter1.PersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID =(byte)clsApplication.enApplicationType.NewDrivingLicense;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblClassFees.Text) ;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;

            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;
            _LocalDrivingLicenseApplication.CreatedByUserID =clsGlobal.CurrentUser.UserID ;



          

            if ( _LocalDrivingLicenseApplication.Save()) { 

                lblAppID.Text= _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Applications";
                MessageBox.Show("Data Is Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _OnSelectedPersonID = obj;   
        }

        private void ctrlPersonCardWithFilter1_DataBack(object sender, int PersonID)
        {
            _OnSelectedPersonID = PersonID;
            ctrlPersonCardWithFilter1.LoadPersonInfo(PersonID);

        }



        /*  private void cbLicenseClasses_SelectedIndexChanged(object sender, EventArgs e)
          {

              _LoadLicenseClassesData();
          }*/
    }
}
