using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class Student : Client
    {
        private const int initialInterest = 2;

        public Student(string name, string id, double income) 
            : base(name, id, initialInterest, income)
        {
        }

        public override void IncreaseInterest()
        {
            Interest++;
        }
    }
}
