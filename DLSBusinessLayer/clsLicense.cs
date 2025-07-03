using DLSDATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DLSBusinessLayer
{
    public class clsLicense
    {

        private enum enMode { AddNew  =1 , Update  =2};

        enMode Mode = enMode.AddNew;

        public enum enIssueReason { FirstTime  =1  ,Renew = 2 , Lost =3 ,DamagedReplacement =4 }
        public int LicenseID {  get; set; }    
        public  int ApplicationID {  get; set; }
        public int DriverID { get; set; }

        public int LicenseClassID {  get; set; }
        public int CreatedByUserID { get; set; }


        public DateTime IssueDate {  get; set; }

        public DateTime ExpirationDate {  get; set; }

        public string Notes { get; set; }
        public float PaidFees {  get; set; }

        public bool IsActive {  get; set; }

        public enIssueReason IssueReason {  get; set; }

        public string IssueReasonText { get { return GetIssueReason(this.IssueReason); } }
      public clsDriver DriverInfo { get; set; }    
      public  clsLicenseClasses LicenseClassesInfo { get; set; }
        public clsApplication ApplicationInfo { get; set; }
        public clsDetainLicense DetainedInfo { set; get; }
        public bool IsDetained
        {
            get { return clsDetainLicense.IsLicenseDetained(this.LicenseID); }
        }

        string  GetIssueReason(enIssueReason IssueReason)
        {
            switch (IssueReason) { 
            case enIssueReason.FirstTime:
                    return "First Time";
                    
            case enIssueReason.Renew:
                    return "Renew License";
          
            case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
            case enIssueReason.Lost:
                    return "Replacement for Lost";
                default:
                    return "Frist Time"
                        ;

            }

        }
      public   clsLicense() { 
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1; 
            this.LicenseClassID = -1; 
            this.CreatedByUserID = -1;
            this.IssueDate =DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive =true;
            this.IssueReason = enIssueReason.FirstTime;

            Mode = enMode.AddNew;
        }


        clsLicense(int LicenseID ,int ApplicationID , int DriverID , int LicenseClassID ,int CreatedByUserID
            ,DateTime IssueDate  ,DateTime ExpirationDate , string Notes , float PaidFees, bool IsActive, enIssueReason IssueReason
            )
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.CreatedByUserID = CreatedByUserID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.DriverInfo = clsDriver.Find(DriverID);
            this.LicenseClassesInfo = clsLicenseClasses.Find(LicenseClassID);
            this.ApplicationInfo = clsApplication.FindBaseApplication(ApplicationID);
            this.DetainedInfo = clsDetainLicense.FindDetainedByLicenseID(LicenseID);    
            Mode = enMode.Update;
        }


        public static bool IsLicenseValidForIntenrationalLicenseHasFound(int LicenseID)
        {
            return clsLicenseData.IsLicenseValidForIntenrationalLicense(LicenseID);
        }

        private bool  _AddNewLicense()
        {

            this.LicenseID = clsLicenseData.AddNewLicnese(this.ApplicationID, this.DriverID, this.LicenseClassID,
            this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,(byte) this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID !=-1);
        }

        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClassID, this.CreatedByUserID,
            this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,(byte) this.IssueReason);
        }


        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            string Note = "";
            DateTime IssueDate = DateTime.Now , ExpirationDate = DateTime.Now;
            float PaidFees = 0;
            byte  IssueReason = 0;
            bool IsActive = true;
            if (clsLicenseData.FindLicenseById(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref CreatedByUserID
                , ref IssueDate, ref ExpirationDate, ref Note, ref PaidFees, ref IsActive, ref IssueReason))
            

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, CreatedByUserID,
                           IssueDate, ExpirationDate, Note, PaidFees, IsActive, (enIssueReason)IssueReason);
            
            else
                return null;

        }

        public static bool IsLicenseExistByPersonID(int PersonID , int LicensseClassID) {
            return (clsLicense.GetActiveLicenseIDByPersonID(PersonID, LicensseClassID) != -1); 
        
        }
        public bool IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);    
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID , int LicenseClassID) { 
        
            return clsLicenseData.GetActiveLicenseByPersonID(PersonID, LicenseClassID); 
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);   
        }

        public  bool DeActivateCurrentLicense()
        {
            return clsLicenseData.DeactivateCurrentLicense(this.LicenseID);
        }






        public int DetainLicense(float FineFees , int CreatedByUserID)
        {
            
                clsDetainLicense DetainedLicense = new clsDetainLicense();
                DetainedLicense.LicenseID = LicenseID;
                DetainedLicense.CreatedByUserID = CreatedByUserID;
                DetainedLicense.FineFees = FineFees;
                DetainedLicense.DetainDate = DateTime.Now;  
             
                if (!DetainedLicense.Save())
                {
                    return -1;
                }


            

            return DetainedLicense.DetainID;
        }    



        public clsLicense RenewLicense(string Note  , int CreatedByUserID)
        {
            int ApplicationID = -1;
            clsApplication PreviousApplication = clsApplication.FindBaseApplication(this.ApplicationID);
            if (PreviousApplication != null) {
               
                clsApplication Application = new clsApplication();
                Application.ApplicantPersonID = ApplicationInfo.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
                Application.ApplicationStatus = ApplicationInfo.ApplicationStatus;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees;
                Application.CreatedByUserID = CreatedByUserID;
              

                if (!Application.Save())
                {
                    return null;
                }

                ApplicationID = Application.ApplicationID;
            }else
                return null;

            int LicenseID = -1;
            clsLicense PreviousLicense = clsLicense.Find(this.LicenseID);
            if (PreviousLicense == null)
            {
                return null;

            }
            else
            {
                clsLicense License = new clsLicense();

                License.ApplicationID = ApplicationID;
                License.DriverID = this.DriverID;
                License.LicenseClassID = this.LicenseClassID;
                License.IssueDate = DateTime.Now;
                License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassesInfo.DefaultValidityLength);
                License.PaidFees = this.LicenseClassesInfo.ClassFees;
                License.IsActive = true;
                License.IssueReason = clsLicense.enIssueReason.Renew;
                License.Notes = Note;
                License.CreatedByUserID = CreatedByUserID;

                if (!License.Save())
                {
                    return null;
                }

                LicenseID = License.LicenseID;

                PreviousLicense.DeActivateCurrentLicense();

                return License;
            }
         
        }


        public bool ReleaseDetainedLicense(int ReleasedByUserID)
        {
            clsApplication Application = new clsApplication();
            Application.ApplicantPersonID = this.ApplicationInfo.ApplicantPersonID ;
            Application.ApplicationDate =DateTime.Now ;
            Application.ApplicationTypeID =(int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense  ;
            Application.ApplicationStatus =this.ApplicationInfo.ApplicationStatus ;
            Application.LastStatusDate= this.ApplicationInfo.LastStatusDate;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            Application.CreatedByUserID =this.CreatedByUserID ; 

            if(!Application.Save())
            {
                return false;
            }


            clsDetainLicense DetainedLicense =  clsDetainLicense.FindDetainedByLicenseID(this.LicenseID);
            if (DetainedLicense != null)
            {
                DetainedLicense.ReleasedDetained(this.LicenseID , ReleasedByUserID,Application.ApplicationID  );              
            }

           return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID , Application.ApplicationID);
          
            
        }
        public static  clsDetainLicense GetDetainLicense(int LicenseID)
        {
            return clsDetainLicense.FindDetainedByLicenseID(LicenseID);
        }
        public clsLicense ReplaceLicense(enIssueReason IssueReason, int CreatedByUserID)
        {
            int ApplicationID = -1;
            clsApplication PreviousApplication = clsApplication.FindBaseApplication(this.ApplicationID);
            if (PreviousApplication != null)
            {

                clsApplication Application = new clsApplication();
                Application.ApplicantPersonID = ApplicationInfo.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = IssueReason == enIssueReason.DamagedReplacement ?  
                    clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).ApplicationTypeID :
                    clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).ApplicationTypeID;
                Application.ApplicationStatus = ApplicationInfo.ApplicationStatus;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.Find(Application.ApplicationTypeID).ApplicationFees ;
                Application.CreatedByUserID = CreatedByUserID;


                if (!Application.Save())
                {
                    return null;
                }

                ApplicationID = Application.ApplicationID;
            }
            else
                return null;

            int NewLicenseID = -1;
            clsLicense PreviousLicense = clsLicense.Find(this.LicenseID);
            if (PreviousLicense == null)
            {
                return null;

            }
            else
            {
                clsLicense NewLicense = new clsLicense();

                NewLicense.ApplicationID = ApplicationID;
                NewLicense.DriverID = this.DriverID;
                NewLicense.LicenseClassID = this.LicenseClassID;
                NewLicense.IssueDate = DateTime.Now;
                NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassesInfo.DefaultValidityLength);
                NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.
                NewLicense.IsActive = true;
                NewLicense.IssueReason = IssueReason;
                NewLicense.Notes = this.Notes;
                NewLicense.CreatedByUserID = CreatedByUserID;

                if (!NewLicense.Save())
                {
                    return null;
                }

                NewLicenseID = NewLicense.LicenseID;

                PreviousLicense.DeActivateCurrentLicense();

                return NewLicense;
            }

        }



        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;

                    }

                case enMode.Update:

                    return _UpdateLicense();

            }

            return false;
        }

    }
}
