using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X500;


namespace BankDB
{
    public class CustomerDALTest: CustomerDAL
    {
        /*
         * Function	   : ConnectionTest()
         * Description : This function is a method that checks whether the AddFildData method properly adds data to the customer account.
         * Parameters  : void
         * Return      : bool true  - process success
         *                    false - process failed
         */
        public void ConnectionTest()
        {
            try
            {
                connection.Open();
            }
            catch(Exception ex)
            {
                // Logger.Log(ex);
            }
            finally
            {
                connection.Close();

            }

        }

        /*
         * Function	   : TestAddFileData()
         * Description : This function is a method that checks whether the AddFildData method properly adds data to the customer account.
         * Parameters  : void
         * Return      : bool true  - process success
         *                    false - process failed
         */
        public bool TestAddFileData()
        {
            CustomerDAL    cusDal   = new CustomerDAL();
            CustomerEntity customer = new CustomerEntity();

            // Add test data components
            customer.Password    = "testPassword";
            customer.FirstName   = "John";
            customer.LastName    = "Smith";
            DateTime dateTime = new DateTime(2023, 12, 3);
            customer.SetDateOfBirth(dateTime);
            customer.Address     = "ABC St West 123";
            customer.PhoneNumber = "+1234567890";

            // Call the AddFileData method
            return cusDal.AddFileData(customer);
        }


        /*
         * Function	   : TestAddFileData()
         * Description : This method tests whether the GetCustomerTable function successfully retrieves all data in the customer table.
         * Parameters  : void
         * Return      : The total number of retrieved rows is returned
         */
        public int TestGetCustomerTable()
        {
            CustomerDAL customerDAL = new CustomerDAL();
            List<CustomerEntity> customerData = customerDAL.GetCustomerTable();
            int numberOfRow = 0;

            foreach (CustomerEntity customer in customerData)
            {
                if(customer.CustomerID != 0)
                {
                    ++numberOfRow;
                }    
            }

            return numberOfRow;
        }



        /*
         * Function	   : TestDeleteCustomerRow()
         * Description : This method tests whether the DeleteCustomerRow function successfully deletes selected data in the customer table.
         *              The test row will be added and deleted from the customer table
         * Parameters  : void
         * Return      : return 0  - Process success
         *               return -1 - process failed
         */
        public bool TestDeleteCustomerRow()
        {
            const bool kProcessFailed = false;

            // Add test data at the end of the customer table's row
            TestAddFileData();

            int tempCustomerID = -1;
            try
            {
                // Retrieves the most recently added customerID and passes it as an argument to DeleteCustomerRow().
                string sqlCmd = $"SELECT MAX(customerID) AS CustomerID FROM customer;";
                command.CommandText = sqlCmd;

                // connect to the SQL server and do the job
                connection.Open();
                reader = command.ExecuteReader();
                reader.Read();

                CustomerEntity entity = new CustomerEntity();
                entity.CustomerID = int.Parse(reader["CustomerID"].ToString());
                tempCustomerID = entity.CustomerID;
            }
            catch (Exception ex)
            {
                // Logger.Log(ex);
            }
            finally
            {
                connection.Close(); 
            }

            if (tempCustomerID != -1)
            {
                return DeleteCustomerRow(tempCustomerID);
            }

            return kProcessFailed;
        }

    }
}
