using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankDB;

namespace BankSystemSQ
{
    public partial class Mainpage : System.Web.UI.Page
    {
        AccountDAL accountDAL = new AccountDAL();
        List<AccountEntity> accountList = new List<AccountEntity>();
        string CustomerID;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (PreviousPage != null && PreviousPageViewState != null)
            {
                accountDAL    = new AccountDAL();

                CustomerID = PreviousPageViewState["CustomerID"].ToString();
                ViewState["CustomerID"] = CustomerID;

                accountList = accountDAL.GetAccountInfo(CustomerID);             
                checkingAmountLiteral.Text = $"${accountList[0].Balance}";
                savingAmountLiteral.Text = $"${accountList[1].Balance}";
                
            }
            else
            {
                CustomerID = ViewState["CustomerID"].ToString();

                accountList = accountDAL.GetAccountInfo(CustomerID);

                checkingAmountLiteral.Text = $"${accountList[0].Balance}";
                savingAmountLiteral.Text = $"${accountList[1].Balance}";

            }


        }


        protected void TCDepositButton_Click(object sender, EventArgs e)
        {
            // Get the entered amount of money the client want to depoist
            string depositAmount = TransactionInput.Text;
            int whichAccount;
            bool isDesopistSucceess;

            // if the user does not enter amount of money or enter negative value, return;
            if (depositAmount == "" || double.Parse(depositAmount) < 0)
            {            
                return;
            }

            // Check which account the user is trying to deposit into
            string selectedAccountType = TCaccountList.SelectedValue;
            if (selectedAccountType == "chequing")
            {
                whichAccount = 0;
            }
            else if (selectedAccountType == "saving")
            {
                whichAccount = 1;
            }
            else
            {
                // Client did not selcet the account
                ShowMessage("Please Selcet account to deposit.");
                return;
            }

            // Start deposit
            isDesopistSucceess = accountDAL.Deposit(accountList[whichAccount].AccountID, depositAmount);

            // if deposit is suceeded, updated the balance to the selected account
            if (isDesopistSucceess == true)
            {
                string preBalance = accountList[whichAccount].Balance;
                double updatedBalace = double.Parse(preBalance) + double.Parse(depositAmount);
                accountList[whichAccount].Balance = updatedBalace.ToString();
            }

            // update the current balance display
            if (whichAccount == 0)
            {
                checkingAmountLiteral.Text = $"${accountList[0].Balance}";
            }
            else
            {
                savingAmountLiteral.Text = $"${accountList[1].Balance}";
            }

            // Clear the text box
            TransactionInput.Text = "";     
        }


        protected void TCWithdrawButton_Click(object sender, EventArgs e)
        {

            // Get the entered amount of money the client want to depoist
            string depositAmount = TransactionInput.Text;
            int whichAccount;
            bool isWithdrawSucceess;

            // if the user does not enter amount of money or enter negative value, return;
            if (depositAmount == "" || double.Parse(depositAmount) < 0)
            {
                return;
            }

            // Check which account the user is trying to deposit into
            string selectedAccountType = TCaccountList.SelectedValue;
            if (selectedAccountType == "chequing")
            {
                whichAccount = 0;
                // cancle the withdraw transaction if the client trys to withdraw more money than the current balance
                if (double.Parse(depositAmount) > double.Parse(accountList[whichAccount].Balance)) return;
            }
            else if (selectedAccountType == "saving")
            {
                whichAccount = 1;
                // cancle the witdraw transaction if the client trys to withdraw more money than the current balance
                if (double.Parse(depositAmount) > double.Parse(accountList[whichAccount].Balance)) return;
            }
            else
            {
                // Client did not selcet the account
                ShowMessage("Please Selcet account to withdraw.");
                return;
            }

            // Start withdrawing
            isWithdrawSucceess = accountDAL.Withdraw(accountList[whichAccount].AccountID, depositAmount);

            // if deposit is suceeded, updated the balance to the selected account
            if (isWithdrawSucceess == true)
            {
                string preBalance = accountList[whichAccount].Balance;
                double updatedBalace = double.Parse(preBalance) - double.Parse(depositAmount);
                accountList[whichAccount].Balance = updatedBalace.ToString();
            }

            // update the current balance display
            if (whichAccount == 0)
            {
                checkingAmountLiteral.Text = $"${accountList[0].Balance}";
            }
            else
            {
                savingAmountLiteral.Text = $"${accountList[1].Balance}";
            }

            // Clear the text box
            TransactionInput.Text = "";
        }

        public void TFDepositButton_Click(object sender, EventArgs e)
        {

            // Get the entered amount of money the client want to depoist
            string depositAmount = TransferInput.Text;
            int AccountFrom;
            int AccountTo;
            bool isTrensferSucceess;

            // if the user does not enter amount of money or enter negative value, return;
            if (depositAmount == "" || double.Parse(depositAmount) < 0)
            {
                return;
            }



        }

        public void SMDepositButton_Click(object sender, EventArgs e)
        {

        }

        private void UpdateCheckingBalance()
        {
            /*
            checkingAmountLiteral.Text = $"${checkingBalance}";
            ViewState["checkingBalance"] = checkingBalance;
            customerEntity.CheckingBalance = checkingBalance;
            dal.UpdateChekingBalance(customerEntity);
            */
        }

        private void ShowMessage(string message)
        {
            transactionMessage.InnerHtml = message;
        }

        private bool IsNumeric(string input)
        {
            return double.TryParse(input, out _);
        }

        // ------------------------------------------------------------
        // Name      : ReturnViewState
        // Purpose   : Returns the ViewState for the current page.
        // Inputs    : None.
        // Outputs   : None.
        // Returns   : The StateBag containing the ViewState.
        // ------------------------------------------------------------
        public StateBag ReturnViewState()
        {
            return ViewState;
        }

        // ------------------------------------------------------------
        // Name      : PreviousPageViewState
        // Purpose   : Gets the ViewState of the previous page.
        // Inputs    : None.
        // Outputs   : None.
        // Returns   : The StateBag containing the ViewState of the previous page.
        // ------------------------------------------------------------
        private StateBag PreviousPageViewState
        {
            get
            {
                StateBag returnValue = null;
                if (Page.PreviousPage != null)
                {
                    Object objPreviousPage = (Object)PreviousPage;
                    MethodInfo objMethod = objPreviousPage.GetType().GetMethod("ReturnViewState");      //System.Reflection class
                    return (StateBag)objMethod.Invoke(objPreviousPage, null);
                }
                return returnValue;
            }
        }
    }
}