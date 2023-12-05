using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDB
{
    internal class TransactionEntity
    {


        public string TransactionID { get; set; }
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public string TransactionType { get; set; }
        public string Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
