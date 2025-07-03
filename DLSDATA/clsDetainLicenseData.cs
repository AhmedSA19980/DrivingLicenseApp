using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSDATA
{
    public  class clsDetainLicenseData
    {
        public static bool FindDetainLicensebyID(int DetainID,ref int LicenseID,ref int ReleasedByUserID,ref int CreatedByUserID
            ,ref int ReleaseApplicationID,ref bool IsReleased,ref float FineFees,ref DateTime DetainDate,ref DateTime ReleasedDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = "SELECT * FROM DetainedLicenses WHERE DetainID= @DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    LicenseID = (int)reader["LicenseID"];
                   
                    CreatedByUserID = (int)reader["CreatedByUserID"];
           
                    IsReleased = (bool)reader["IsReleased"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    DetainDate = (DateTime)reader["DetainDate"];


                    if (reader["ReleaseDate"] == DBNull.Value)
                        ReleasedDate = DateTime.MaxValue;
                    else
                         ReleasedDate = (DateTime)reader["ReleaseDate"];

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];



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

        public static bool GetDetainInfoByLicenseID(int LicenseID, ref int DetainID , ref int ReleasedByUserID, ref int CreatedByUserID
            , ref int ReleaseApplicationID, ref bool IsReleased, ref float FineFees, ref DateTime DetainDate, ref DateTime ReleasedDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = "SELECT * FROM DetainedLicenses WHERE LicenseID= @LicenseID Order by DetainID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    DetainID = (int)reader["DetainID"];

                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    IsReleased = (bool)reader["IsReleased"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    DetainDate = (DateTime)reader["DetainDate"];


                    if (reader["ReleaseDate"] == DBNull.Value)
                        ReleasedDate = DateTime.MaxValue;
                    else
                        ReleasedDate = (DateTime)reader["ReleaseDate"];

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];



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




       


        public static bool IsLicenseDetained(int LicenseID)
        {


            bool IsDetained = false;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"select IsDetained=1 from DetainedLicenses where LicenseID=@LicenseID and IsReleased = 0;"
            ;

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();

                if (Result != null)
                {
                    IsDetained = Convert.ToBoolean(Result);

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally { connection.Close(); };


            return IsDetained;

        }

        public static int AddDetainedLicense(  int LicenseID,  int CreatedByUserID
            ,   bool IsReleased,  float FineFees,  DateTime DetainDate)
        {


            int DetainID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"Insert Into DetainedLicenses (LicenseID , DetainDate ,FineFees ,CreatedByUserID,
                    IsReleased)

                    VALUES(@LicenseID , @DetainDate ,@FineFees  ,@CreatedByUserID, 
                            0)
                    SELECT SCOPE_IDENTITY();"
            ;

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
       
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            cmd.Parameters.AddWithValue("@IsReleased", IsReleased);
            cmd.Parameters.AddWithValue("@FineFees", FineFees);
            cmd.Parameters.AddWithValue("@DetainDate", DetainDate);




            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedLicenseID))
                {
                    DetainID = InsertedLicenseID;

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.Message);
            }
            finally { connection.Close(); };


            return DetainID;

        }


        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, int CreatedByUserID
          , bool IsReleased, float FineFees, DateTime DetainDate)
        {


            int rowAfffected = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"Update DetainedLicenses SET 
                                LicenseID = @LicenseID,
                                CreatedByUserID =@CreatedByUserID,
                                IsReleased=@IsReleased,
                                FineFees=@FineFees ,
                                DetainDate=@DetainDate

                                WHERE DetainID =@DetainID "
            ;

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            cmd.Parameters.AddWithValue("@IsReleased", IsReleased);
            cmd.Parameters.AddWithValue("@FineFees", FineFees);
            cmd.Parameters.AddWithValue("@DetainDate", DetainDate);




            try
            {
                connection.Open();
                rowAfffected = cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                

            }
            finally { connection.Close(); }


            return rowAfffected > 0;

        }


        public static DataTable GetAllDetainedLicenses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = "select * from DetainedLicenses_view order by IsReleased, DetainID";

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


        public static bool ReleasedDetainedLicense( int LicenseID, int ReleasedByUserID
          , int ReleaseApplicationID)
        {


            int rowAfffected = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"Update DetainedLicenses SET 
                              
                                IsReleased=1,
                                ReleaseDate=@ReleaseDate,
                                ReleasedByUserID=@ReleasedByUserID,
                                ReleaseApplicationID=@ReleaseApplicationID 

                                WHERE LicenseID =@LicenseID and
                                DetainID = (select top 1 DetainID from DetainedLicenses where LicenseID =@LicenseID order by DetainID DESC)"
            ;

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            cmd.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            cmd.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

            cmd.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);





            try
            {
                connection.Open();
                rowAfffected = cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);


            }
            finally { connection.Close(); }


            return rowAfffected > 0;

        }



    }
}
