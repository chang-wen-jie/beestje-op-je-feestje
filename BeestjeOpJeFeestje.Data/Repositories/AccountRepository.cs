using BeestjeOpJeFeestje.Data.DbContext;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Repositories;

public class AccountRepository(BeestjeOpJeFeestjeDbContext context) : IAccountRepository
{
    private readonly BeestjeOpJeFeestjeDbContext _context = context;

    public IEnumerable<Account> GetAccounts()
    {
        return _context.Accounts;
    }
    
    public Account? GetAccount(int accountId)
    {
        var accountToRead = _context.Accounts.Find(accountId);
        return accountToRead;
    }

    public void AddAccount(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
    }

    public bool UpdateAccount(Account account)
    {
        var accountToUpdate = _context.Accounts.Find(account.Id);
        if (accountToUpdate == null) return false;
        
        _context.Entry(accountToUpdate).CurrentValues.SetValues(account);
        _context.SaveChanges();
        return true;
    }

    public bool DeleteAccount(int accountId)
    {
        var accountToDelete = _context.Accounts.Find(accountId);
        if (accountToDelete == null) return false;
        
        _context.Accounts.Remove(accountToDelete);
        _context.SaveChanges();
        return true;
    }
}