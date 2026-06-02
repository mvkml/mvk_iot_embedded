# MariVshApp — Module Implementation Template

> **Purpose:** Reusable step-by-step guide to implement any new CRUD module.
> Proven pattern from: `Site`, `Subsite`, `ComponentGroup` modules.
> Replace all `{Entity}` placeholders with your actual entity name (e.g. Hub, Room, Component).

---

## Placeholder Key

| Placeholder         | Example (Subsite)     | Meaning                              |
|---------------------|-----------------------|--------------------------------------|
| `{Entity}`          | `Subsite`             | PascalCase entity name               |
| `{Entities}`        | `Subsites`            | Plural PascalCase                    |
| `{EntityId}`        | `SubsiteId`           | Primary key field name               |
| `{ParentId}`        | `SiteId`              | FK to parent entity (if any)         |
| `{ParentEntity}`    | `Site`                | Parent entity name                   |
| `{CategoryId}`      | `105`                 | AppImage category ID for this entity |
| `{entity}`          | `subsite`             | camelCase entity name                |
| `{entities}`        | `subsites`            | camelCase plural                     |

---

## Step 1 — Files to Create

```
marivshapp/
├── Models/
│   └── {Entity}.cs
├── Repositories/
│   ├── Interfaces/
│   │   └── I{Entity}Repository.cs
│   └── SQLite/
│       └── {Entity}Repository.cs
├── Services/
│   └── {Entity}/
│       └── {Entity}Service.cs
├── ViewModels/
│   └── {Entity}/
│       ├── Manage{Entities}ViewModel.cs     ← list page VM
│       └── Manage{Entity}ViewModel.cs       ← add/edit form VM
└── Views/
    └── {Entity}/
        ├── {Entity}ListPage.xaml + .xaml.cs ← list page
        ├── Manage{Entity}Page.xaml + .xaml.cs ← add/edit form
```

---

## Step 2 — Model

```csharp
// Models/{Entity}.cs
using SQLite;

namespace MariVshApp.Models;

public class {Entity}
{
    [PrimaryKey]                              // no AutoIncrement — user enters ID
    public int      {EntityId}        { get; set; }
    public string   Name              { get; set; } = string.Empty;
    public int      {ParentId}        { get; set; }   // 0 if no parent
    public string   Description       { get; set; } = string.Empty;
    public bool     IsActive          { get; set; } = true;
    public DateTime CreatedDate       { get; set; }
    public DateTime UpdatedDate       { get; set; }
    public int      DefaultImageId    { get; set; }
    public int      DisableImageId    { get; set; }
    public int      BackGroundImageId { get; set; }

    // Not stored in DB — populated at runtime by list ViewModel
    [Ignore] public string DefaultImageFilePath { get; set; } = string.Empty;
    [Ignore] public bool   HasDefaultImage      => !string.IsNullOrEmpty(DefaultImageFilePath);

    public override string ToString() => Name;
}
```

> **Note:** SQLite-net `CreateTableAsync` uses `CREATE TABLE IF NOT EXISTS` and auto-adds
> new columns on next run — no migration script needed when adding fields.

---

## Step 3 — Repository Interface

```csharp
// Repositories/Interfaces/I{Entity}Repository.cs
using MariVshApp.Models;

namespace MariVshApp.Repositories.Interfaces;

public interface I{Entity}Repository
{
    Task<List<{Entity}>> GetAllAsync();
    Task<List<{Entity}>> GetAllActiveAsync();
    Task<{Entity}?>      GetByIdAsync(int {entity}Id);
    Task<List<{Entity}>> GetBy{ParentId}Async(int {parentId});   // remove if no parent
    Task<List<{Entity}>> SearchAsync(string query);
    Task<bool>           NameExistsAsync(string name, int exclude{EntityId} = 0);
    Task<int>            InsertAsync({Entity} {entity});
    Task<int>            UpdateAsync({Entity} {entity});
    Task<int>            DeleteAsync(int {entity}Id);
}
```

---

## Step 4 — SQLite Repository

