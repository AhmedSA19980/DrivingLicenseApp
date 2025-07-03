using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSDATA
{
    public class clsTestAppointmentsData
    {
            
       
             public static bool FindTestAppointmentByID(
                 int TestAppointmentID,ref DateTime AppointmentDate,
                 ref int CreatedByUserID,ref int LocalDrivingLicenseApplicationID, 
                 ref int TestTypeID,ref float PaidFees,
                 ref bool IsLocked , ref int RetakeTestApplicationID)
             {
                bool isFound = false;

                SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

                string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {

                        // The record was found
                        isFound = true;
                        TestTypeID = (int)reader["TestTypeID"];
                        LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                        AppointmentDate = (DateTime)reader["AppointmentDate"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];
                        PaidFees = Convert.ToSingle(reader["PaidFees"]);
                        IsLocked = (bool)reader["IsLocked"];

                   
                        if (reader["RetakeTestApplicationID"] == DBNull.Value)
                            RetakeTestApplicationID = -1;
                        else
                            RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                    
             
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
                    //Console.WriteLine("Error: " + ex.Message);
                    isFound = false;
                }
                    finally
                {
                    connection.Close();
                }

                    return isFound;
             }
    



            public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
            {
                DataTable dt = new DataTable();
                SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

                string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                        FROM TestAppointments
                        WHERE  
                        (TestTypeID = @TestTypeID) 
                        AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        order by TestAppointmentID desc;";


                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }

                    reader.Close();

                }catch (Exception ex) { 
            
                }finally { connection.Close(); }    
                return dt;
            }



             public static int AddNewTestAppointment(  DateTime AppointmentDate,  int CreatedByUserID,
                 int LocalDrivingLicenseApplicationID,  int TestTypeID,  float PaidFees ,int RetakeTestApplicationID)
                {
            //this function will return the new contact id if succeeded and -1 if not.
                int TestAppointmentID = -1;

                SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

                string query = @"Insert Into TestAppointments (TestTypeID,LocalDrivingLicenseApplicationID,AppointmentDate,PaidFees,CreatedByUserID,IsLocked,RetakeTestApplicationID)
                            Values (@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedByUserID,0,@RetakeTestApplicationID);
                            SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
              
                if(RetakeTestApplicationID == -1)
                    command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value );
                else
                    command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            try
                {
                    connection.Open();

                    object result = command.ExecuteScalar();


                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                    TestAppointmentID = insertedID;
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


                return TestAppointmentID;
             }


        public static bool UpdatTestAppointment(int TestAppointmentID ,DateTime AppointmentDate, int CreatedByUserID,
                 int LocalDrivingLicenseApplicationID, int TestTypeID, float PaidFees, bool IsLocked , int RetakeTestApplicationID)
        {
            //this function will return the new contact id if succeeded and -1 if not.
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = @"UPDATE TestAppointments SET 
                            AppointmentDate=@AppointmentDate ,
                            CreatedByUserID = @CreatedByUserID  ,
                            LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                            TestTypeID=@TestTypeID  ,
                            PaidFees = @PaidFees,
                            IsLocked= @IsLocked,
                            RetakeTestApplicationID=@RetakeTestApplicationID
                             WHERE TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);


            if (RetakeTestApplicationID == -1)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);


            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }


            return rowAffected > 0;
        }


        public static int GetActiveTestAppointmentIDTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            int TestActiveAppointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = @"SELECT top 1 found=1  FROM TestAppointments inner join  LocalDrivingLicenseApplications ON 
                    TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.ApplicationID 
                    WHERE TestAppointments.TestTypeID =@TestTypeID AND 
                    TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestAppointments.IsLocked !=1 ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
           // command.Parameters.AddWithValue("@IsLocked", IsLocked);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    TestActiveAppointmentID = AppID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return TestActiveAppointmentID;
            }
            finally
            {
                connection.Close();
            }

            return TestActiveAppointmentID;
        }

        public static DataTable GetTestAppointments()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = @"SELECT TestAppointments.TestAppointmentID  as AppointmentID, TestAppointments.AppointmentDate , TestAppointments.PaidFees,
                            TestAppointments.IsLocked FROM TestAppointments";

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

        public  static bool GetLastTestAppointment(
        int LocalDrivingLicenseApplicationID,  int TestTypeID,
            ref int TestAppointmentID,ref DateTime AppointmentDate,
            ref float PaidFees, ref int CreatedByUserID,
            ref bool IsLocked,ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = @"SELECT       top 1 *
                FROM            TestAppointments
                WHERE        (TestTypeID = @TestTypeID) 
                AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                order by TestAppointmentID Desc";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];


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
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

      

    }
}
