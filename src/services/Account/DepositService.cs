using DIO.Bank.domain;

namespace DIO.Bank.services
{
    public class DepositService
    {
        private readonly IAccountRepository _accountRepository;
        public DepositService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void run(int accountId, double value){
            var account = _accountRepository.Find(a => a.Id == accountId);

            Account.Deposit(account, value);

            return;
        }
        public void run(string owner, double value){
            var account = _accountRepository.Find(a => a.Owner == owner);

            Account.Deposit(account, value);

            return;
        }
    }
}