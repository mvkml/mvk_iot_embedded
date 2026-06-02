using MariVshApp.Models;
using SQLite;

namespace MariVshApp.Database;

public class DatabaseService
{
    private SQLiteAsyncConnection? _database;
    private readonly string _dbPath;

    public DatabaseService()
    {
        var dataDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "MariVshApp");
        Directory.CreateDirectory(dataDir);
        _dbPath = Path.Combine(dataDir, "marivshapp.db3");

        // Initialize DB immediately on app start — no navigation required
        Task.Run(async () => await GetDatabaseAsync());
    }

    public async Task<SQLiteAsyncConnection> GetDatabaseAsync()
    {
        if (_database is not null)
            return _database;

        _database = new SQLiteAsyncConnection(_dbPath);

        await _database.ExecuteAsync("PRAGMA foreign_keys = ON;");

        await _database.CreateTableAsync<UserType>();
        await _database.CreateTableAsync<UserAccount>();
        await _database.CreateTableAsync<ManageItem>();
        await _database.CreateTableAsync<Project>();
        await _database.CreateTableAsync<AppImage>();
        await _database.CreateTableAsync<Category>();
        await _database.CreateTableAsync<ComponentType>();
        await _database.CreateTableAsync<Site>();
        await _database.CreateTableAsync<Subsite>();
        await _database.CreateTableAsync<ComponentGroup>();
        await _database.CreateTableAsync<Component>();
        await _database.CreateTableAsync<ManageComponent>();
        await _database.CreateTableAsync<MasterType>();
        await _database.CreateTableAsync<MasterDetails>();
        await _database.CreateTableAsync<Hub>();

        // Migrate: add NavigateUrl column to ManageItem if it does not yet exist
        try { await _database.ExecuteAsync("ALTER TABLE ManageItem ADD COLUMN NavigateUrl TEXT NOT NULL DEFAULT ''"); }
        catch { /* column already exists — safe to ignore */ }

        // Migrate: add ParentId column to Category if it does not yet exist
        try { await _database.ExecuteAsync("ALTER TABLE Category ADD COLUMN ParentId INTEGER"); }
        catch { /* column already exists — safe to ignore */ }

        // Migrate: add CategoryId column to AppImage if it does not yet exist
        try { await _database.ExecuteAsync("ALTER TABLE AppImage ADD COLUMN CategoryId INTEGER"); }
        catch { /* column already exists — safe to ignore */ }

        // Migrate: add image ID columns to ComponentGroup if they do not yet exist
        try { await _database.ExecuteAsync("ALTER TABLE ComponentGroup ADD COLUMN DefaultImageId INTEGER NOT NULL DEFAULT 0"); }
        catch { /* column already exists — safe to ignore */ }
        try { await _database.ExecuteAsync("ALTER TABLE ComponentGroup ADD COLUMN DisableImageId INTEGER NOT NULL DEFAULT 0"); }
        catch { /* column already exists — safe to ignore */ }
        try { await _database.ExecuteAsync("ALTER TABLE ComponentGroup ADD COLUMN BackGroundImageId INTEGER NOT NULL DEFAULT 0"); }
        catch { /* column already exists — safe to ignore */ }

        // Seed Category manage item if not yet present
        var categoryManageItem = await _database.Table<ManageItem>()
            .FirstOrDefaultAsync(m => m.NavigateUrl == "ManageCategoriesPage");
        if (categoryManageItem is null)
        {
            var now = DateTime.Now;
            await _database.InsertAsync(new ManageItem
            {
                ItemId      = 3,
                Name        = "Category",
                Description = "Manage categories",
                NavigateUrl = "ManageCategoriesPage",
                ImageUrl    = string.Empty,
                IsActive    = true,
                CreatedDate = now,
                UpdatedDate = now,
            });
        }

        var componentTypeManageItem = await _database.Table<ManageItem>()
            .FirstOrDefaultAsync(m => m.NavigateUrl == "ComponentTypePage");
        if (componentTypeManageItem is null)
        {
            var now = DateTime.Now;
            await _database.InsertAsync(new ManageItem
            {
                ItemId      = 4,
                Name        = "Component Type",
                Description = "Manage component types",
                NavigateUrl = "ComponentTypePage",
                ImageUrl    = string.Empty,
                IsActive    = true,
                CreatedDate = now,
                UpdatedDate = now,
            });
        }

        // Site manage item
        var siteManageItem = await _database.Table<ManageItem>()
            .FirstOrDefaultAsync(m => m.NavigateUrl == "SiteListPage");
        if (siteManageItem is null)
        {
            var now = DateTime.Now;
            await _database.InsertAsync(new ManageItem
            {
                ItemId      = 5,
                Name        = "Site",
                Description = "Manage sites",
                NavigateUrl = "SiteListPage",
                ImageUrl    = string.Empty,
                IsActive    = true,
                CreatedDate = now,
                UpdatedDate = now,
            });
        }

        // Subsite manage item
        var subsiteManageItem = await _database.Table<ManageItem>()
            .FirstOrDefaultAsync(m => m.NavigateUrl == "SubsiteListPage");
        if (subsiteManageItem is null)
        {
            var now = DateTime.Now;
            await _database.InsertAsync(new ManageItem
            {
                ItemId      = 6,
                Name        = "Subsite",
                Description = "Manage subsites",
                NavigateUrl = "SubsiteListPage",
                ImageUrl    = string.Empty,
                IsActive    = true,
                CreatedDate = now,
                UpdatedDate = now,
            });
        }

        // ComponentGroup manage item
        var componentGroupManageItem = await _database.Table<ManageItem>()
            .FirstOrDefaultAsync(m => m.NavigateUrl == "ComponentGroupListPage");
        if (componentGroupManageItem is null)
        {
            var now = DateTime.Now;
            await _database.InsertAsync(new ManageItem
            {
                ItemId      = 7,
                Name        = "Component Group",
                Description = "Manage component groups",
                NavigateUrl = "ComponentGroupListPage",
                ImageUrl    = string.Empty,
                IsActive    = true,
                CreatedDate = now,
                UpdatedDate = now,
            });
        }

        // Component manage item
        var componentManageItem = await _database.Table<ManageItem>()
            .FirstOrDefaultAsync(m => m.NavigateUrl == "ComponentListPage");
        if (componentManageItem is null)
        {
            var now = DateTime.Now;
            await _database.InsertAsync(new ManageItem
            {
                ItemId      = 8,
                Name        = "Component",
                Description = "Manage components",
                NavigateUrl = "ComponentListPage",
                ImageUrl    = string.Empty,
                IsActive    = true,
                CreatedDate = now,
                UpdatedDate = now,
            });
        }

        // MasterType manage item
        var masterTypeManageItem = await _database.Table<ManageItem>()
            .FirstOrDefaultAsync(m => m.NavigateUrl == "MasterTypePage");
        if (masterTypeManageItem is null)
        {
            var now = DateTime.Now;
            await _database.InsertAsync(new ManageItem
            {
                ItemId      = 9,
                Name        = "Master Type",
                Description = "Manage master configuration types",
                NavigateUrl = "MasterTypePage",
                ImageUrl    = string.Empty,
                IsActive    = true,
                CreatedDate = now,
                UpdatedDate = now,
            });
        }

        // MasterDetails manage item
        var masterDetailsManageItem = await _database.Table<ManageItem>()
            .FirstOrDefaultAsync(m => m.NavigateUrl == "MasterDetailsPage");
        if (masterDetailsManageItem is null)
        {
            var now = DateTime.Now;
            await _database.InsertAsync(new ManageItem
            {
                ItemId      = 10,
                Name        = "Master Details",
                Description = "Manage master configuration key-value records",
                NavigateUrl = "MasterDetailsPage",
                ImageUrl    = string.Empty,
                IsActive    = true,
                CreatedDate = now,
                UpdatedDate = now,
            });
        }

        // Seed MasterType if not yet present
        var masterTypeCount = await _database.Table<MasterType>().CountAsync();
        if (masterTypeCount == 0)
        {
            var now = DateTime.Now;
            await _database.InsertAllAsync(new[]
            {
                new MasterType { TypeId = 1, Name = "App Setting",     Description = "General application configuration values",  IsActive = true, CreatedDate = now, UpdatedDate = now },
                new MasterType { TypeId = 2, Name = "Network Config",  Description = "MQTT broker, API endpoints, connectivity",  IsActive = true, CreatedDate = now, UpdatedDate = now },
                new MasterType { TypeId = 3, Name = "Hardware Default",Description = "Default values for ESP32 hubs/components", IsActive = true, CreatedDate = now, UpdatedDate = now },
                new MasterType { TypeId = 4, Name = "System Parameter",Description = "Internal system-level parameters",          IsActive = true, CreatedDate = now, UpdatedDate = now },
            });
        }

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

        var componentTypeCount = await _database.Table<ComponentType>().CountAsync();
        if (componentTypeCount == 0)
        {
            var now = DateTime.Now;
            await _database.InsertAllAsync(new[]
            {
                new ComponentType { TypeId = 1, Name = "Switch",   Description = "On/Off switch component",     IsActive = true, CreatedDate = now, UpdatedDate = now },
                new ComponentType { TypeId = 2, Name = "Monitor",  Description = "Sensor/monitor component",    IsActive = true, CreatedDate = now, UpdatedDate = now },
                new ComponentType { TypeId = 3, Name = "InfraRed", Description = "Infrared control component",  IsActive = true, CreatedDate = now, UpdatedDate = now },
                new ComponentType { TypeId = 4, Name = "Alarm",    Description = "Alarm/buzzer component",      IsActive = true, CreatedDate = now, UpdatedDate = now }
            });
        }

        return _database;
    }

    public async Task<List<UserType>> GetActiveUserTypesAsync()
    {
        var db = await GetDatabaseAsync();
        return await db.Table<UserType>().Where(t => t.IsActive).ToListAsync();
    }
}
