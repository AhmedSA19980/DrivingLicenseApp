using DLSDATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DLSBusinessLayer
{
    public class clsTestAppointment
    {

        public enum enMode { AddNew = 0, Update=1};

        enMode _Mode = enMode.AddNew;   
        public int TestAppointmentID { get; set; }  
        public DateTime AppointmentDate { get; set; }
        public int CreatedByUserID { get; set;}
        public int LocalDrivingLicenseApplicationID { get;set; }
        public clsTestTypes.enTestType TestTypeID { get; set; }
        public float PaidFees { get;set; }
        public int RetakeTestApplicationID { set; get; }

        public  clsApplication RetakeTestAppInfo { get; set; }
        public bool IsLocked { get; set; }  



        public int TestID { get { return _GetTestID(); } }
    

     
        public clsTestAppointment() { 
        
        
            this.TestAppointmentID = -1; 
            this.AppointmentDate = DateTime.Now;    
            this.CreatedByUserID = -1; 
            this.TestTypeID = clsTestTypes.enTestType.VisionTest;   
            this.PaidFees = 0;
            this.RetakeTestApplicationID = -1;
           
            _Mode = enMode.AddNew;  
        }


        public   clsTestAppointment(
            int TestAppointmentID, DateTime AppointmentDate,
            int CreatedByUserID,int LocalDrivingLicenseApplicationID, 
            clsTestTypes.enTestType TestTypeID,
            float PaidFees , bool IsLocked , 
            int RetakeTestApplicationID)
        {


            this.TestAppointmentID = TestAppointmentID;
            this.AppointmentDate = AppointmentDate;
            this.CreatedByUserID = CreatedByUserID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.TestTypeID = TestTypeID;
            this.PaidFees = PaidFees;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestAppInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID); 
            _Mode = enMode.Update;
        }


       private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentsData.AddNewTestAppointment(this.AppointmentDate  ,this.CreatedByUserID ,this.LocalDrivingLicenseApplicationID
              ,(int)this.TestTypeID  ,this.PaidFees  , this.RetakeTestApplicationID);
            return (this.TestAppointmentID != -1);
        }


        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentsData.UpdatTestAppointment(this.TestAppointmentID , this.AppointmentDate , this.CreatedByUserID,
                this.LocalDrivingLicenseApplicationID,(int)this.TestTypeID , this.PaidFees ,this.IsLocked, this.RetakeTestApplicationID);
        }


       public static  clsTestAppointment FindTestAppointmentByID(int TestAppointmentID)
        {
           DateTime AppointmentDate = DateTime.Now;
            int CreatedByUserID = -1, TestTypeID = 1, 
                LocalDrivingLicenseApplicationID = -1 ;

            float PaidFees = 0; 
            bool IsLocked =false;
            int RetakeTestApplicationID = -1;


            bool IsFound =clsTestAppointmentsData.FindTestAppointmentByID(TestAppointmentID , ref AppointmentDate ,ref CreatedByUserID,
                ref LocalDrivingLicenseApplicationID , ref TestTypeID,ref PaidFees , ref IsLocked ,ref RetakeTestApplicationID );

            if (IsFound)
            
                return new clsTestAppointment(TestAppointmentID ,AppointmentDate ,
                    CreatedByUserID ,LocalDrivingLicenseApplicationID,
                    (clsTestTypes.enTestType)TestTypeID, PaidFees,IsLocked  , RetakeTestApplicationID);
         
            else
            
                return null;
            

        }

        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            if (clsTestAppointmentsData.GetLastTestAppointment(LocalDrivingLicenseApplicationID, (int)TestTypeID,
               ref TestAppointmentID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointment(TestAppointmentID, AppointmentDate,
             CreatedByUserID, LocalDrivingLicenseApplicationID,TestTypeID,PaidFees, IsLocked,
             RetakeTestApplicationID);
            else
                return null;

        }


        private int _GetTestID()
        {
            return clsTestsData.GetTestAppointmentID(TestAppointmentID);
        }

        public static int CheckIsActiveTestAppointmentIDTestAppointment(clsTestTypes.enTestType TestTypeID , int LDLAppID  )
        {
            return clsTestAppointmentsData.GetActiveTestAppointmentIDTestAppointment((int)TestTypeID ,LDLAppID  );
        }

        public DataTable GetApplicationTestAppointmentPerTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsTestAppointmentsData.GetApplicationTestAppointmentsPerTestType(this.LocalDrivingLicenseApplicationID,(int) TestTypeID);
        }


        public static DataTable GetApplicationTestAppointmentPerTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            return clsTestAppointmentsData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

       



        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {

                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestAppointment();

            }

            return false;
        }


        public static DataTable GetTestAppointments()
        {
           return  clsTestAppointmentsData.GetTestAppointments();   

        }


    }
}
