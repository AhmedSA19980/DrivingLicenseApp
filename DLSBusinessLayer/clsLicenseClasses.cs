using DLSDATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DLSBusinessLayer
{
    public class clsLicenseClasses
    {
        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;


        public int LicenseClassID {  get; set; }    
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }

        public byte MinimumAllowedAge {  get; set; }
        public byte DefaultValidityLength { get; set;}

        public float ClassFees { get; set; }




        clsLicenseClasses()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 18;
            this.DefaultValidityLength = 10;
            this.ClassFees = 0;
            _Mode = enMode.AddNew;

        }


        clsLicenseClasses(int LicenseClassesID, string ClassName ,string ClassDescription, byte MinimumAllowedAge , byte DefaultValidityLength ,float ClassFees)
        {
            this.LicenseClassID = LicenseClassesID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength; 
            this.ClassFees = ClassFees; 
            _Mode = enMode.Update;  

        }

        public static clsLicenseClasses Find(int LicenseClassID)
        {
            byte   MinimumAllowedAge = 0 , DefaultValidityLength = 0 ;
            string  ClassDescription = "" , ClassName="";
            float ClassFees = -1;

            bool IsFound = clsLicenseClassesData.FindLicenseClassByID(  LicenseClassID ,  ref ClassName, ref ClassDescription, ref MinimumAllowedAge
                , ref DefaultValidityLength, ref ClassFees);

            if (IsFound)
            {
                return new clsLicenseClasses(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
                return null; 


        }

        public static clsLicenseClasses Find(string ClassName)
        {
            byte MinimumAllowedAge = 18, DefaultValidityLength = 10;
            string ClassDescription = "";
            float ClassFees = 0;
            int LicenseClassID = -1; 
            bool IsFound = clsLicenseClassesData.FindLicenseClassByName(ClassName, ref LicenseClassID, ref ClassDescription, ref MinimumAllowedAge
                , ref DefaultValidityLength, ref ClassFees);

            if (IsFound)
            {
                return new clsLicenseClasses(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
                return null;


        }

        public static DataTable GetLicenseClasses()
        {
            return clsLicenseClassesData.GetLicenseCalsses();

        }

        private bool _AddNewLicenseClass()
        {
            this.LicenseClassID = clsLicenseClassesData.AddNewLicenseClass( this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge , this.DefaultValidityLength , this.ClassFees);

            return (this.LicenseClassID != -1);
        }




        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassesData.UpdateLicenseClass(this.LicenseClassID ,this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);

        }

        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {

                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;

                    }

                case enMode.Update:

                    return _UpdateLicenseClass();

            }

            return false;
        }

    }
}
