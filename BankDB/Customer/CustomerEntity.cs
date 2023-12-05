using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDB
{
    public class CustomerEntity
    {
        public int CustomerID { get; set; }
        public string ID { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DateOfBirth;
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public void SetDateOfBirth(DateTime DateOfBirth)
        {

            // type casting dateOfBirth to string & parse only the year-month-date part
            string toString = DateOfBirth.ToString();
            string[] dateOfBirth = toString.Split(' ');
            const int kDateOfBirthIndex = 0;            // index 1 = time, index 2 = AM or PM

            this.DateOfBirth = dateOfBirth[kDateOfBirthIndex];  
        }

        public string GetDateOfBirth()
        {
            return this.DateOfBirth;
        }
    }
}
