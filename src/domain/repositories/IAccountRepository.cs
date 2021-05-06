using System.Collections.Generic;
using DIO.Bank.domain.dtos;

namespace DIO.Bank.domain
{
    public interface IAccountRepository
    {
        public Account Create(CreateAccountDTO createAccountDTO);
        public Account Updade(Account accountUpdated);
        public Account Find(System.Predicate<Account> match);
        public List<Account> FindAll();
    }
}