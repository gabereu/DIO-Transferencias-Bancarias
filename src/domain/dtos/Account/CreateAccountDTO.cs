namespace DIO.Bank.domain.dtos
{
    public class CreateAccountDTO
    {
        public string Owner {get; set;}
        public AccountType AccountType {get; set;}
    }
}