```csharp
// Repositories/SQLite/{Entity}Repository.cs
using MariVshApp.Database;
using MariVshApp.Models;
using MariVshApp.Repositories.Interfaces;

namespace MariVshApp.Repositories.SQLite;

public class {Entity}Repository : I{Entity}Repository
{
    private readonly DatabaseService _db;

    public {Entity}Repository(DatabaseService db) => _db = db;

    public async Task<List<{Entity}>> GetAllAsync()
    {
        var db = await _db.GetDatabaseAsync();
        return await db.Table<{Entity}>().ToListAsync();
    }

    public async Task<List<{Entity}>> GetAllActiveAsync()
    {
        var db = await _db.GetDatabaseAsync();
        return await db.Table<{Entity}>().Where(x => x.IsActive).ToListAsync();
    }

    public async Task<{Entity}?> GetByIdAsync(int {entity}Id)
    {
        var db = await _db.GetDatabaseAsync();
        return await db.Table<{Entity}>().FirstOrDefaultAsync(x => x.{EntityId} == {entity}Id);
    }

    public async Task<List<{Entity}>> GetBy{ParentId}Async(int parentId)
    {
        var db = await _db.GetDatabaseAsync();
        return await db.Table<{Entity}>().Where(x => x.{ParentId} == parentId).ToListAsync();
    }

    public async Task<List<{Entity}>> SearchAsync(string query)
    {
        var all = await GetAllAsync();
        if (string.IsNullOrWhiteSpace(query)) return all;
        return all.Where(x =>
            x.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            x.Description.Contains(query, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }

    public async Task<bool> NameExistsAsync(string name, int exclude{EntityId} = 0)
    {
        var all = await GetAllAsync();
        return all.Any(x =>
            x.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase) &&
            x.{EntityId} != exclude{EntityId});
    }

    public async Task<int> InsertAsync({Entity} {entity})
    {
        var db = await _db.GetDatabaseAsync();
        return await db.InsertAsync({entity});
    }

    public async Task<int> UpdateAsync({Entity} {entity})
    {
        var db = await _db.GetDatabaseAsync();
        return await db.UpdateAsync({entity});
    }

    public async Task<int> DeleteAsync(int {entity}Id)
    {
        var db = await _db.GetDatabaseAsync();
        return await db.DeleteAsync<{Entity}>({entity}Id);
    }
}
```

---

## Step 5 — Service

```csharp
// Services/{Entity}/{Entity}Service.cs
using MariVshApp.Repositories.Interfaces;

namespace MariVshApp.Services.{Entity};

public class {Entity}Service
{
    private readonly I{Entity}Repository _repository;

    public {Entity}Service(I{Entity}Repository repository) => _repository = repository;

    public Task<List<Models.{Entity}>> GetAllAsync()         => _repository.GetAllAsync();
    public Task<List<Models.{Entity}>> GetAllActiveAsync()   => _repository.GetAllActiveAsync();
    public Task<Models.{Entity}?>      GetByIdAsync(int id)  => _repository.GetByIdAsync(id);
    public Task<List<Models.{Entity}>> SearchAsync(string q) => _repository.SearchAsync(q);

    public async Task<(bool Success, string Message)> AddAsync(Models.{Entity} {entity})
    {
        if (string.IsNullOrWhiteSpace({entity}.Name))
            return (false, "{Entity} name is required.");

        if (await _repository.NameExistsAsync({entity}.Name))
            return (false, "A {entity} with this name already exists.");

        {entity}.CreatedDate = DateTime.Now;
        {entity}.UpdatedDate = DateTime.Now;
        await _repository.InsertAsync({entity});
        return (true, "{Entity} added successfully.");
    }

    public async Task<(bool Success, string Message)> UpdateAsync(Models.{Entity} {entity})
    {
        if (string.IsNullOrWhiteSpace({entity}.Name))
            return (false, "{Entity} name is required.");

        if (await _repository.NameExistsAsync({entity}.Name, {entity}.{EntityId}))
            return (false, "A {entity} with this name already exists.");

        {entity}.UpdatedDate = DateTime.Now;
        await _repository.UpdateAsync({entity});
        return (true, "{Entity} updated successfully.");
    }

    public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
}
```

---

## Step 6 — List ViewModel (Manage{Entities}ViewModel)

