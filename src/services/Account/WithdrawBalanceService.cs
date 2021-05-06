using DIO.Bank.domain;

namespace DIO.Bank.services
{
    public class WithdrawBalanceService
    {
        private readonly IAccountRepository _accountRepository;
        public WithdrawBalanceService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public bool run(int accountId, double value){
            var account = _accountRepository.Find(a => a.Id == accountId);

            return Account.WithdrawBalance(account, value);
        }
        public bool run(string owner, double value){
            var account = _accountRepository.Find(a => a.Owner == owner);

            return Account.WithdrawBalance(account, value);
        }
    }
}