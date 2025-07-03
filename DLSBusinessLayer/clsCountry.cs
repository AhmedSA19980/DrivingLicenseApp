using DLSDATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSBusinessLayer
{
    public class clsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }


        public clsCountry()
        {

            this.CountryID = -1;
            this.CountryName = "";

        }

        public  clsCountry(int CountryID, string CountryName)
        {

            this.CountryID = CountryID;
            this.CountryName = CountryName;

        }

        public static clsCountry Find(int countryID)
        {
            string countryName = "";
            if (clsCountryData.FindById(countryID, ref countryName))
            {

                return new clsCountry(countryID, countryName);
            }
            else return null;

        }

        public static clsCountry Find(string countryName)
        {
            int countryID = -1;
            //string countryName = "";
            if (clsCountryData.FindByName(countryName, ref countryID))
            {

                return new clsCountry(countryID, countryName);
            }
            else return null;

        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllData();
        }

    }
}
