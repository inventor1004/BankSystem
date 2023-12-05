using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankDB
{
    public class AccountEntity
    {

        public string AccountID { get; set; }
        public string CustomerID {  get; set; }
        public string Balance { get; set; }
        public string AccountType { get; set; }

    }
}