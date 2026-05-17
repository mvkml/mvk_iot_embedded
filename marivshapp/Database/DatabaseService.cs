using MariVshApp.Models;
using SQLite;

namespace MariVshApp.Database;

/// <summary>
/// Provides a single SQLite database connection for the entire application.
/// Uses lazy initialization to create the database only when first accessed.
/// </summary>
public class DatabaseService
{
    private SQLiteAsyncConnection? _database;
    private readonly string _dbPath;

    public DatabaseService()
    {
        // Store the database file in the app's local data folder
        _dbPath = Path.Combine(FileSystem.AppDataDirectory, "marivshapp.db3");
    }

    /// <summary>
    /// Gets the SQLite database connection.
    /// Creates the connection and required tables if they don't exist yet.
    /// </summary>
    public async Task<SQLiteAsyncConnection> GetDatabaseAsync()
    {
        if (_database is not null)
            return _database;

        _database = new SQLiteAsyncConnection(_dbPath);

        // Enable foreign key support in SQLite
        await _database.ExecuteAsync("PRAGMA foreign_keys = ON;");

        // Drop and recreate tables to apply schema changes and clear old data
        await _database.DropTableAsync<UserAccount>();
        await _database.DropTableAsync<UserType>();
        await _database.CreateTableAsync<UserType>();
        await _database.CreateTableAsync<UserAccount>();

        // Seed default user types if table is empty
        var count = await _database.Table<UserType>().CountAsync();
        if (count == 0)
        {
            var now = DateTime.Now;
            await _database.InsertAllAsync(new[]
            {
                new UserType { TypeId = 1, TypeName = "User", Description = "Default user with basic access rights", IsActive = true, CreatedDateTime = now, UpdatedDateTime = now },
                new UserType { TypeId = 2, TypeName = "Admin", Description = "Administrative user with full access", IsActive = true, CreatedDateTime = now, UpdatedDateTime = now }
            });
        }

        return _database;
    }

    /// <summary>
    /// Gets all active user types from the database.
    /// </summary>
    public async Task<List<UserType>> GetActiveUserTypesAsync()
    {
        var db = await GetDatabaseAsync();
        return await db.Table<UserType>().Where(t => t.IsActive).ToListAsync();
    }
}
