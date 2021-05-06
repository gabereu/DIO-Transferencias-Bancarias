using DIO.Bank.domain;
using DIO.Bank.domain.dtos;

namespace DIO.Bank.services
{
    public class CreateAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public CreateAccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public Account run(CreateAccountDTO createAccountDTO){

            var existAccount = _accountRepository.Find(a => a.Owner == createAccountDTO.Owner);

            if(existAccount != null){
                throw new System.Exception("Owner already exist");
            }

            var account = _accountRepository.Create(createAccountDTO);

            return account;
        }
    }
}