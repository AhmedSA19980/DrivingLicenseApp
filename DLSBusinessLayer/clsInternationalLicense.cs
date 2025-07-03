using DLSDATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using static DLSDATA.clsApplication;

namespace DLSBusinessLayer
{
    public  class clsInternationalLicense:clsApplication
    {

        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
  

        public int DriverID { get; set; }

        public int IssuedUsingLocalLicenseID {  get; set; } 
        public DateTime IssueDate { get; set; }
       public DateTime ExpirationDate { get; set; } 
        public bool IsActive { get; set; }  
     

        public clsLicenseClasses LicenseClassInfo;

        public clsDriver DriverInfo { get; set; }

        public clsInternationalLicense()
        {

            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            this.InternationalLicenseID = -1;   
            this.DriverID = -1; 
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate =DateTime.Now;
            this.IsActive = true;



            _Mode = enMode.AddNew;  

        }


        public clsInternationalLicense(int InternationalLicenseID ,int ApplicationID ,int ApplicantPersonID, int DriverID , int IssuedUsingLocalLicenseID
            
            ,DateTime IssueDate,DateTime ExpirationDate ,DateTime LastStatusDate, DateTime ApplicationDate,enApplicationStatus ApplicationStatus,
            bool IsActive  ,int CreatedByUserID ,float PaidFees)
        {

            // find base application

            this.ApplicationID = ApplicationID;

            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
         

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            this.DriverInfo = clsDriver.Find(DriverID);




            _Mode = enMode.AddNew;

        }

        public static int GetActiveInternationalLicenseByDriverId(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicense(DriverID);
        }

        public static int GetPersonInfo(int DriverID)
        {
            return clsInternationalLicenseData.GetPersonInfo(DriverID);
        }


        private bool _AddNewInternationalLicense()
        {

            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
            this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            return (this.InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(this.InternationalLicenseID ,this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
            this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }


        public static clsInternationalLicense Find (int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID=  -1, IssuedUsingLocalLicenseID = -1 ,CreatedByUserID  =-1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;

 
            if (clsInternationalLicenseData.FindInternationalLicenseByID(InternationalLicenseID, ref ApplicationID, ref DriverID
                , ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {


                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsInternationalLicense(InternationalLicenseID, ApplicationID,Application.ApplicantPersonID, DriverID
                , IssuedUsingLocalLicenseID, IssueDate, ExpirationDate,Application.LastStatusDate,Application.ApplicationDate,
               (enApplicationStatus) Application.ApplicationStatus, IsActive, CreatedByUserID,Application.PaidFees);
            
            }

            else
                return null;
            
        }

        public static DataTable GetInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalDrivingLicenes();    
        }
        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }
        public bool Save()
        {
           base.Mode = (clsApplication.enMode)_Mode;
           if(!base.Save())
                return false;
    

            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {

                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;

                    }

                case enMode.Update:

                    return _UpdateInternationalLicense();

            }

            return false;
        }






    }
}
