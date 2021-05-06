using System;
using DIO.Bank.domain;
using DIO.Bank.domain.dtos;
using DIO.Bank.services;

namespace DIO.Bank.application
{
    public class Menu
    {
        private ListAllAccounts _listAllAccounts;
        private TransferService _transferService;
        private WithdrawBalanceService _withdrawBalanceService;
        private CreateAccountService _createAccountService;
        private DepositService _depositService;
        private FindAccountService _findAccountService;

        public Menu(IAccountRepository accountRepository)
        {
            _createAccountService = new CreateAccountService(accountRepository);
            _depositService = new DepositService(accountRepository);
            _findAccountService = new FindAccountService(accountRepository);
            _listAllAccounts = new ListAllAccounts(accountRepository);
            _transferService = new TransferService(accountRepository);
            _withdrawBalanceService = new WithdrawBalanceService(accountRepository);
            
        }

        public void Start(){
            Console.WriteLine("DIO Bank ao seu dispor!");
            while (true)
            {
                var option = GetOption();
                switch (option.ToUpper())
                {
                    case "1":
                        CreateAccount();
                        break;
                    case "2":
                        Console.Clear();
                        ShowAllAccounts();
                        break;
                    case "3":
                        ShowAnAccount();
                        break;
                    case "4":
                        Deposit();
                        break;
                    case "5":
                        Transfer();
                        break;
                    case "6":
                        WithdrawBalance();
                        break;
                    case "X":
                        Console.WriteLine("\nObrigado por utilizar nossos serviços!\n");
                        return;
                    default:
                        Console.WriteLine("\nOps! Opção inválida.");
                        break;
                }
                Console.WriteLine("\nAperte uma tecla para continuar");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public string GetLine(string description = null){
            if(description != null){
                Console.WriteLine(description);
            }
            Console.Write("> ");
            return Console.ReadLine();
        }

        public string GetOption(){
            Console.WriteLine("Escolha uma opção: ");
            Console.WriteLine("1 - Criar Conta");
            Console.WriteLine("2 - Listar Contas");
            Console.WriteLine("3 - Visualizar conta");
            Console.WriteLine("4 - Depositar");
            Console.WriteLine("5 - Transferir");
            Console.WriteLine("6 - Sacar");
            Console.WriteLine("X - Sair");
            var option = GetLine();
            return option;
        }

        public void ShowAllAccounts(){
            Console.WriteLine("Número - Nome");
            var accounts = _listAllAccounts.run();
            foreach(Account account in accounts){
                Console.WriteLine(account.ToStringList());
            }
        }
        public void ShowAnAccount(){
            var length = _listAllAccounts.run().Count;
            if(length > 0){
                Console.Clear();
                var account = GetAnAccount();
                Console.WriteLine(account.ToDescription());
            }
        }

        public void CreateAccount(){
            String owner = GetLine("Digite um nome");
            String accountTypeString = GetLine("Selecione o tipo da conta: \n1 - PF \n2 - PJ");
            AccountType accountType;
            while (true)
            {
                var parsed = Enum.TryParse<AccountType>(accountTypeString, result: out accountType);
                if(parsed && Enum.IsDefined<AccountType>(accountType)){
                    break;
                }
                accountTypeString = GetLine("Número incorreto. Digite um número válido");
            }
            var account = new CreateAccountDTO(){
                Owner = owner,
                AccountType = accountType,
            };
            
            try
            {
                _createAccountService.run(account);
                Console.WriteLine("Conta criada com sucesso!");
            }
            catch (System.Exception)
            {
                Console.WriteLine("Não foi possível criar!");
                Console.WriteLine("Nome já utilizado em outra conta!");
            }
        }

        private Account GetAnAccount(){
            ShowAllAccounts();
            int accountId;
            Account account;
            while (true)
            {
                var accountIdOrOwner = GetLine("Digite o número ou nome da conta");
                if(int.TryParse(accountIdOrOwner, out accountId)){
                    account = _findAccountService.run(accountId);
                } else {
                    account = _findAccountService.run(accountIdOrOwner);
                }

                if(account != null){
                    break;
                }

                Console.WriteLine("Conta não encontrada!");
            }

            return account;
        }

        private int CountAccounts(){
            var count = _listAllAccounts.run().Count;

            return count;
        }

        private Double GetValue(string command = "Digite um valor") {
            var input = GetLine(command);
            Double value;
            while (true)
            {
                if(Double.TryParse(input, out value) && value > 0){
                    return value;
                }

                input = GetLine("Valor inválido! Digite novamente!");
            }
        }

        public void Transfer(){
            var count = CountAccounts();
            if(count < 2){
                Console.WriteLine("Necessário 2 contas no mínimo");
                return;
            }

            Console.Clear();
            Console.WriteLine("De qual conta deseja Transferir?");
            var accountFrom = GetAnAccount();

            Console.Clear();
            Console.WriteLine("Para qual conta deseja Transferir?");
            var accountTo = GetAnAccount();

            Console.Clear();
            var value = GetValue("Digite quanto deseja transeferir: ");

            var concluded = _transferService.run(accountFrom.Id, accountTo.Id, value);

            if(concluded){
                Console.WriteLine("Transferência realizada!");
            } else {
                Console.WriteLine("Saldo insuficiente!");
            }

            
        }

        public void Deposit(){
            var count = CountAccounts();
            if(count < 1){
                Console.WriteLine("Não há contas cadastradas");
                return;
            }

            Console.Clear();
            var value = GetValue("Digite quanto deseja depositar: ");

            Console.Clear();
            Console.WriteLine("Em qual conta deseja depositar?");
            var account = GetAnAccount();

            _depositService.run(account.Id, value);

            Console.WriteLine("Depósito realizado!");
        }

        public void WithdrawBalance(){
            var count = CountAccounts();
            if(count < 1){
                Console.WriteLine("Não há contas cadastradas");
                return;
            }
            
            Console.Clear();
            Console.WriteLine("De qual conta deseja sacar?");
            var account = GetAnAccount();

            Console.Clear();
            var value = GetValue("Digite quanto deseja sacar: ");

            var concluded = _withdrawBalanceService.run(account.Id, value);

            if(concluded){
                Console.WriteLine("Dinheiro sacado!");
            } else {
                Console.WriteLine("Saldo insuficiente!");
            }

        }

    }
}