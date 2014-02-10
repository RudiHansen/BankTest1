using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTest1
{
    class Program
    {
        public interface IAccount
        {
            void PayInFunds(decimal amount);
            bool WithdrawFunds(decimal amount);
            decimal GetBalance();
            void SetBalance(decimal amount);
            string RudeLetterString();
            string GetName();
            void SetName(string accountName);
        }
        public abstract class Account : IAccount
        {
            private string name; 
            private decimal balance = 0;

            public abstract string RudeLetterString();

            public virtual bool WithdrawFunds(decimal amount)
            {
                if (balance < amount)
                {
                    return false;
                }
                balance = balance - amount; return true;
            }
            public decimal GetBalance()
            {
                return balance;
            }
            public void SetBalance(decimal amount)
            {
                balance = amount;
            }
            
            public void PayInFunds(decimal amount)
            {
                balance = balance + amount;
            }
            public string GetName()
            {
                return this.name;
            }  
            public void SetName(string accountName)
            {
                this.name = accountName;
            }
        }
        public class CustomerAccount : Account
        {
            public CustomerAccount(string accountName, decimal amount)
            {
                this.SetName(accountName);
                this.SetBalance(amount);
            }
            public override string RudeLetterString()
            {
                return "You are overdrawn";
            }
        }
        public class BabyAccount : Account
        {
            public override bool WithdrawFunds(decimal amount)
            {
                if (amount > 10)
                {
                    return false;
                }
                return base.WithdrawFunds(amount);
            }
            public override string RudeLetterString()
            {
                return "Tell daddy you are overdrawn";
            }
        } 

        interface IBank 
        { 
            IAccount FindAccount (string name);
            bool StoreAccount (IAccount account);
            void PrintAccountList();
        }

        public class ArrayBank : IBank
        {
            private IAccount[] accounts;

            public ArrayBank(int bankSize)
            {
                accounts = new IAccount[bankSize];
            }

            public bool StoreAccount(IAccount account)
            {
                int position = 0;
                for (position = 0; position < accounts.Length; position++)
                {
                    if (accounts[position] == null)
                    {
                        accounts[position] = account;
                        return true;
                    }
                }
                return false;
            }
            public IAccount FindAccount(string name)
            {
                int position = 0;
                for (position = 0; position < accounts.Length; position++)
                {
                    if (accounts[position] == null)
                    {
                        continue;
                    }
                    if (accounts[position].GetName() == name)
                    {
                        return accounts[position];
                    }
                }
                return null;
            }
            public void PrintAccountList()
            {
                int position = 0;
                for (position = 0; position < accounts.Length; position++)
                {
                    if (accounts[position] == null)
                    {
                        continue;
                    }
                    Console.WriteLine("AccountName : " + accounts[position].GetName() + "  :  " + accounts[position].GetBalance());
                }
            }

        }
        static void Main(string[] args)
        {
            IBank friendlyBank = new ArrayBank(50); 
            Random rand = new Random();

            friendlyBank.StoreAccount(new CustomerAccount("Rob", (decimal) rand.Next(101) * 100));
            friendlyBank.StoreAccount(new CustomerAccount("Rudi", (decimal)rand.Next(101) * 100));
            friendlyBank.StoreAccount(new CustomerAccount("Toni", (decimal)rand.Next(101) * 100));
            friendlyBank.StoreAccount(new CustomerAccount("Per", (decimal)rand.Next(101) * 100));
            friendlyBank.PrintAccountList();
            Console.ReadKey();

        }
    }
}
