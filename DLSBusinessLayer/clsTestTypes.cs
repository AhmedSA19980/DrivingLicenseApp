using DLSDATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSBusinessLayer
{
    public  class clsTestTypes
    {

        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestTypes.enTestType ID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }




        public bool _UpdateTestType()
        {
            return clsTestTypesData.UpdateTestType((int) this.ID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }


        public clsTestTypes()
        {

            this.ID = clsTestTypes.enTestType.VisionTest;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";

            this.TestTypeFees = 0;


            _Mode = enMode.AddNew;
        }

        public clsTestTypes(clsTestTypes.enTestType TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {

            this.ID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;

            this.TestTypeFees = TestTypeFees;


              _Mode = enMode.Update;
        }


        private bool _AddNewTestType()
        {
            //call DataAccess Layer 

            this.ID = (clsTestTypes.enTestType) clsTestTypesData.AddNewTestType(this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);

            return (this.TestTypeTitle != "");
        }


        public static clsTestTypes Find(clsTestTypes.enTestType TestTypeID)
        {


            string TestTypeTitle = "", TestTypeDescription = "";
            float TestTypeFees = 0;

            bool isFound = clsTestTypesData.FindTestTypeById((int)TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees);

            if (isFound)
                return new clsTestTypes(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            else
                return null;


        }



        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetTestTypes();

        }


        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {

                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestType();

            }

            return false;
        }

    }
}
