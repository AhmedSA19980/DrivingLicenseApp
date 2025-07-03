using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSDATA
{
    public class clsTestsData
    {

        public static bool GetTestById(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = "SELECT * FROM TESTS WHERE TestID=@TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    //TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }else
                        Notes = (string)reader["Notes"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];



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


        public static bool IsTestExist(int TestID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = "SELECT * TOP 1 IsFounnd = 1 FROM TESTS WHERE TestID=@TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

               
                    isFound = reader.HasRows;
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


        public static bool GetID(int TestID , int TestAppointment)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = "SELECT * TOP 1 IsFounnd = 1 FROM TESTS WHERE TestID=@TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                isFound = reader.HasRows;
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



        public static int GetTestAppointmentID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = "SELECT TestID FROM Tests WHERE TestAppointmentID =@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);



            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    TestID = InsertedID;

                }

              


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
               // TestID = -1;
            }
            finally
            {
                connection.Close();
            }

            return TestID;


        }


        public static int AddNewTest( int TestAppointmentID,  bool TestResult,  string Notes,  int CreatedByUserID)
        {
            //this function will return the new contact id if succeeded and -1 if not.
            int TestID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = @"INSERT INTO Tests (TestAppointmentID ,TestResult,Notes ,CreatedByUserID)
                             VALUES (@TestAppointmentID ,@TestResult,@Notes ,@CreatedByUserID);

                             UPDATE TestAppointments SET IsLocked = 1 WHERE TestAppointmentID=@TestAppointmentID;
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);

            if (Notes != "" && Notes != null)
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
         

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return TestID;
        }


        public static bool UpdateTest( int TestID ,int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int rowAfffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"UPDATE Tests SET TestAppointmentID=@TestAppointmentID,
                        TestResult=@TestResult,
                        Notes=@Notes,
                        CreatedByUserID =@CreatedByUserID
                        WHERE TestID =@TestID";

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@TestID", TestID);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            cmd.Parameters.AddWithValue("@TestResult", TestResult);
            if (Notes != "" && Notes != null)
                cmd.Parameters.AddWithValue("@Notes", Notes);
            else
                cmd.Parameters.AddWithValue("@Notes", System.DBNull.Value);
    
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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


        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass(
            int PersonID  , int LicenseClassID , int TestTypeID ,ref int TestID,
            ref int TestAppointmentID , ref bool TestResult ,
            ref string Notes , ref int CreatedByUserID
            ) {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = @"SELECT  top 1 Tests.TestID, 
                Tests.TestAppointmentID, Tests.TestResult, 
			    Tests.Notes, Tests.CreatedByUserID, Applications.ApplicantPersonID
                FROM            LocalDrivingLicenseApplications INNER JOIN
                                         Tests INNER JOIN
                                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                WHERE        (Applications.ApplicantPersonID = @PersonID) 
                        AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                        AND ( TestAppointments.TestTypeID=@TestTypeID)
                ORDER BY Tests.TestAppointmentID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);



            try {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read()) { 
                    isFound = true; 
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] == DBNull.Value)

                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
                else { isFound = false; }
                reader.Close();
            }
            catch (Exception ex) {
            
                isFound = false;

            }
            finally { connection.Close(); }   

            return isFound;


        }


        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss) ;

            string query = @"SELECT PassedTestCount = count(TestTypeID)
                         FROM Tests INNER JOIN
                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
						 where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID and TestResult=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                {
                    PassedTestCount = ptCount;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return PassedTestCount;



        }



    }
}
