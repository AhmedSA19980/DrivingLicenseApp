using DLSDATA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSBusinessLayer
{
    public class clsDetainLicense
    {

        public enum enMode { AddNew = 1 , Update = 2 }
       public enMode Mode = enMode.AddNew;   

        public int DetainID { get; set; }   
        public int LicenseID { get; set; }

        public int ReleasedByUserID { get; set; }   
        public int CreatedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }   
        public bool IsReleased { get; set; }


        public float FineFees { get; set; }
        public DateTime DetainDate { get; set; }
        public DateTime ReleasedDate { get; set; }


        public clsUser RelaeasedByUserInfo { get; set; }    
        public clsUser CreatedByUserInfo { get; set; }    


        public clsDetainLicense() { 
            this.DetainID = -1;
            this.LicenseID = -1;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.FineFees = 0;
            this.DetainDate = DateTime.Now;
            this.ReleasedDate= DateTime.MaxValue;    
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1; 
        
            Mode = enMode.AddNew;
        }


        public clsDetainLicense(int DetainID , int LicenseID ,int ReleasedByUserID ,int CreatedByUserID
            ,int ReleaseApplicationID , bool IsReleased ,float FineFees ,DateTime DetainDate , DateTime ReleasedDate)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.ReleasedByUserID = ReleasedByUserID;
            this.CreatedByUserID = CreatedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.IsReleased = IsReleased;
            this.FineFees = FineFees;
            this.DetainDate = DetainDate;
            this.ReleasedDate = ReleasedDate;
            this.CreatedByUserInfo = clsUser.FindUserById(CreatedByUserID);
            this.RelaeasedByUserInfo = clsUser.FindUserById(ReleasedByUserID);
            Mode = enMode.AddNew;
        }



        private bool _AddDetainLicense()
        {
            this.DetainID = clsDetainLicenseData.AddDetainedLicense(this.LicenseID, this.CreatedByUserID, this.IsReleased
                , this.FineFees ,this.DetainDate);

            return (this.DetainID != -1);
        }



        private bool _UpdateDriver()
        {

            return clsDetainLicenseData
               .UpdateDetainedLicense(this.DetainID,this.LicenseID, this.CreatedByUserID, this.IsReleased
                , this.FineFees, this.DetainDate);
        }


        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainLicenseData.IsLicenseDetained (LicenseID);  
        }


        public static clsDetainLicense Find(int DetainID)
        {
            int LicenseID = -1, ReleasedByUserID = -1, CreatedByUserID = -1, ReleaseApplicationID = -1;
            bool IsReleased = false;
            float FineFees = 0;
            DateTime DetainDate = DateTime.Now, 
            ReleasedDate = DateTime.Now;



            if (clsDetainLicenseData.FindDetainLicensebyID(DetainID  ,ref LicenseID , ref ReleasedByUserID , ref CreatedByUserID ,ref ReleaseApplicationID
                , ref IsReleased  ,ref FineFees , ref DetainDate  ,ref ReleasedDate))

                return new clsDetainLicense(DetainID, LicenseID, ReleasedByUserID, CreatedByUserID, ReleaseApplicationID
                , IsReleased, FineFees, DetainDate, ReleasedDate);

            else
                return null;
        }


        public static clsDetainLicense FindDetainedByLicenseID(int LicenseID)
        {
            int DetainID = -1, ReleasedByUserID = -1, CreatedByUserID = -1, ReleaseApplicationID = -1;
            bool IsReleased = false;
            float FineFees = 0;
            DateTime DetainDate = DateTime.Now,
            ReleasedDate = DateTime.Now;



            if (clsDetainLicenseData.GetDetainInfoByLicenseID( LicenseID ,ref DetainID, ref ReleasedByUserID, ref CreatedByUserID, ref ReleaseApplicationID
                , ref IsReleased, ref FineFees, ref DetainDate, ref ReleasedDate))

                return new clsDetainLicense(DetainID, LicenseID, ReleasedByUserID, CreatedByUserID, ReleaseApplicationID
                , IsReleased, FineFees, DetainDate, ReleasedDate);

            else
                return null;
        }

        public bool ReleasedDetained(int  LicenseID , int ReleasedByUserID , int ReleaseApplication)
        {
           return  clsDetainLicenseData.ReleasedDetainedLicense(LicenseID , ReleasedByUserID, ReleaseApplication); 
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplication)
        {
            return clsDetainLicenseData.ReleasedDetainedLicense(this.LicenseID ,  ReleasedByUserID,  ReleaseApplication);
        }
 

        public static DataTable GetDetainedLicenses()
        {
            return clsDetainLicenseData.GetAllDetainedLicenses();
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddDetainLicense())
                    {

                        Mode = enMode.Update;
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


    }
}
