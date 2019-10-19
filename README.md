# Design by Contract - assignment 04

The task was to create a simple account class with a deposit and a withdraw method. In both methods the amount to withdraw must be positive (I also excluded zero). In the withraw method the amount should throw an exception if greater than the account balance. I interpreted this as a general rule that the balance could not be negative but zero was allowed. I were to implement design by contract principles using Code Contracts in C#.

The contract for the deposit method looks like this:

	Contract.Requires<ArgumentException>(amount > 0, nameof(amount));
	Contract.Ensures(Contract.Result<double>() - amount == Contract.OldValue(Balance));
	Contract.EnsuresOnThrow<ArgumentException>(Contract.OldValue(Balance) == Balance);

The contract for the withdraw method looks like this:

	Contract.Requires<ArgumentException>(amount > 0, nameof(amount));
	Contract.Requires<ArgumentException>(amount <= Contract.OldValue(Balance), nameof(amount));
	Contract.Ensures(Contract.Result<double>() - amount == Contract.OldValue(Balance));
	Contract.EnsuresOnThrow<ArgumentException>(Contract.OldValue(Balance) == Balance);

I also have an invariant method where I check that the balance is never negative:

	[ContractInvariantMethod]
	public void AccountInvariant()
	{
		Contract.Invariant(Balance >= 0);
	}

My main method implmenting the Account looks like this:

	static void Main(string[] args)
	{
		var acc = new Account { Balance = 1 };
		acc.Deposit(3);
		acc.Withdraw(100); // should fail
	}
	
Due to lack of support for Code Contracts in more recent versions (ATTOW October 2019) of Visual Studio I was not able to run the program with the features of Code Contracts.