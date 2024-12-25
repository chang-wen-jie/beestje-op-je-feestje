using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Interfaces;

public interface IAccountRepository
{
    public IEnumerable<Account> GetAccounts();
    public Account? GetAccount(int accountId);
    public void AddAccount(Account account);
    public bool UpdateAccount(Account account);
    public bool DeleteAccount(int accountId);
}