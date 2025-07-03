using DLSBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DLSDATA
{
    public class clsApplication
    {

        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5,
            NewInternationalLicense = 6, RetakeTest = 7
        };

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }

        public DateTime ApplicationDate { get; set; }

        public int ApplicationTypeID { get; set; }

        public enApplicationStatus ApplicationStatus { get; set; }

        public DateTime LastStatusDate { get; set; }

        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public clsApplicationType ApplicationTypeInfo;

        public clsUser CreatedUserInfo { get; set; }

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };
      
        public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:

                        return "New";

                    case enApplicationStatus.Cancelled:
                        return "Cancelled";

                    case enApplicationStatus.Completed:
                        return "Compeleted";

                    default:
                        return "Unknown";

                }

            }
        }
        
        public string FullName{   get{ return clsPerson.Find(ApplicantPersonID).FullName; } }



       public  clsApplication() {

            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now; 
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;  
        }



       public  clsApplication(int ApplicationID , int ApplicantPersonID ,DateTime ApplicationDate  ,int ApplicationTypeID ,
           enApplicationStatus ApplicationStatus ,
             DateTime LastStatusDate , float PaidFees , int CreatedByUserID)
        {

            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID =ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedUserInfo = clsUser.FindUserById(CreatedByUserID);
           

            Mode = enMode.Update;
        }


        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationsData.AddNewApplication( this.ApplicantPersonID, this.ApplicationDate
                , this.ApplicationTypeID,(byte) this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }




        private bool _UpdateApplication()
        {
            return clsApplicationsData.UpdateApplication(this.ApplicationID , this.ApplicantPersonID , this.ApplicationDate
                ,this.ApplicationTypeID,(byte) this.ApplicationStatus , this.LastStatusDate ,this.PaidFees , this.CreatedByUserID);

        }
        



        public static int GetActiveApplicationIDForLicenseClass(int PersonID , clsApplication.enApplicationType ApplicationTypeID , int LicenseClassID)
        {
            return clsApplicationsData.GetActiveApplicationIDForLicenseClass(PersonID , (int)ApplicationTypeID , LicenseClassID);
        }
        public static clsApplication FindBaseApplication(int ApplicationID)
        {

     
            int ApplicantPersonID = -1 , ApplicationTypeID = -1 , CreateByUserID= -1;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate =DateTime.Now;
            float PaidFees = 0;
            byte ApplicationStatus = 1;

            bool isFound = clsApplicationsData.FindApplicationByID(
                       ApplicationID, ref ApplicantPersonID,
                        ref ApplicationDate, ref ApplicationTypeID,
                        ref ApplicationStatus,ref LastStatusDate,
                        ref PaidFees, ref CreateByUserID);

            if (isFound)

                return new clsApplication(ApplicationID,  ApplicantPersonID,
                    ApplicationDate,ApplicationTypeID,
                    (enApplicationStatus) ApplicationStatus,
                    LastStatusDate,
                 PaidFees, CreateByUserID);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;

                    }

                case enMode.Update:

                    return _UpdateApplication();

            }

            return false;
        }


        public static DataTable GetApplications()
        {
            return clsApplicationsData.GetLocalApplications();

        }


        public bool SetCompleted()
        {

            return clsApplicationsData.UpdateStatus(this.ApplicationID, 3);


        }
        public  bool DeleteApplication()
        {
            return clsApplicationsData.DeleteApplication(this.ApplicationID);
        }


       public  bool CancelApplication()
        {
            return clsApplicationsData.UpdateStatus(this.ApplicationID , 2);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationsData.IsApplicationExist(ApplicationID);
        }


        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationsData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }
        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }


        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationsData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }

        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }


    }
}
