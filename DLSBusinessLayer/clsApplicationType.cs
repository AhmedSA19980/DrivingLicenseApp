using DLSDATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSBusinessLayer
{
    public class clsApplicationType
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees { get; set; }

 

        public clsApplicationType()
        {

            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle = "";

            this.ApplicationFees = 0;


            Mode = enMode.AddNew;
        }

        public clsApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {

            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;

            this.ApplicationFees = ApplicationFees;


               Mode = enMode.Update;
        }

        private bool _AddNewApplicationType()
        {
            //call DataAccess Layer 

            this.ApplicationTypeID = clsApplicationTypesData.AddNewApplicationType(this.ApplicationTypeTitle, this.ApplicationFees);


            return (this.ApplicationTypeID != -1);
        }

        public bool UpdateApplicationType()
        {
            return clsApplicationTypesData.UpdateApplicationType(this.ApplicationTypeID,
                this.ApplicationTypeTitle, this.ApplicationFees);
        }


        public static clsApplicationType Find(int ApplicationTypeID)
        {

            string ApplicationTypeTitle = "";
            float ApplicationFees = 0;

            bool isFound = clsApplicationTypesData.FindApplicationTypeById(
                   (int)ApplicationTypeID,
                ref ApplicationTypeTitle, ref ApplicationFees);

            if (isFound)
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle,
                    ApplicationFees);
            else
                return null;


        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return UpdateApplicationType();

            }

            return false;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesData.GetApplicationTypes();

        }
    }
}
