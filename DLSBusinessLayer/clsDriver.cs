using DLSDATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSBusinessLayer
{
    public class clsDriver
    {
        private  enum enMode { AddNew=0 , Update =1};
        enMode _Mode = enMode.AddNew;    
        public int DriverID { get; set; }

        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
       
        public DateTime  CreatedDate {  get; set; }

        public clsPerson PersonInfo { get; set; }   

        public clsDriver()
        {

            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
         
            _Mode = enMode.AddNew;
        }


        public clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {

            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = DateTime.Now;
            this.PersonInfo = clsPerson.Find(PersonID);
            _Mode = enMode.Update;
        }



        public static clsDriver Find(int DriverID)
        {
            DateTime CreatedDate  = DateTime.Now;
            int PersonID = -1   , CreatedByUserID = -1;

            

            if (clsDriverData.FindDriverById(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }

        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return (this.DriverID != -1);
        }

        private bool _UpdateDriver()
        {
          
            return clsDriverData.UpdateDriver(this.DriverID ,this.PersonID, this.CreatedByUserID, this.CreatedDate) ;
        }


        public static clsDriver FindDriverByPersonID(int PersonID)
        {
            DateTime CreatedDate = DateTime.Now;
            int DriverID = -1, CreatedByUserID = -1;

            if (clsDriverData.FindDriverByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }


        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {

                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;

                    }

                case enMode.Update:

                    return _UpdateDriver();

            }

            return false;
        }


        public static DataTable GetInternationalDriverLicense(int DriverID)
        {
            return clsInternationalLicense.GetDriverInternationalLicenses(DriverID);
        }
        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicense.GetDriverLicenses(DriverID);
        }
        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetDriverLicenses();
        }
    }
}
