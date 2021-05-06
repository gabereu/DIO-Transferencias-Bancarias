using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DIO.Bank.domain
{
    public class Account
    {
        public int Id {get; private set;}
        public AccountType AccountType {get; private set;}

        public string Owner {get; private set;}

        public double Balance {get; private set;}

        public Account(int id, AccountType accountType, string owner, double balance)
        {
            this.Id = id;
            this.AccountType = accountType;
            this.Owner = owner;
            this.Balance = balance;
        }

        public static bool WithdrawBalance(Account from, double value){
            if(from.Balance < value){
                return false;
            }

            from.Balance -= value;

            return true;
        }

        public static void Deposit(Account account, double value){
            account.Balance += value;
        }

        public static bool Transfer(double value, Account from, Account to){
            if(WithdrawBalance(from, value)){
                Deposit(to, value);
                
                return true;
            }

            return false;
        }

        public string ToStringList(){
            string stringList = $"{this.Id} - {this.Owner}";
            return stringList;
        }

        public string ToDescription(){

            string description = $"{this.Id} - {this.TranslateAccountType()} \n{this.Owner} \nSaldo: {this.Balance}";
            return description;
        }

        private string TranslateAccountType(){
            var translate = new Dictionary<String, String>();
            translate.Add("PhysicalPerson", "Pessoa Física");
            translate.Add("LegalPerson", "Pessoa Jurídica");

            return translate[this.AccountType.ToString()];
        }
    }
}