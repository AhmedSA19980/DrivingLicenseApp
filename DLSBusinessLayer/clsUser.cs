using DLSDATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSBusinessLayer
{
    public  class clsUser
    {
        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsPerson PersonInfo;



        public clsUser()
        {

            this.UserID = -1;
        
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;
            _Mode = enMode.AddNew;
        }


        public clsUser(int UserId, int PersonID, string UserName, string Password, bool IsActive)
        {

            this.UserID = UserId;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            _Mode = enMode.Update;
        }


        //check the functions because it's logically not correct
        public clsUser Find(int UserID)
        {
            bool isFound, IsActive = false;
            string Password = "", UserName = "";
            int PersonID = -1;

            isFound = clsUserData.FindUserById(UserID ,ref PersonID , ref UserName , ref Password , ref IsActive);

            if (isFound)
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;


        }

        public static clsUser FindByNameAndPassword(string UserName, string password)
        {
            bool IsActive = false;
            int UserID = -1, PersonID = -1;


            bool isFound = clsUserData.FindUserByNameAndPassword(UserName, password, ref UserID, ref PersonID, ref IsActive);
            if (isFound)
                return new clsUser(UserID, PersonID, UserName, password, IsActive);
            else
                return null;

        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return (this.UserID != -1);
        }




        private bool _UpdateUser()
        {
            return clsUserData.UpdateNewUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);

        }
        // delete the below func
      /*  private bool ChangePassword()
        {

            return clsUserData.ChangePassword(this.UserID, this.Password);



        }*/


        public static clsUser FindUserById(int UserID)
        {

            string UserName = "", Password = "";
            int PersonID = -1;
            bool IsActive = true;



            if (clsUserData.FindUserById(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))

                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;

        }

        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;

                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }


        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();

        }

        public static bool DeleteUser(int ID)
        {
            return clsUserData.DeleteUser(ID);
        }

        public static bool IsUserExist(int UserID)
        {
            return (clsUserData.IsUserExist(UserID));
        }
        public static bool IsUserExist(string UserName)
        {
            return (clsUserData.IsUserExist(UserName));
        }


    }
}
