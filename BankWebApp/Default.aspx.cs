using System;
using System.Web;
using System.Web.UI;
using BankDB;

namespace BankSystemSQ
{
    public partial class _Default : System.Web.UI.Page
    {
        private  CustomerDAL customerDAL;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize DAL with your database connection string
            customerDAL = new CustomerDAL();
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            // Get the entered username and password
            string ID       = userID.Text.Trim();
            string password = userPW.Text.Trim();

            // Check whether the customer ID and PW are match and exist in the customer table
            string customerID = customerDAL.LogInValidation(ID, password);

            if (customerID != string.Empty)
            {
                ViewState["CustomerID"] = customerID;

                // Redirect to the main page upon successful login
                Server.Transfer("Mainpage.aspx");
            }
            
            
        }

        protected void signupButton_Click(object sender, EventArgs e)
        {
            // Redirect to the registration page
            Response.Redirect("Register.aspx");
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
    }
}
