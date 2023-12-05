using BankDB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankDB
{
    public class AccountDAL: AccountEntity
    {
        internal MySqlConnection connection;
        internal MySqlCommand command;
        internal MySqlDataReader reader;

        TransactionDAL transactionDAL;

        public AccountDAL()
        {
            connection = new MySqlConnection("server=localhost;port=3306;uid=root;pwd=inventor1004++; database=BankDB");
            command = new MySqlCommand();
            command.Connection = connection;

            transactionDAL = new TransactionDAL();
        }


        public List<AccountEntity> GetAccountInfo (string CustomerID)
        {
            // SQL Syntax
            // >> Retrive  data from the Account table
            string sqlCmd = "SELECT AccountID, CustomerID, Balance, AccountType FROM Account;";
            List <AccountEntity> accountList = new List<AccountEntity>();

            try
            {
                command.CommandText = sqlCmd;
                connection.Open();
                reader = command.ExecuteReader();

                // Read all data from the customer table
                while (reader.Read())
                {
                    if (reader["CustomerID"].ToString() == CustomerID)
                    {
                        AccountEntity account = new AccountEntity();  
                        account.AccountID = reader["AccountID"].ToString();
                        account.CustomerID = reader["CustomerID"].ToString();
                        account.Balance = reader["Balance"].ToString();
                        account.AccountType = reader["AccountType"].ToString();

                        accountList.Add(account);
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

            return accountList;
        }


        public bool Deposit(string accountID, string amountOfMoney)
        {
            const bool kProcessFailed = false, kProcessSuccess = true;

            // SQL Syntax
            // >> update the balance from the Account table based on the AccountID
            string sqlCmd = $"UPDATE `Account` SET Balance = Balance + {amountOfMoney} WHERE AccountID = {accountID};";

            try
            {
                command.CommandText = sqlCmd;
                connection.Open();

                int result = command.ExecuteNonQuery();
                if(result == 0)
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

            // add new transaction recode to the transaction table
            transactionDAL.AddNewTransaction(accountID, accountID, "deposit", amountOfMoney);
            return kProcessSuccess;
        }

        public bool Withdraw(string accountID, string amountOfMoney)
        {
            const bool kProcessFailed = false, kProcessSuccess = true;

            // SQL Syntax
            // >> update the balance from the Account table based on the AccountID
            string sqlCmd = $"UPDATE `Account` SET Balance = Balance - {amountOfMoney} WHERE AccountID = {accountID};";

            try
            {
                command.CommandText = sqlCmd;
                connection.Open();

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

            // add new transaction recode to the transaction table
            transactionDAL.AddNewTransaction(accountID, accountID, "withdraw", amountOfMoney);
            return kProcessSuccess;
        }


        public bool Transfer(string accountIDFrom, string toAccountID, string amountOfMoney)
        {
            const bool kProcessFailed = false, kProcessSuccess = true;

            // SQL Syntax
            // >> Check whether the receiver's account exist or not
            string sqlCmdFindID = $"SELECT AccountID FROM `Account` WHERE AccountID = 1;";

            // Prepare to witdraw from the sender's account and 
            // to deposit to the receiver's account
            // if the receiver's account exist in the account table
            string sqlCmdWitdraw = string.Empty;
            string sqlCmdDeposit = string.Empty;


            try
            {
                command.CommandText = sqlCmdFindID;
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string accountID = reader["AccountID"].ToString();

                    // if ID and Password are found in the customer table, return the customerID
                    if (accountID == toAccountID)
                    {
                        sqlCmdWitdraw = $"";
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

            // add new transaction recode to the transaction table
            // transactionDAL.AddNewTransaction(accountID, accountID, "withdraw", amountOfMoney);
            return kProcessSuccess;
        }
    }
}
