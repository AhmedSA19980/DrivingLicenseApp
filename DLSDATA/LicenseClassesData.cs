using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSDATA
{
    public class clsLicenseClassesData
    {

        public static bool FindLicenseClassByID( int LicenseClassID, ref string ClassName,  ref string ClassDescription, ref byte MinimumAllowedAge, 
            ref byte DefaultValidityLength , ref float ClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = "SELECT * FROM LicenseClasses WHERE LicenseClassID=@LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                   MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
      

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }



        public static bool FindLicenseClassByName(  string ClassName,ref int LicenseClassID, ref string ClassDescription, ref byte MinimumAllowedAge,
            ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = "SELECT * FROM LicenseClasses WHERE ClassName=@ClassName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];

                    ClassFees =Convert.ToSingle( reader["ClassFees"]);


                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static DataTable GetLicenseCalsses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = @"SELECT *  FROM LicenseClasses Order By ClassName";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static int AddNewLicenseClass(  string ClassName, string ClassDescription,  byte MinimumAllowedAge,
            byte DefaultValidityLength,  float ClassFees)
        {
            int LicenseClassID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"INSERT INTO LicenseClasses(ClassName , ClassDescription , MinimumAllowedAge , DefaultValidityLength ,ClassFees)
                  VALUES(@ClassName , @ClassDescription , @MinimumAllowedAge , @DefaultValidityLength , @ClassFees);
                  SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@ClassName", ClassName);
            cmd.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            cmd.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            cmd.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            cmd.Parameters.AddWithValue("@ClassFees", ClassFees);


            try
            {
                connection.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    LicenseClassID = InsertedID;

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.Message);
            }
            finally { connection.Close(); }


            return LicenseClassID;
        }



        public static bool UpdateLicenseClass(int LicenseClassID , string ClassName, string ClassDescription, byte MinimumAllowedAge,
            byte DefaultValidityLength, float ClassFees)
        {
            int rowAfffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"UPDATE LicenseClasses SET ClassName=@ClassName , 
                        ClassDescription=@ClassDescription  , 
                        MinimumAllowedAge=@MinimumAllowedAge,
                        DefaultValidityLength=@DefaultValidityLength,
                        ClassFees=@ClassFees
                        WHERE LicenseClassID =@LicenseClassID";

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            cmd.Parameters.AddWithValue("@ClassName", ClassName);
            cmd.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            cmd.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            cmd.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            cmd.Parameters.AddWithValue("@ClassFees", ClassFees);


            try
            {
                connection.Open();
                rowAfffected = cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;

            }
            finally { connection.Close(); }


            return rowAfffected > 0;
        }


    }
}
