using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
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
        const int kChequingIndex = 0, kSavingIndex = 1;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (PreviousPage != null && PreviousPageViewState != null)
            {
                accountDAL    = new AccountDAL();

                CustomerID = PreviousPageViewState["CustomerID"].ToString();
                ViewState["CustomerID"] = CustomerID;

                accountList = accountDAL.GetAccountInfo(CustomerID);             
                checkingAmountLiteral.Text = $"${accountList[kChequingIndex].Balance}";
                savingAmountLiteral.Text = $"${accountList[1].Balance}";
                
            }
            else
            {
                CustomerID = ViewState["CustomerID"].ToString();

                accountList = accountDAL.GetAccountInfo(CustomerID);

                checkingAmountLiteral.Text = $"${accountList[kChequingIndex].Balance}";
                savingAmountLiteral.Text = $"${accountList[1].Balance}";

            }


        }


        /*
         * Function	   : TCDepositButton_Click()
         * Description : This function will be called when the client presses the button for the deposit to the their own account (chequing or saving)
         * Parameters  : object sender, EventArgs e
         * Return      : void
         */
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
                whichAccount = kChequingIndex;
            }
            else if (selectedAccountType == "saving")
            {
                whichAccount = kSavingIndex;
            }
            else
            {
                // Client did not selcet the account
                ShowMessage("Please Selcet account to deposit.");
                return;
            }

            // Start deposit
            isDesopistSucceess = accountDAL.Deposit(accountList[whichAccount].AccountID, double.Parse(depositAmount));

            // if deposit is suceeded, updated the balance to the selected account
            if (isDesopistSucceess == true)
            {
                accountList[whichAccount].Balance = accountList[whichAccount].Balance + double.Parse(depositAmount);
            }

            // update the current balance display
            if (whichAccount == kChequingIndex)
            {
                checkingAmountLiteral.Text = $"${accountList[kChequingIndex].Balance}";
            }
            else
            {
                savingAmountLiteral.Text = $"${accountList[kSavingIndex].Balance}";
            }

            // Clear the text box
            TransactionInput.Text = "";     
        }


        /*
         * Function	   : 
         * Description :
         * Parameters  : 
         * Return      :  
         */
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
                whichAccount = kChequingIndex;
                // cancle the withdraw transaction if the client trys to withdraw more money than the current balance
                if (double.Parse(depositAmount) > accountList[whichAccount].Balance) return;
            }
            else if (selectedAccountType == "saving")
            {
                whichAccount = kSavingIndex;
                // cancle the witdraw transaction if the client trys to withdraw more money than the current balance
                if (double.Parse(depositAmount) > accountList[whichAccount].Balance) return;
            }
            else
            {
                // Client did not selcet the account
                ShowMessage("Please Selcet account to withdraw.");
                return;
            }

            // Start withdrawing
            isWithdrawSucceess = accountDAL.Withdraw(accountList[whichAccount].AccountID, double.Parse(depositAmount));

            // if deposit is suceeded, updated the balance to the selected account
            if (isWithdrawSucceess == true)
            {
                accountList[whichAccount].Balance = accountList[whichAccount].Balance - double.Parse(depositAmount);
            }

            // update the current balance display
            if (whichAccount == kChequingIndex)
            {
                checkingAmountLiteral.Text = $"${accountList[kChequingIndex].Balance}";
            }
            else
            {
                savingAmountLiteral.Text = $"${accountList[kSavingIndex].Balance}";
            }

            // Clear the text box
            TransactionInput.Text = "";
        }

        public void TFDepositButton_Click(object sender, EventArgs e)
        {

            // Get the entered amount of money the client want to depoist
            string TransferAmount = TransferInput.Text;
            string AccountFrom = TSFromAccountList.SelectedValue;
            string AccountTo = TSToAccountList.SelectedValue;

            int whichAccountToWithdraw;
            int whichAccountToDeposit;
            bool isTrensferSucceess;

            // if the user
            // 1. does not enter amount of money
            // 2. enter negative value
            // 3. selects the same account for trensfer
            // return
            if (TransferAmount == "" || double.Parse(TransferAmount) < 0 || AccountFrom == AccountTo)
            {
                return;
            }


            // Check which account the user is trying to deposit into
            if (AccountFrom == "chequing")
            {
                whichAccountToWithdraw = kChequingIndex;
                whichAccountToDeposit = kSavingIndex;
                // cancle the withdraw transaction if the client trys to withdraw more money than the current balance
                if (double.Parse(TransferAmount) > accountList[whichAccountToWithdraw].Balance) return;

            }
            else if (AccountFrom == "saving")
            {
                whichAccountToWithdraw = kSavingIndex;
                whichAccountToDeposit = kChequingIndex;
                // cancle the witdraw transaction if the client trys to withdraw more money than the current balance
                if (double.Parse(TransferAmount) > accountList[whichAccountToWithdraw].Balance) return;
            }
            else
            {
                // Client did not selcet the account
                ShowMessage("Please Selcet account to withdraw.");
                return;
            }

            // start the transfer
            isTrensferSucceess = accountDAL.Transfer(accountList[whichAccountToWithdraw].AccountID,
                                                     accountList[whichAccountToDeposit].AccountID,
                                                     double.Parse(TransferAmount));

            // if deposit is suceeded, updated the balance to the selected account
            if (isTrensferSucceess == true)
            {
                accountList[whichAccountToWithdraw].Balance = accountList[whichAccountToWithdraw].Balance - double.Parse(TransferAmount);
                accountList[whichAccountToDeposit].Balance = accountList[whichAccountToDeposit].Balance + double.Parse(TransferAmount);

                checkingAmountLiteral.Text = $"${accountList[kChequingIndex].Balance}";
                savingAmountLiteral.Text = $"${accountList[kSavingIndex].Balance}";
            }

            // Clear the text box
            TransactionInput.Text = "";
        }
    

        public void SMDepositButton_Click(object sender, EventArgs e)
        {
            // Get the entered amount of money the client want to depoist
            string TransferAmount = SendMoneyInput.Text;
            string AccountFrom = SMFromAccountList.SelectedValue;
            string AccountTo = ReceiverAccountNum.Text;

            int whichAccountToWithdraw;
            bool isSendingSucceess;

            // if the user
            // 1. does not enter amount of money
            // 2. enter negative value
            // 3. selects the same account for trensfer
            // return
            if (TransferAmount == "" || double.Parse(TransferAmount) < 0 || AccountFrom == AccountTo)
            {
                return;
            }


            // Check which account the user is trying to deposit into
            if (AccountFrom == "chequing")
            {
                whichAccountToWithdraw = kChequingIndex;
                // cancle the withdraw transaction if the client trys to withdraw more money than the current balance
                if (double.Parse(TransferAmount) > accountList[whichAccountToWithdraw].Balance) return;

            }
            else if (AccountFrom == "saving")
            {
                whichAccountToWithdraw = kSavingIndex;
                // cancle the witdraw transaction if the client trys to withdraw more money than the current balance
                if (double.Parse(TransferAmount) > accountList[whichAccountToWithdraw].Balance) return;
            }
            else
            {
                // Client did not selcet the account
                ShowMessage("Please Selcet account to withdraw.");
                return;
            }

            // start the transfer
            isSendingSucceess = accountDAL.Transfer(accountList[whichAccountToWithdraw].AccountID,
                                                    int.Parse(AccountTo),
                                                    double.Parse(TransferAmount));

            // if deposit is suceeded, updated the balance to the selected account
            if (isSendingSucceess == true)
            {
                accountList[whichAccountToWithdraw].Balance = accountList[whichAccountToWithdraw].Balance - double.Parse(TransferAmount);

                if(whichAccountToWithdraw == kChequingIndex)
                {
                    checkingAmountLiteral.Text = $"${accountList[kChequingIndex].Balance}";
                }
                else if (whichAccountToWithdraw == kSavingIndex)
                {
                    savingAmountLiteral.Text = $"${accountList[kSavingIndex].Balance}";
                }
            }

            // Clear the text box
            TransactionInput.Text = "";
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