using DLSDATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static DLSDATA.clsApplication;

namespace DLSBusinessLayer
{
    public class clsLocalDrivingLicenseApplications : clsApplication
    {
        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        //public int ApplicationID { get; set; }

        public int LicenseClassID { get; set; }

        public clsLicenseClasses clsLicenseClassesInfo;




        public string PersonFullName
        {
            get
            {

                return base.FullName;
                // return clsPerson.Find(ApplicantPersonID).FullName; 
            }

        }




        public clsLocalDrivingLicenseApplications()
        {

            this.LocalDrivingLicenseApplicationID = -1;
         
            this.LicenseClassID = -1;

            _Mode = enMode.AddNew;
        }

        public clsLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID,

            int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus,
             DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            this.LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;    


            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.clsLicenseClassesInfo = clsLicenseClasses.Find(LicenseClassID);
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate; 
            this.ApplicationTypeID =(int) ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;   
            this.CreatedByUserID = CreatedByUserID; 


            _Mode = enMode.Update;
        }


       private bool _AddNewLocalDrivingLicenseApplications()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID,this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }



        public static clsLocalDrivingLicenseApplications FindByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            // 
            int ApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.FindLocalDrivingLicenseApplicationByID
                (LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                //now we find the base application
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLocalDrivingLicenseApplications(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID, LicenseClassID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID );
            }
            else
                return null;
            

        }

        public int IssueLicenseForTheFirtTime(string notes ,int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindDriverByPersonID(this.ApplicantPersonID);

            if (Driver == null) {

                Driver = new clsDriver();
                Driver.CreatedByUserID = CreatedByUserID;
                Driver.PersonID =this.ApplicantPersonID;
                //Driver.CreatedDate =DateTime.Now;
                if (Driver.Save())
                {
                    DriverID  = Driver.DriverID ;
                }
                else
                {
                    return -1;
                }

            }
            else
            {
                DriverID = Driver.DriverID; 
            }

            clsLicense License = new clsLicense();
            
            License.ApplicationID =this.ApplicationID ;
            License.DriverID = DriverID ;
            License.CreatedByUserID = CreatedByUserID ; 
            License.Notes = notes ;
            License.PaidFees = this.clsLicenseClassesInfo.ClassFees ;
            License.IssueDate =DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.clsLicenseClassesInfo.DefaultValidityLength);
            License.IssueReason =clsLicense.enIssueReason.FirstTime;
            License.IsActive = true;
            License.LicenseClassID =this.LicenseClassID ;

            if (License.Save())
            {
                this.SetCompleted();
                return License.LicenseID;
            }
            else
                return -1; 
        }



        private bool _UpdateLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, 
                this.LicenseClassID);

        }
   
        public clsTest GetLastTestPerTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        } 
        public bool DoesAttendTestType(clsTestTypes.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID ,(int)TestType );
        } 

        public byte TotalTrialsPerTest(clsTestTypes.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID , (int)TestType);
        }
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }


        public bool AttendedTest(clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }


        public bool Save()
        {
           
            base.Mode = (clsApplication.enMode)_Mode;
            if (!base.Save())
                return false;


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplications())
                    {

                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;

                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseApplications();

            }

            return false;
        }



        public bool DoesPassTestType(clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }


        public bool DoesPassPreviousTest(clsTestTypes.enTestType CurrentTestType)
        {

            switch (CurrentTestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    return true;

                case clsTestTypes.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.

                    return this.DoesPassTestType(clsTestTypes.enTestType.VisionTest);


                case clsTestTypes.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    return this.DoesPassTestType(clsTestTypes.enTestType.WrittenTest);

                default:
                    return false;
            }
        }


        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static DataTable GetLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();

        }

        public  bool Delete()
        {

            bool IsLocalDrivingLicenseApplicationDeleted = false;
            bool IsBaseDrivingLicenseApplicationDeleted = false;
            IsLocalDrivingLicenseApplicationDeleted =  clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!IsLocalDrivingLicenseApplicationDeleted)
                return false;

            IsBaseDrivingLicenseApplicationDeleted = base.DeleteApplication();
    
            return IsBaseDrivingLicenseApplicationDeleted;
        
        
        }



        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }


        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
         //  return clsLicenseClassesData.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID , this.LicenseClassID);  // this function is not completed 
        }

        /* public static DataTable GetApplications()
         {
             return clsApplicationsData.GetLocalApplications();

         }

         public static bool DeleteApplication(int ID)
         {
             return clsApplicationsData.DeleteApplication(ID);
         }

         */


    }
}
