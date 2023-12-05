using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankDB
{
    public class AccountEntity
    {

        public int AccountID { get; set; }
        public int CustomerID {  get; set; }
        public double Balance { get; set; }
        public string AccountType { get; set; }

    }
}