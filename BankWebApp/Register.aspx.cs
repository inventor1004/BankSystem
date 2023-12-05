using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankDB;

namespace BankSystemSQ
{
    public partial class Registration : System.Web.UI.Page
    {
        private CustomerDAL customerDAL;
        protected void Page_Load(object sender, EventArgs e)
        {
            customerDAL = new CustomerDAL();
        }

        protected void signUpButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Perform signup logic here
                string   enteredID          = ID.Text;
                string   enteredPassword    = Password.Text;
                string   enteredFirstName   = FirstName.Text;
                string   enteredLastName    = LasName.Text;
                DateTime enteredDateOfBirth = DateOfBirth.SelectedDate;
                string   enteredAddress     = Address.Text;
                string   enteredPhoneNumber = PhoneNumber.Text;

                // Set the new customer's information to the customer entity
                CustomerEntity customerEntity = new CustomerEntity();
                customerEntity.ID          = enteredID;
                customerEntity.Password    = enteredPassword;
                customerEntity.FirstName   = enteredFirstName;
                customerEntity.LastName    = enteredLastName;
                customerEntity.SetDateOfBirth(enteredDateOfBirth);
                customerEntity.Address     = enteredAddress;
                customerEntity.PhoneNumber = enteredPhoneNumber;

                // Add new row to the customer table to the BankDB
                customerDAL.AddFileData(customerEntity);

                // Go back to the first log-in page
                Server.Transfer("Default.aspx");
            }
        }
    }
}