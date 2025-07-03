using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DLSDATA
{
    public class clsInternationalLicenseData
    {

        public static bool FindInternationalLicenseByID(int InternationalLicenseID , ref int ApplicationID, ref int  DriverID,ref  int IssuedUsingLocalLicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID=@InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
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

      

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            DataTable dt = new DataTable(); ;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"SELECT    InternationalLicenseID, ApplicationID,
		                IssuedUsingLocalLicenseID , IssueDate, 
                        ExpirationDate, IsActive
		    from InternationalLicenses where DriverID=@DriverID
                order by ExpirationDate desc";


            SqlCommand cmd = new SqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@DriverID", DriverID)
                ;
            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.HasRows)
                {
                    dt.Load(read);
                }
                connection.Close();

            }
            catch(Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            finally { connection.Close(); }

            return dt;
        }



        public static DataTable GetAllInternationalDrivingLicenes()
        {
            DataTable dt = new DataTable(); ;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"select InternationalLicenseID , ApplicationID ,DriverID, IssuedUsingLocalLicenseID, IssueDate ,ExpirationDate, IsActive  
                            from InternationalLicenses  order by IsActive, ExpirationDate desc";


            SqlCommand cmd = new SqlCommand(Query, connection);
      
                ;
            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                if (read.HasRows)
                {
                    dt.Load(read);
                }
                connection.Close();

            }
            catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            finally { connection.Close(); }

            return dt;
        }

        public static int AddNewInternationalLicense(  int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate,  bool IsActive, int CreatedByUserID)
        {
            int InternationalLicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            // if we  driver has previous international licesne then deactivate it .
            string Query = @"
             Update InternationalLicenses 
                               set IsActive=0
                               where DriverID=@DriverID;

             INSERT INTO InternationalLicenses ( ApplicationID,  DriverID,  IssuedUsingLocalLicenseID,
             IssueDate,  ExpirationDate,   IsActive,  CreatedByUserID)
                                        VALUES( @ApplicationID,  @DriverID,  @IssuedUsingLocalLicenseID,
             @IssueDate,  @ExpirationDate,   @IsActive,  @CreatedByUserID);
                                        SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    InternationalLicenseID = InsertedID;

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.Message);
            }
            finally { connection.Close(); }


            return InternationalLicenseID;
        }



        public static bool UpdateInternationalLicense( int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int rowAfffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"UPDATE InternationalLicenses SET ApplicationID=@ApplicationID , 
                        DriverID=@DriverID  , 
                        IssuedUsingLocalLicenseID=@IssuedUsingLocalLicenseID,
                        IssueDate=@IssueDate,
                        ExpirationDate=@ExpirationDate,
                        IsActive=@IsActive,
                        CreatedByUserID=@CreatedByUserID    
                        WHERE InternationalLicenseID =@InternationalLicenseID";


            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
           
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


        
        



        public static int GetPersonInfo(int DriverID)
        {
            int LicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"select top 1  People.PersonID from People inner join Drivers on Drivers.PersonID = People.PersonID inner join InternationalLicenses on    
                            Drivers.DriverID =InternationalLicenses.DriverID  
                            where  InternationalLicenses.DriverID =@DriverID; ";


            SqlCommand cmd = new SqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@DriverID", DriverID)
                ;
            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertID))
                {

                    LicenseID = InsertID;

                }
                connection.Close();

            }
            catch { LicenseID = -1; }
            finally { connection.Close(); }

            return LicenseID;
        }

        public static int GetActiveInternationalLicense(int DriverID)
        {
            int internationalid = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"select top 1 * from InternationalLicenses where DriverID = @DriverID
                and  GETDATE() between IssueDate and ExpirationDate and IsActive = 1 ; ";


            SqlCommand cmd = new SqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@DriverID", DriverID)
                ;
            try {
                connection.Open();
                object Result = cmd.ExecuteScalar();

                if(Result != null && int.TryParse(Result.ToString() , out int InsertID)) {

                    internationalid = InsertID;    
                
                }connection.Close();
            
            } catch { internationalid = -1; } finally { connection.Close(); }   

            return internationalid;
        }


        }
}
