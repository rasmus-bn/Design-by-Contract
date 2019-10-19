using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace DesignByContract
{
    public class Account
    {
        public Account(double balance)
        {
            Balance = balance;
        }

        private double Balance { get; set; }

        [ContractInvariantMethod]
        public void AccountInvariant()
        {
            Contract.Invariant(Balance >= 0);
        }

        public double Deposit(double amount)
        {
            Contract.Requires<ArgumentException>(amount > 0, nameof(amount));
            Contract.Ensures(Contract.Result<double>() - amount == Contract.OldValue(Balance));
            Contract.EnsuresOnThrow<ArgumentException>(Contract.OldValue(Balance) == Balance);
            if (amount <= 0) throw new ArgumentException($"{nameof(amount)} must always be positive");
            Balance += amount;
            return Balance;
        }

        public double Withdraw(double amount)
        {
            Contract.Requires<ArgumentException>(amount > 0, nameof(amount));
            Contract.Requires<ArgumentException>(amount <= Contract.OldValue(Balance), nameof(amount));
            Contract.Ensures(Contract.Result<double>() - amount == Contract.OldValue(Balance));
            Contract.EnsuresOnThrow<ArgumentException>(Contract.OldValue(Balance) == Balance);
            if (amount <= 0) throw new ArgumentException($"{nameof(amount)} must always be positive");
            if (amount > Balance) throw new ArgumentException($"{nameof(amount)} is greater than {nameof(Balance)}");
            Balance -= amount;
            return Balance;
        }
    }
}
