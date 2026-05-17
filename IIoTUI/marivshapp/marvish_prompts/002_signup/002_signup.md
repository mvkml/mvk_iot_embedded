# 002 - Sign Up Page

## Prompt

Create a Sign-Up page for the .NET MAUI application under the UserAccount section.

### Sign-Up Page Requirements
- Phone Number
- First Name
- Last Name
- Email ID
- Date of Birth (DatePicker)
- Gender — Male or Female (Picker)
- Sign Up button
- Message label below the button for success or error messages

### Behavior
- When the user clicks the Sign Up button:
  - Validate that Phone Number is not empty
  - Validate that First Name is not empty
  - Validate that Last Name is not empty
  - Validate that Email ID is not empty
  - Validate that Gender is selected
  - If any value is empty, show a validation message
  - If all values are entered, show a success message like "Sign-up successful!"
- For now, no database integration — keep logic simple and local

### Architecture / Code Style
- Follow the same MVVM pattern as Login page
- Create:
  - `Views/UserAccount/SignUpPage.xaml`
  - `Views/UserAccount/SignUpPage.xaml.cs`
  - `ViewModels/UserAccount/SignUpViewModel.cs`
- Use data binding for all fields and SignUpCommand
- Keep code readable and beginner-friendly

### Navigation
- Login page has a "Don't have an account? Sign Up" link at the bottom
- Tapping it navigates to the Sign-Up page using Shell navigation
- Route registered in `AppShell.xaml.cs`

---

## Files Created / Modified

| File | Action | Description |
|------|--------|-------------|
| `Views/UserAccount/SignUpPage.xaml` | Created | Sign-Up UI with all fields: phone, name, email, DOB, gender, button, message |
| `Views/UserAccount/SignUpPage.xaml.cs` | Created | Minimal code-behind (logic stays in ViewModel) |
| `ViewModels/UserAccount/SignUpViewModel.cs` | Created | MVVM ViewModel with all field properties, GenderOptions list, and SignUpCommand |
| `Views/UserAccount/LoginPage.xaml` | Modified | Added "Don't have an account? Sign Up" link at bottom |
| `Views/UserAccount/LoginPage.xaml.cs` | Modified | Added `OnSignUpTapped` handler for navigation to SignUpPage |
| `AppShell.xaml.cs` | Modified | Registered `SignUpPage` route |

## Key Design Decisions
- **MVVM pattern** — All fields bound to ViewModel properties
- **Compiled bindings** — `x:DataType` set on ScrollView for performance
- **Gender picker** — Uses `Picker` with `ItemsSource` bound to `GenderOptions` list (Male, Female)
- **Date of Birth** — Uses `DatePicker` control
- **Phone number** — Entry with `Keyboard="Telephone"`
- **Email** — Entry with `Keyboard="Email"`
- **Validation** — Each field validated individually with specific error messages
- **Message color** — Red for errors, Green for success
- **Consistent layout** — Same styling as Login page (VerticalStackLayout, 30px padding, 16px spacing, max 400px width)

## Build & Run
```
dotnet build MariVshApp.csproj -f net9.0-windows10.0.19041.0
dotnet run --project MariVshApp.csproj -f net9.0-windows10.0.19041.0
```

## Status
- Build: Succeeded (0 warnings)
- Run: Tested successfully on Windows

---

## Database Setup (SQLite)

### Prompt
Create a Database folder with SQLite integration for the application.

### Files Created / Modified

| File | Action | Description |
|------|--------|-------------|
| `Database/DatabaseService.cs` | Created | SQLite connection manager (singleton), auto-creates UserAccount table |
| `Models/UserAccount.cs` | Created | User model with Id, PhoneNumber, FirstName, LastName, Email, DateOfBirth, Gender, Username, Password |
| `MauiProgram.cs` | Modified | Registered `DatabaseService` as singleton |
| `MariVshApp.csproj` | Modified | Added `sqlite-net-pcl` v1.9.172 NuGet package |

### Key Details
- Database file: `marivshapp.db3` stored in `FileSystem.AppDataDirectory`
- `DatabaseService` is a singleton registered via dependency injection
- `UserAccount` table created automatically on first database access
- Not yet wired to Login/SignUp — ready for next step

### Status
- Build: Succeeded (0 warnings)
- Run: Tested successfully on Windows

---

## Update — Added User ID and Password to Sign-Up

### Prompt
Add User ID, Password, and Confirm Password fields to the Sign-Up page. User ID is the login username. Drop and recreate the UserAccount table to clear old data and apply the schema change (renamed `Username` → `UserId`).

### Changes Made

| File | Action | Description |
|------|--------|-------------|
| `Models/UserAccount.cs` | Modified | Renamed `Username` → `UserId` |
| `Database/DatabaseService.cs` | Modified | Added `DropTableAsync` before `CreateTableAsync` to clear old data and apply schema change |
| `ViewModels/UserAccount/SignUpViewModel.cs` | Modified | Added `UserId`, `Password`, `ConfirmPassword` properties + validation (empty checks, password match, duplicate UserId check) |
| `Views/UserAccount/SignUpPage.xaml` | Modified | Added User ID, Password, and Confirm Password entry fields (password fields masked with `IsPassword="True"`) |

### Sign-Up Validation Flow
1. Validate all existing fields (phone, name, email, gender)
2. Validate **User ID** is not empty
3. Validate **Password** is not empty
4. Validate **Confirm Password** matches Password
5. Check for **duplicate User ID** in database
6. Save to SQLite and show success alert

### Status
- Build: Succeeded (0 warnings)
- Run: Tested successfully on Windows
