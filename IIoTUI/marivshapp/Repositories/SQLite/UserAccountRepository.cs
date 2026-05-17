using MariVshApp.Database;
using MariVshApp.Models;
using MariVshApp.Repositories.Interfaces;

namespace MariVshApp.Repositories.SQLite;

public class UserAccountRepository : IUserAccountRepository
{
    private readonly DatabaseService _databaseService;

    public UserAccountRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<int> InsertAsync(UserAccount user)
    {
        var db = await _databaseService.GetDatabaseAsync();
        return await db.InsertAsync(user);
    }

    public async Task<UserAccount?> GetByUserIdAsync(string userId)
    {
        var db = await _databaseService.GetDatabaseAsync();
        return await db.Table<UserAccount>()
            .Where(u => u.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task<UserAccount?> GetByPinAsync(string pin)
    {
        var db = await _databaseService.GetDatabaseAsync();
        return await db.Table<UserAccount>()
            .Where(u => u.Pin == pin)
            .FirstOrDefaultAsync();
    }

    public async Task<List<UserAccount>> GetAllAsync()
    {
        var db = await _databaseService.GetDatabaseAsync();
        return await db.Table<UserAccount>().ToListAsync();
    }

    public async Task<UserAccount?> GetByIdAsync(int id)
    {
        var db = await _databaseService.GetDatabaseAsync();
        return await db.Table<UserAccount>()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> UpdateAsync(UserAccount user)
    {
        var db = await _databaseService.GetDatabaseAsync();
        return await db.UpdateAsync(user);
    }

    public async Task<int> DeleteAsync(int id)
    {
        var db = await _databaseService.GetDatabaseAsync();
        return await db.DeleteAsync<UserAccount>(id);
    }
}