```csharp
// ViewModels/{Entity}/Manage{Entities}ViewModel.cs
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MariVshApp.Services.AppImage;
using MariVshApp.Services.{Entity};

namespace MariVshApp.ViewModels.{Entity};

public class Manage{Entities}ViewModel : INotifyPropertyChanged, IQueryAttributable
{
    private readonly {Entity}Service  _service;
    private readonly AppImageService  _appImageService;

    private string _userName   = string.Empty;
    private bool   _isAdmin;
    private string _searchText = string.Empty;

    public string UserName   { get => _userName;   set { _userName   = value; OnPropertyChanged(); } }
    public bool   IsAdmin    { get => _isAdmin;    set { _isAdmin    = value; OnPropertyChanged(); } }
    public string SearchText { get => _searchText; set { _searchText = value; OnPropertyChanged(); } }

    public ObservableCollection<Models.{Entity}> FilteredItems { get; } = new();

    public ICommand SearchCommand  { get; }
    public ICommand AddCommand     { get; }
    public ICommand EditCommand    { get; }
    public ICommand DeleteCommand  { get; }
    public ICommand BackCommand    { get; }
    public ICommand LogoutCommand  { get; }

    public Manage{Entities}ViewModel({Entity}Service service, AppImageService appImageService)
    {
        _service         = service;
        _appImageService = appImageService;

        SearchCommand = new Command(async () => await LoadAsync());

        AddCommand = new Command(async () =>
            await Shell.Current.GoToAsync(nameof(Views.{Entity}.Manage{Entity}Page)));

        EditCommand = new Command<Models.{Entity}>(async {entity} =>
        {
            var p = new Dictionary<string, object>
            {
                { "mode",              "edit" },
                { "{entity}Id",        {entity}.{EntityId}.ToString() },
                { "name",              {entity}.Name },
                { "{parentId}",        {entity}.{ParentId}.ToString() },
                { "description",       {entity}.Description },
                { "isActive",          {entity}.IsActive.ToString() },
                { "createdDate",       {entity}.CreatedDate.ToString("o") },
                { "updatedDate",       {entity}.UpdatedDate.ToString("o") },
                { "defaultImageId",    {entity}.DefaultImageId.ToString() },
                { "disableImageId",    {entity}.DisableImageId.ToString() },
                { "backGroundImageId", {entity}.BackGroundImageId.ToString() },
            };
            await Shell.Current.GoToAsync(nameof(Views.{Entity}.Manage{Entity}Page), p);
        });

        DeleteCommand = new Command<Models.{Entity}>(async {entity} =>
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Delete {Entity}",
                $"Delete \"{{{entity}.Name}}\"? This cannot be undone.",
                "Delete", "Cancel");

            if (!confirm) return;
            await _service.DeleteAsync({entity}.{EntityId});
            await LoadAsync();
        });

        BackCommand   = new Command(async () => await Shell.Current.GoToAsync(".."));
        LogoutCommand = new Command(async () => await Shell.Current.GoToAsync("//LoginPage"));
    }

    public async Task LoadAsync()
    {
        var items  = await _service.SearchAsync(SearchText);
        var images = await _appImageService.GetAllAsync();
        var map    = images.ToDictionary(i => i.ImageId, i => i.FilePath);

        FilteredItems.Clear();
        foreach (var item in items)
        {
            if (item.DefaultImageId > 0 && map.TryGetValue(item.DefaultImageId, out var fp))
                item.DefaultImageFilePath = fp;
            FilteredItems.Add(item);
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("userName", out var name))
            UserName = Uri.UnescapeDataString(name?.ToString() ?? string.Empty);

        if (query.TryGetValue("userTypeId", out var typeId) &&
            int.TryParse(typeId?.ToString(), out var id))
            IsAdmin = id == 2;

        _ = LoadAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string p = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
}
```

---

## Step 7 — Add/Edit ViewModel (Manage{Entity}ViewModel)

Key sections only — see `ManageSubsiteViewModel.cs` for full reference.

