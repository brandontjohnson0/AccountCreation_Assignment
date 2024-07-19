using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountCreation_Assignment
{
    class Account //Base class for instance variables and Credit/Debit formulas.
    {
        private decimal Balance;
        private string AccountName;
        private int AccountNumber;


        public Account(decimal balance, string name, int number) //Constructor for Balance, AccountName, and AccountNumber.
        {
            SetBalance(balance);
            this.AccountName = name;
            this.AccountNumber = number;
        }
        public void SetBalance(decimal balance) //Balance setter, validating if balance is greater than 0.
        {

            if (balance < 0)
            {
                NegativeNumberException nne =
                    new NegativeNumberException();
                throw (nne);
            }

            else
            {
                Balance = balance;
            }
        }


        public decimal GetBalance() //Balance get method
        {
            return this.Balance;
        }

        public void SetAccountName(string name)
        {
            this.AccountName = name;
        }

        public string GetAccountName()
        {
            return this.AccountName;
        }

        public void SetAccountNumber(int number)
        {
            this.AccountNumber = number;
        }

        public int GetAccountNumber()
        {
            return this.AccountNumber;
        }


        public virtual void Credit(decimal cred) //Method Credit adds to current amount on account
        {

            this.Balance += cred;
        }

        public virtual bool Debit(decimal deb) //Method Debit withdraws from account
        {

            if (deb > Balance) //If-else detects if account is able to withdraw money. Returns line if withdraw exceeds amount
            {
                Console.WriteLine("Insufficient Funds");
                return false;
            }

            else
            {
                this.Balance -= deb;
                return true;
            }


        }

        public virtual void PrintAccount() //This method will return the information when called upon
        {
            Console.WriteLine("Account Name: " + this.AccountName);
            Console.WriteLine("Account Number: " + this.AccountNumber);
            Console.WriteLine("Balance: " + this.Balance.ToString("C"));
        }


    }

    class SavingsAccount : Account //Derived class inherits functionality of Account
    {
        private decimal InterestRate;

        public SavingsAccount(string name, int number, decimal balance, decimal interestRate) : base(balance, name, number)
        {
            this.SetInterestRate(interestRate); //SavingsAccount constructor, calls upon base constructor for balance, name, and number

        }

        public void SetInterestRate(decimal interestRate) //setter makes interest rate 0 if number is negative
        {
            if (interestRate < 0)
            {
                NegativeNumberException nne =
                    new NegativeNumberException();
                throw (nne);
            }
            else
            {
                InterestRate = interestRate;
            }


        }

        public decimal CalculateInterest() //Calculates interest rate
        {
            return base.GetBalance() * this.InterestRate;
        }

        public override void PrintAccount()
        {
            base.PrintAccount();
            Console.WriteLine("Interest Rate: " + this.InterestRate.ToString("P"));
        }


    }

    class CheckingAccount : Account //Derived class inherits from base class
    {
        private decimal FeeCharged; //Creates FeeCharged variable

        public CheckingAccount(string name, int number, decimal balance, decimal feeAmount) : base(balance, name, number) //Constructor for CheckingAccount, calls upon base constructor like SavingsAccount
        {
            this.SetFeeAmount(feeAmount);

        }

        public void SetFeeAmount(decimal feeAmount) //Setter operates much like interest rate, where negative is made into 0
        {
            if (feeAmount < 0)
            {
                NegativeNumberException nne =
                    new NegativeNumberException();
                throw (nne);
            }

            else
            {
                FeeCharged = feeAmount;
            }
        }

        public override void Credit(decimal reCred) //Method Redefines Credit with successful transaction, updates balance
        {
            base.Credit(reCred);
            ChargeFee();

        }

        public override bool Debit(decimal reDeb) //Method Redefines Debit with successful transaction, withdraws money 
        {
            if (base.Debit(reDeb))
            {
                ChargeFee();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ChargeFee() //Charge fee subtracts transaction fee
        {
            base.SetBalance(base.GetBalance() - this.FeeCharged);
            Console.WriteLine(this.FeeCharged.ToString("C") + "transaction fee charges. ");
        }

        public override void PrintAccount()
        {
            base.PrintAccount();
            Console.WriteLine("FeeCharged: " + this.FeeCharged.ToString("C"));
        }

    }

    class NegativeNumberException : Exception
    {
        private static readonly string msg = "Invalid Entry - Negative numbers are not permitted.";
        public NegativeNumberException() : base(msg)
        {

        }
    }

    class Application
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("|| This program was written by Brandon Johnson for CSIS 209-D01 ||");
            Console.WriteLine();

            Console.WriteLine("****************************************");
            Console.WriteLine("Create Checking Account 'C'");
            Console.WriteLine("Create Savings Account 'S'");
            Console.WriteLine("Quit Application 'Q'");
            Console.WriteLine("****************************************");
            Console.Write("Enter choice: ");
            string line = Console.ReadLine().ToLower();

            Console.WriteLine();
            Console.WriteLine("****************************************");

            while (line != "q")
            {
                if (line == "c")
                {
                    Console.Write("Enter a name for the account: ");
                    string AccountName = Console.ReadLine();

                    Console.Write("Enter an account number: ");
                    int AccountNumber = Convert.ToInt32(Console.ReadLine());


                    Console.Write("Enter an initial balance for the account: ");
                    decimal Balance = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("Enter the fee to be charged each transaction: ");
                    decimal FeeCharged = Convert.ToDecimal(Console.ReadLine());

                    Console.WriteLine("***********************************");

                    try
                    {
                        CheckingAccount userChecking = new CheckingAccount(AccountName, AccountNumber, Balance, FeeCharged);
                    }
                    catch (NegativeNumberException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                        Console.WriteLine("Please enter a non-negative value.");
                    }
                }

                else if (line == "s")
                {
                    Console.Write("Enter a name for the account: ");
                    string AccountName = Console.ReadLine();

                    Console.Write("Enter an account number: ");
                    int AccountNumber = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter an initial balance for the account: ");
                    decimal Balance = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("Enter the account's interest rate: ");
                    decimal InterestRate = Convert.ToDecimal(Console.ReadLine());

                    Console.WriteLine("****************************************");


                    try
                    {
                        SavingsAccount userSavings = new SavingsAccount(AccountName, AccountNumber, Balance, InterestRate);
                    }
                    catch (NegativeNumberException ex)
                    {

                        Console.WriteLine($"{ex.Message}");
                        Console.WriteLine("Please enter a non-negative value.");
                    }
                }

                else
                {
                    Console.WriteLine("Please input a valid choice.");
                }

                Console.WriteLine("****************************************");
                Console.WriteLine("Create Checking Account 'C'");
                Console.WriteLine("Create Savings Account 'S'");
                Console.WriteLine("Quit Application 'Q'");
                Console.WriteLine("****************************************");
                Console.Write("Enter choice: ");
                line = Console.ReadLine().ToLower();

                Console.WriteLine();
                Console.WriteLine("****************************************");
            }

            Console.Write("Enter choice: ");
            line = Console.ReadLine().ToLower();

        }
    }
}
