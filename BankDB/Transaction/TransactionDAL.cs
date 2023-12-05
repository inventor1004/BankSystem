using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDB
{
    internal class TransactionDAL: TransactionEntity
    {
        internal MySqlConnection connection;
        internal MySqlCommand command;
        internal MySqlDataReader reader;

        public TransactionDAL() 
        {
            connection = new MySqlConnection("server=localhost;port=3306;uid=root;pwd=inventor1004++; database=BankDB");
            command = new MySqlCommand();
            command.Connection = connection;
        }


        /*
         * Function	   :
         * Description :
         * Parameters  :
         * Return      :           
         */
        public bool AddNewTransaction(int senderAccountID, int receiverAccountID, string typeOfTransaction, double amountOfMoney)
        {
            const bool kProcessFailed = false, kProcessSuccess = true;

            // get current time and transfer to 24hr format 
            DateTime dateTime = DateTime.Now;
            string formattedDateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

            // SQL Syntax
            // >> Retrive all data from the customer table
            string sqlCmd = "INSERT INTO `Transaction` (SenderAccountID, ReceiverAccountID, TransactionType, Amount, TransactionDate) " +
                           $"VALUES ({senderAccountID}, {receiverAccountID}, '{typeOfTransaction}', {amountOfMoney}, '{formattedDateTime}');";

            try
            {
                // add the values to tje sql database 
                command.CommandText = sqlCmd;
                connection.Open();

                // check whether the data is successfully added
                int result = command.ExecuteNonQuery();
                if (result == 0)
                {
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
    }
}