```csharp
// ViewModels/{Entity}/Manage{Entity}ViewModel.cs

// Constructor dependencies
public Manage{Entity}ViewModel(
    {Entity}Service   {entity}Service,
    {ParentEntity}Service {parentEntity}Service,   // parent picker — remove if no parent
    AppImageService   appImageService)

// Image pick commands — repeat for disable + background
PickDefaultImageCommand = new Command(async () =>
{
    var p = new Dictionary<string, object>
    {
        { "targetField",        "default" },
        { "categoryId",         "{CategoryId}" },
        { "preSelectedImageId", DefaultImageId.ToString() }
    };
    await Shell.Current.GoToAsync("SelectImagePage", p);
});

// SaveAsync validation order
// 1. {EntityId} > 0 (add mode only)
// 2. Name not empty
// 3. Parent selected (only if required)

// ApplyQueryAttributes — two entry points:
// A) Returning from SelectImagePage  → check for "selectedImageId" key first
// B) Edit mode navigation            → load parent picker + LoadImageLabelsAsync()
// C) Add mode navigation             → reset all fields
```

---

## Step 8 — List Page XAML Key Structure

```xml
<!-- Views/{Entity}/{Entity}ListPage.xaml -->
<!-- Table header columns -->
<Grid ColumnDefinitions="60,50,160,80,*,85,105,Auto" ColumnSpacing="8">
    <Label Grid.Column="0" Text="ID"          ... />
    <Label Grid.Column="1" Text="IMAGE"       ... />
    <Label Grid.Column="2" Text="NAME"        ... />
    <Label Grid.Column="3" Text="{PARENT} ID" ... />
    <Label Grid.Column="4" Text="DESCRIPTION" ... />
    <Label Grid.Column="5" Text="IS ACTIVE"   ... />
    <Label Grid.Column="6" Text="CREATED"     ... />
    <Label Grid.Column="7" Text="ACTIONS"     ... />
</Grid>

<!-- Image thumbnail in each row -->
<Border Grid.Column="1" WidthRequest="36" HeightRequest="36"
        IsVisible="{Binding HasDefaultImage}" StrokeShape="RoundRectangle 4">
    <Image Source="{Binding DefaultImageFilePath}" Aspect="AspectFill" />
</Border>
```

---

## Step 9 — Add/Edit Page XAML Key Sections

```xml
<!-- {EntityId} field — always visible, editable in Add, read-only in Edit -->
<Entry Text="{Binding {EntityId}}" IsReadOnly="{Binding IsEditMode}" Keyboard="Numeric" />

<!-- Parent picker — optional field (no asterisk) -->
<Picker Title="Select {ParentEntity}"
        ItemsSource="{Binding {ParentEntities}}"
        SelectedItem="{Binding Selected{ParentEntity}}"
        ItemDisplayBinding="{Binding Name}" />

<!-- Image picker row — repeat for Disable + Background -->
<Grid ColumnDefinitions="Auto,*,Auto" ColumnSpacing="8">
    <Border Grid.Column="0" WidthRequest="44" HeightRequest="44"
            IsVisible="{Binding HasDefaultImage}" StrokeShape="RoundRectangle 6">
        <Image Source="{Binding DefaultImageFilePath}" Aspect="AspectFill" />
    </Border>
    <Label Grid.Column="1" Text="{Binding DefaultImageLabel}" />
    <Button Grid.Column="2" Text="+" Command="{Binding PickDefaultImageCommand}"
            WidthRequest="44" HeightRequest="44" />
</Grid>

<!-- Dates — edit mode only -->
<Grid IsVisible="{Binding IsEditMode}">
    <DatePicker Date="{Binding CreatedDate}" />
    <DatePicker Date="{Binding UpdatedDate}" />
</Grid>

<!-- Buttons -->
<Button Text="Save &amp; Next" IsVisible="{Binding IsAddMode}" Command="{Binding SaveNextCommand}" />
<Button Text="Save"            Command="{Binding SaveCommand}" />
```

---

## Step 10 — Code-Behind (both pages)

```csharp
// Views/{Entity}/{Entity}ListPage.xaml.cs
public partial class {Entity}ListPage : ContentPage
{
    public {Entity}ListPage(Manage{Entities}ViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing() => ((Manage{Entities}ViewModel)BindingContext).LoadAsync();
}

// Views/{Entity}/Manage{Entity}Page.xaml.cs
public partial class Manage{Entity}Page : ContentPage
{
    public Manage{Entity}Page(Manage{Entity}ViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
```

---

## Step 11 — DI Registration (MauiProgram.cs)

