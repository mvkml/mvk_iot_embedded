using MariVshApp.Database;
using MariVshApp.Models;
using MariVshApp.Repositories.Interfaces;

namespace MariVshApp.Repositories.SQLite;

public class UserTypeRepository : IUserTypeRepository
{
    private readonly DatabaseService _databaseService;

    public UserTypeRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<List<UserType>> GetAllActiveAsync()
    {
        var db = await _databaseService.GetDatabaseAsync();
        return await db.Table<UserType>().Where(t => t.IsActive).ToListAsync();
    }

    public async Task<UserType?> GetByIdAsync(int typeId)
    {
        var db = await _databaseService.GetDatabaseAsync();
        return await db.Table<UserType>()
            .Where(t => t.TypeId == typeId)
            .FirstOrDefaultAsync();
    }

    public async Task<List<UserType>> GetAllAsync()
    {
        var db = await _databaseService.GetDatabaseAsync();
        return await db.Table<UserType>().ToListAsync();
    }

    public async Task<int> InsertAsync(UserType userType)
    {
        var db = await _databaseService.GetDatabaseAsync();
        return await db.InsertAsync(userType);
    }

    public async Task<int> UpdateAsync(UserType userType)
    {
        var db = await _databaseService.GetDatabaseAsync();
        return await db.UpdateAsync(userType);
    }

    public async Task<int> DeleteAsync(int typeId)
    {
        var db = await _databaseService.GetDatabaseAsync();
        return await db.DeleteAsync<UserType>(typeId);
    }
}
