using DIO.Bank.domain;

namespace DIO.Bank.services
{
    public class FindAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public FindAccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Account run(string owner) {
            return _accountRepository.Find(a => a.Owner == owner);
        }
        public Account run(int Id) {
            return _accountRepository.Find(a => a.Id == Id);
        }
        
    }
}