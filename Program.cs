using DIO.Bank.infra.repositories;
using DIO.Bank.domain;
using DIO.Bank.domain.dtos;
using DIO.Bank.application;

namespace DIO.Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            IAccountRepository accountRepository = new FakeAccountRepository();

            accountRepository.Create(new CreateAccountDTO(){Owner = "Gabriel", AccountType = AccountType.PhysicalPerson});

            var menu = new Menu(accountRepository);

            menu.Start();
        }
    }
}
