using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignByContract
{
    class Program
    {
        static void Main(string[] args)
        {
            var acc = new Account(1);
            acc.Deposit(3);
            acc.Withdraw(100); // should fail
        }
    }
}
