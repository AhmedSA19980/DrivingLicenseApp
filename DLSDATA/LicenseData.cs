using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DLSDATA
{
    public class clsLicenseData
    {



        public static bool FindLicenseById(int LicenseID ,ref  int ApplicationID,ref int DriverID,ref int LicenseClass, ref int CreatedByUserID
            ,ref DateTime IssueDate,ref DateTime ExpirationDate,ref string Notes,ref float PaidFees,ref bool IsActive,ref byte IssueReason)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = "SELECT * FROM Licenses WHERE LicenseID= @LicenseID";

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
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = (string)reader["Notes"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];





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



     
       public static DataTable GetDriverLicenses(int DriverID)
        {

            DataTable dt = new DataTable();
            // changed Driver.personID =Licenses.LicenseID
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = @"SELECT Licenses.LicenseID,
                                   ApplicationID,
                                   LicenseClasses.ClassName, Licenses.IssueDate,
                                   Licenses.ExpirationDate, Licenses.IsActive
                                   FROM Licenses INNER JOIN
                                        LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
        
                                    where DriverID = @DriverID
        
                                    Order By IsActive Desc, ExpirationDate Desc;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
     
            try
            {
                connection.Open();
               SqlDataReader Reader = command.ExecuteReader();  
                if(Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        public static int GetActiveLicenseByPersonID(int PersonID,  int LicenseClass)
        {

            int LicenseID = -1;
            // changed Driver.personID =Licenses.LicenseID
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = @"SELECT  Licenses.LicenseID FROM Licenses inner join
                Drivers on Drivers.DriverID = Licenses.DriverID 
                WHERE
                Drivers.PersonID =@PersonID 
                and Licenses.LicenseClass=@LicenseClass 
                and Licenses.IsActive = 1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedLicenseID))
                {
                    LicenseID = InsertedLicenseID;

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

            return LicenseID;
        }



        public static bool IsLicenseValidForIntenrationalLicense(int LicenseID) {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"select top 1  * from Licenses where Licenses.LicenseID =@LicenseID
                        and (GETDATE() between IssueDate and ExpirationDate )and (IsActive = 1) and(LicenseClass = 3) ";

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

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

        public static int AddNewLicnese( int ApplicationID, int DriverID, int LicenseClass
            , DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID) {


            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"Insert Into Licenses (ApplicationID , DriverID ,LicenseClass ,IssueDate,
                    ExpirationDate,Notes,PaidFees,IsActive,IssueReason,CreatedByUserID )
                    VALUES(@ApplicationID , @DriverID ,@LicenseClass  ,@IssueDate, 
                            @ExpirationDate,@Notes,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID)
                    SELECT SCOPE_IDENTITY();"
            ;

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@Notes", Notes);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@IssueReason", IssueReason);


            try { 
                connection.Open();  
                object Result = cmd.ExecuteScalar();   
               
                if(Result != null && int.TryParse(Result.ToString() , out int InsertedLicenseID))
                {
                    LicenseID = InsertedLicenseID;  

                }

            } catch (Exception ex){

                Console.WriteLine("Error: " + ex.Message);
            } finally { connection.Close(); };


            return LicenseID;

        }

        public static bool DeactivateCurrentLicense(int LicenseID)
        {
            int rowAfffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"UPDATE Licenses SET 
                        IsActive=0
                      
    
                        WHERE LicenseID =@LicenseID";

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            try {
                connection.Open();
                rowAfffected = cmd.ExecuteNonQuery();   

            
            }catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            finally { connection.Close(); };
            return rowAfffected > 1;


        }
        public static bool UpdateLicense(int LicenseID,int ApplicationID, int DriverID, int LicenseClass, int CreatedByUserID
            , DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, byte IssueReason)
        {
            int rowAfffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string Query = @"UPDATE USERS SET ApplicationID=@ApplicationID , 
                        DriverID=@DriverID  , 
                        LicenseClass=@LicenseClass,
                        CreatedByUserID=@CreatedByUserID,
                        IssueDate=@IssueDate,
                        ExpirationDate=@ExpirationDate,      
                        Notes=@Notes,
                        PaidFees=@PaidFees,
                        IsActive=@IsActive,
                        IssueReason=@IssueReason
    
                        WHERE LicenseID =@LicenseID";

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@Notes", Notes);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@IssueReason", IssueReason);


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

        public static DataTable GetAllLicenses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.Addresss);

            string query = "SELECT * FROM Licenses";

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
    }
}
