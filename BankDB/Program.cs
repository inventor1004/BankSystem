using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace BankDB
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            CustomerDALTest cusDalTest = new CustomerDALTest();

            /* ---------------------------------------------------------------
             * Uncomment if you want to test adding data to the customer table
             * ---------------------------------------------------------------
             * 
            // Test adding a new data to customer table
            int isProgramSuccess = cusDalTest.TestAddFileData();
            if (isProgramSuccess == 0)
            {
                Console.WriteLine("Data are successfully added to the customer table.");
                Console.WriteLine("Press any button to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Data are failed to add to the customer table.");
            }
            */


            /*
            * --------------------------------------------------------------------
            * Uncomment if you want to test delete data from the customer table
            * --------------------------------------------------------------------
            *
            // Test whether GetCustomerTable() retrieve all data from the customer table properly or not
            int howManyRows = cusDalTest.TestGetCustomerTable();
            Console.WriteLine($"{howManyRows} columns was retrieved.");
            // Print all data from the customer table
            printAllCustomerTable();
            */


            // Test the deletion of row from the customer table
            // The test row will be added and deleted by the TestDeleteCustomerRow()
            bool isDeletionSuccess = cusDalTest.TestDeleteCustomerRow();
            if(isDeletionSuccess == true)
            {
                Console.WriteLine($"Row was successfully deleted.");
            }
            else
            {
                Console.WriteLine($"Delition is failed.");
            }
            



            Console.WriteLine("Press any button to continue...");
            Console.ReadKey();
        }


        /*
         * Function	   : printAllCustomerTable()
         * Description : Retrieve all data from the customer table and print it 
         * Parameters  : void
         * Return      : void
         */
        public static void printAllCustomerTable()
        {
            CustomerDAL cusDAL = new CustomerDAL();
            List<CustomerEntity> customerData = cusDAL.GetCustomerTable();
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            foreach (CustomerEntity customer in customerData)
            {
                Console.WriteLine("Customer ID            : " + customer.CustomerID);
                Console.WriteLine("Customer Password      : " + customer.Password);            
                Console.WriteLine("Customer Name          : " + customer.FirstName + " " + customer.LastName);
                Console.WriteLine("Customer DateOfBirth   : " + customer.DateOfBirth);
                Console.WriteLine("Customer Address       : " + customer.Address);
                Console.WriteLine("Customer Phone Number  : " + customer.PhoneNumber);
                Console.WriteLine();
            };
            Console.WriteLine("-------------------------------------------------------------------------------------------");         
        }
    }
}
