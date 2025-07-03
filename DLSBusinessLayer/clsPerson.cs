using DLSDATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSBusinessLayer
{
    public class clsPerson
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;




        public int PersonID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }

        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }

        public string Email { set; get; }
        public string Gender { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }

        public clsCountry CountryInfo;

        public string ImagePath { set; get; }

        public int NationalityCountryID { set; get; }


        public clsPerson()

        {

            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Gender = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.ImagePath = "";
            this.NationalityCountryID = -1;


            _Mode = enMode.AddNew;

        }

        private clsPerson(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
        DateTime DateOfBirth, string Gender, string Address, string Phone,
                string Email, int NationalityCountryID,
                string ImagePath)

        {
            this.PersonID = ID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Email = Email;
            this.Gender = Gender;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.ImagePath = ImagePath;
            this.NationalityCountryID = NationalityCountryID;

            this.CountryInfo = clsCountry.Find(NationalityCountryID);

            _Mode = enMode.Update;

        }


        private bool _AddNewPerson()
        {
            //call DataAccess Layer 

            this.PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
              this.DateOfBirth, this.Gender, this.Address, this.Phone,
                this.Email, this.NationalityCountryID,
                this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            //call DataAccess Layer 

            return clsPersonData.UpdatPerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
              this.DateOfBirth, this.Gender, this.Address, this.Phone,
                this.Email, this.NationalityCountryID,
                this.ImagePath);

        }


        public static clsPerson Find(int PersonID)
        {

            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "", Gender = "";


            DateTime DateOfBirth = DateTime.Now;

            int NationalityCountryID = -1;

            if (clsPersonData.GetPersonInfoByID(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
              ref DateOfBirth, ref Gender, ref Address, ref Phone,
                ref Email, ref NationalityCountryID,
                ref ImagePath))

                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone,
                           Email, NationalityCountryID, ImagePath);
            else
                return null;

        }


        public static clsPerson Find(string NationalNo)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "", Gender = "";


            DateTime DateOfBirth = DateTime.Now;

            int NationalityCountryID = -1, PersonID = -1;

            if (clsPersonData.GetPersonInfoByNationalNo(NationalNo, ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
              ref DateOfBirth, ref Gender, ref Address, ref Phone,
                ref Email, ref NationalityCountryID,
                ref ImagePath))

                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone,
                           Email, NationalityCountryID, ImagePath);
            else
                return null;

        }



        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

            }




            return false;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();

        }

        public static bool DeletePerson(int ID)
        {
            return clsPersonData.DeletePerson(ID);
        }

        public static bool isPersonExist(int ID)
        {
            return clsPersonData.IsPersonExist(ID);
        }


        public static bool isPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }
        public static bool GetPersonByEmail(string Email )
        {
            return clsPersonData.GetPersonByEmail(Email);
        }


    }
}
