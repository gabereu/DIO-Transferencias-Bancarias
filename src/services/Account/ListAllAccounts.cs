using System.Collections.Generic;
using DIO.Bank.domain;

namespace DIO.Bank.services
{
    public class ListAllAccounts
    {
        private readonly IAccountRepository _accountRepository;
        public ListAllAccounts(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public List<Account> run() {
            return _accountRepository.FindAll();
        }
        
    }
}