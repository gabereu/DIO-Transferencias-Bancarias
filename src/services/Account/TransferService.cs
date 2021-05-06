using DIO.Bank.domain;

namespace DIO.Bank.services
{
    public class TransferService
    {
        private readonly IAccountRepository _accountRepository;
        public TransferService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public bool run(int fromAccountId, int toAccountId, double value){
            var fromAccount = _accountRepository.Find(a => a.Id == fromAccountId);
            var toAccount = _accountRepository.Find(a => a.Id == toAccountId);

            return Account.Transfer(value, from: fromAccount, to: toAccount);
        }
        public bool run(string fromOwner, string toOwner, double value){
            var fromAccount = _accountRepository.Find(a => a.Owner == fromOwner);
            var toAccount = _accountRepository.Find(a => a.Owner == toOwner);

            return Account.Transfer(value, from: fromAccount, to: toAccount);
        }
    }
}