```csharp
// Repository
builder.Services.AddScoped<I{Entity}Repository, {Entity}Repository>();

// Service
builder.Services.AddScoped<{Entity}Service>();

// ViewModels
builder.Services.AddTransient<Manage{Entities}ViewModel>();
builder.Services.AddTransient<Manage{Entity}ViewModel>();

// Pages
builder.Services.AddTransient<{Entity}ListPage>();
builder.Services.AddTransient<Manage{Entity}Page>();
```

Also ensure `DatabaseService` registers the table:
```csharp
await db.CreateTableAsync<{Entity}>();
```

---

## Step 12 — Shell Registration (AppShell.xaml.cs)

```csharp
Routing.RegisterRoute(nameof({Entity}ListPage),   typeof({Entity}ListPage));
Routing.RegisterRoute(nameof(Manage{Entity}Page), typeof(Manage{Entity}Page));
```

---

## Step 13 — Implementation Flowchart

```
[List Page]
    │
    ├─── Search ──► LoadAsync()
    │                 ├─ {Entity}Service.SearchAsync()
    │                 ├─ AppImageService.GetAllAsync()
    │                 └─ Set DefaultImageFilePath on each row
    │
    ├─── [+ Add] ──► GoToAsync(Manage{Entity}Page)  [ADD MODE]
    │                    │
    │                    ├─ Enter {EntityId}*  (required, numeric)
    │                    ├─ Enter Name*        (required)
    │                    ├─ Select {Parent}    (optional)
    │                    ├─ [+] Default Image ──► SelectImagePage (categoryId={CategoryId})
    │                    ├─ [+] Disable Image ──► SelectImagePage (categoryId={CategoryId})
    │                    ├─ [+] BG Image      ──► SelectImagePage (categoryId={CategoryId})
    │                    ├─ Description / IsActive
    │                    │
    │                    ├─ [Save]       ──► {Entity}Service.AddAsync() ──► GoToAsync(..)
    │                    └─ [Save&Next]  ──► {Entity}Service.AddAsync() ──► Reset form
    │
    ├─── [Edit] ──► GoToAsync(Manage{Entity}Page, params)  [EDIT MODE]
    │                    │
    │                    ├─ {EntityId} read-only
    │                    ├─ LoadSitesAsync(parentId) ──► pre-selects parent
    │                    ├─ LoadImageLabelsAsync()   ──► resolves image names + paths
    │                    ├─ Save&Next hidden
    │                    │
    │                    └─ [Save] ──► {Entity}Service.UpdateAsync() ──► GoToAsync(..)
    │
    └─── [Delete] ──► DisplayAlert confirm
                         └─ {Entity}Service.DeleteAsync() ──► LoadAsync()


SelectImagePage return path:
    ApplyQueryAttributes checks "selectedImageId" key FIRST
    └─ Sets correct field based on "targetField" (default / disable / background)
```

---

## AppImage Category IDs Reference

| Entity         | CategoryId |
|----------------|-----------|
| Project        | 102       |
| Component      | 103       |
| ComponentGroup | 104       |
| Subsite        | 105       |
| Site           | 106       |

---

## Checklist

- [ ] Model created with `[PrimaryKey]` (no AutoIncrement), 3 image IDs, `[Ignore]` FilePath + HasDefaultImage
- [ ] Repository interface + SQLite implementation
- [ ] Service with Add/Update/Delete + name-duplicate check
- [ ] List ViewModel with AppImageService, LoadAsync populates DefaultImageFilePath
- [ ] Add/Edit ViewModel with 3 pick commands (correct categoryId), LoadImageLabelsAsync, ApplyQueryAttributes handles SelectImagePage return
- [ ] List XAML with IMAGE column (36×36 thumbnail)
- [ ] Add/Edit XAML with {EntityId} field (editable add / readonly edit), parent picker, 3 image rows, date pickers (edit only)
- [ ] Code-behind wired (BindingContext = vm, OnAppearing calls LoadAsync)
- [ ] DI: Repository (Scoped), Service (Scoped), ViewModels (Transient), Pages (Transient)
- [ ] DatabaseService: `CreateTableAsync<{Entity}>()`
- [ ] AppShell: `Routing.RegisterRoute` for both pages
