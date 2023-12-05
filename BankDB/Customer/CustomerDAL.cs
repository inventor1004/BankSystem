using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X500;


namespace BankDB
{
    public class CustomerDAL
    {

        internal MySqlConnection connection;
        internal MySqlCommand command;
        internal MySqlDataReader reader;

        public CustomerDAL()
        {
            connection = new MySqlConnection("server=localhost;port=3306;uid=root;pwd=inventor1004++; database=BankDB");
            command = new MySqlCommand();
            command.Connection = connection;
        }


        /*
         * Function	   : public int AddFileData(CustomerEntity ce)
         * Description : This method add new colum to the customer table in SQL
         * Parameters  : CustomerEntity ce - the object which contains 
         * Return      : return true  - Process success
         *               return flase - process failed
         */
        public bool AddFileData(CustomerEntity ce)
        {
            const bool kProcessFailed = false, kProcessSuccess = true;
            // Check Password is not NULL
            if (ce.Password == null)
            {
                // Logger.Log("Pasword can not be null.");
                return kProcessFailed;
            }

            
            // SQL Syntax
            // Create the query want to add
            string sqlCmd = "INSERT INTO Customer (ID, Password,  FirstName, LastName, DateOfBirth, Address, PhoneNumber)"
                               + "VALUES("
                                           + "'" + ce.ID          + "'" + ","
                                           + "'" + ce.Password    + "'" + ","
                                           + "'" + ce.FirstName   + "'" + ","
                                           + "'" + ce.LastName    + "'" + ","
                                           + "'" + ce.DateOfBirth + "'" + ","
                                           + "'" + ce.Address     + "'" + ","
                                           + "'" + ce.PhoneNumber + "'" +     ");";

            try
            {
                // add the values to tje sql database 
                command.CommandText = sqlCmd;
                connection.Open();
                int result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Logger.Log(ex);
            }
            finally
            {
                connection.Close();
            }

            return kProcessSuccess;
        }


        /*
         * Function	   : List<CustomerEntity> GetCustomerTable()
         * Description : Retrieve all data from the Customer table and return as a List of CustomerEntity
         * Parameters  : void
         * Return      : List<CustomerEntity> - Contain all data from the customer table          
         */
        public List<CustomerEntity> GetCustomerTable()
        {
            // SQL Syntax
            // >> Retrive all data from the customer table
            string sqlCmd = "SELECT CustomerID, Password, FirstName, LastName, DateOfBirth, Address, PhoneNumber FROM CUSTOMER ORDER BY CustomerID DESC;";
            List<CustomerEntity> dataList = new List<CustomerEntity>();

            try
            {
                command.CommandText = sqlCmd;
                connection.Open();
                reader = command.ExecuteReader();

                // Read all data from the customer table
                while (reader.Read())
                {
                    CustomerEntity entity = new CustomerEntity();
                    entity.CustomerID  = reader["CustomerID"].ToString();
                    entity.Password    = reader["Password"].ToString();
                    entity.FirstName   = reader["FirstName"].ToString();
                    entity.LastName    = reader["LastName"].ToString();
                    entity.DateOfBirth = reader["DateOfBirth"].ToString();
                    entity.Address     = reader["Address"].ToString();
                    entity.PhoneNumber = reader["PhoneNumber"].ToString();
                    dataList.Add(entity);

                }
            }
            catch (Exception ex)
            {
                // Logger.Log(ex);
            }
            finally
            {
                connection.Close();
            }

            return dataList;
        }


        /*
        * Function	  : DeleteCustomerRow()
        * Description : Delecte the selected row based on the customerID
        * Parameters  : int customerID
        * Return      : return ture  - Process success
        *               return false - process failed
        */
        public bool DeleteCustomerRow(int customerID)
        {
            const bool kProcessSuccess = true, kProcessFailed = false;

            // SQL Syntax
            // >> Retrive all data from the customer table
            string sqlCmd = $"DELETE FROM customer WHERE customerID = {customerID};";

            try
            {
                command.CommandText = sqlCmd;
                connection.Open();

                // Execute the DELETE command
                //  >> The deleted number of rows will be stored in the rowsAffected
                //    So, rowsAffected should be 1 if the deleting process is successful
                int rowsAffected = command.ExecuteNonQuery();

                // Check if any row was affected
                if (rowsAffected < 1)
                {
                    // customerID not found or deletion failed
                    return kProcessFailed;
                }
 
            }
            catch (Exception ex)
            {
                // Logger.Log(ex);
            }
            finally
            {
                connection.Close();
            }

            return kProcessSuccess;
        }


        public string LogInValidation(string ID, string PW)
        {

            // SQL Syntax
            string sqlCmd = "SELECT CustomerID, ID, Password FROM Customer;";

            try
            {
                command.CommandText = sqlCmd;
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CustomerEntity entity = new CustomerEntity();
                    entity.CustomerID = reader["CustomerID"].ToString();
                    entity.ID         = reader["ID"].ToString();
                    entity.Password   = reader["Password"].ToString();                 

                    // if ID and Password are found in the customer table, return the customerID
                    if (entity.ID == ID && entity.Password == PW)
                    {
                        return entity.CustomerID;
                    }
                }
            }
            catch (Exception ex)
            {
                // Logger.Log(ex);
            }
            finally
            {
                connection.Close();
            }

            return string.Empty;
        }
    }
}
