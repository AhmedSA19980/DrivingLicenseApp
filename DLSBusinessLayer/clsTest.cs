using DLSDATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DLSBusinessLayer
{
    public  class clsTest
    {

        enum enMode { AddNew =  0, Update = 1 }  
        enMode _Mode = enMode.AddNew;       
       public  int TestID { get; set; } 
        public int TestAppointmentID { get; set;}
        public bool TestResult { get; set; }

        public string Notes { get; set; }
        public int  CreatedByUserID {  get; set; }

        public clsTestAppointment TestAppointmentInfo { get; set; }


        public clsTest() { 
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult =false;
            this.Notes = "";
            this.CreatedByUserID = -1;

            _Mode = enMode.AddNew;  
        }

        public clsTest(int TestID ,int TestAppointmentID ,bool TestResult , string Notes, int CreatedByUserID )
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            this.TestAppointmentInfo = clsTestAppointment.FindTestAppointmentByID( TestAppointmentID );    
            _Mode = enMode.Update;

        }




        private bool _AddNewTest()
        {
            //call DataAccess Layer 
            this.TestID = clsTestsData.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {
            //call DataAccess Layer 

            return clsTestsData.UpdateTest( this.TestID,this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);

        }



        public static clsTest FindLastTestPerPersonAndLicenseClass(
            int PersonID, int LicenseClassID, clsTestTypes.enTestType TestTypeID
            )
        {

            int TestID = -1; 
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedUserByID = -1;

            if (clsTestsData.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID
                , LicenseClassID, (int)TestTypeID, ref TestID, ref TestAppointmentID, ref TestResult,
                 ref Notes, ref CreatedUserByID))
            
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedUserByID);

            else
                return null;
        }


        public static clsTest Find(int TestID)
        {
           int TestAppointmentID = -1   , CreatedByUserID  = -1;
            bool TestResult = true; 
            string Notes = "";


            bool IsFound = clsTestsData.GetTestById(TestID , ref TestAppointmentID , ref TestResult ,ref Notes, ref CreatedByUserID); 
            
            if (IsFound)
            {
                return new clsTest(TestID , TestAppointmentID , TestResult ,Notes, CreatedByUserID);  

            }else
                return null;    
                
        }



        public static bool IsTestExist(int ID)
        {
            return clsTestsData.IsTestExist(ID);
        }




        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTest();

            }

            return false;
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestsData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }


    }


}
