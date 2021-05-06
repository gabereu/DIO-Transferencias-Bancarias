using System.Collections.Generic;
using DIO.Bank.domain;
using DIO.Bank.domain.dtos;

namespace DIO.Bank.infra.repositories
{
    public class FakeAccountRepository : IAccountRepository
    {
        private readonly List<Account> _accounts;
        private int _nextId;
        public FakeAccountRepository()
        {
            _accounts = new List<Account>();
            _nextId = 0;
        }

        public Account Create(CreateAccountDTO createAccountDTO)
        {
            var account = new Account(
                _nextId,
                createAccountDTO.AccountType,
                createAccountDTO.Owner,
                0.0
            );

            _nextId += 1;

            _accounts.Add(account);

            return account;
        }

        public Account Find(System.Predicate<Account> match)
        {
            var account = _accounts.Find(match);
            return account;
        }

        public List<Account> FindAll()
        {
            return _accounts;
        }

        public Account Updade(Account accountUpdated)
        {
            var index = _accounts.FindIndex(a => a.Id == accountUpdated.Id);
            
            _accounts[index] = accountUpdated;

            return accountUpdated;
        }
    }
}