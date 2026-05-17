# 001 - Login Page

## Prompt

Create a .NET MAUI application that will initially run for Windows.

### Requirement
- Build the first screen as a Login Page.
- This is the starting point of the application. The project will be enhanced step by step later, so keep the structure clean, modular, and easy to extend.

### Login Page Requirements
- Show a title at the top: "Login"
- Add a Username entry field
- Add a Password entry field
- Password field should hide the typed characters
- Add a Login button
- Add a message label below the button for success or error messages

### Behavior
- When the user clicks the Login button:
  - Validate that Username is not empty
  - Validate that Password is not empty
  - If either value is empty, show a validation message
  - If both values are entered, show a success message like "Login successful"
- For now, no database or API integration is required
- Keep the login logic simple and local for now

### Architecture / Code Style
- Use clean .NET MAUI structure
- Separate UI and logic as much as possible
- Prefer MVVM pattern so the application can be expanded later
- Create:
  - LoginPage.xaml
  - LoginPage.xaml.cs
  - LoginViewModel.cs
- Use data binding for Username, Password, Message, and LoginCommand
- Keep code readable and beginner-friendly
- Add comments where needed for understanding

### UI Requirements
- Keep the design simple and professional
- Center the login form on the page
- Add proper spacing, padding, and alignment
- Use standard MAUI controls only

### Output
- Generate all required code files for the initial working login page
- Make sure the app opens with the Login Page as the startup page

---

## Files Created / Modified

| File | Action | Description |
|------|--------|-------------|
| `Views/UserAccount/LoginPage.xaml` | Created | Login UI with title, username/password entries, login button, message label, and "Sign Up" link |
| `Views/UserAccount/LoginPage.xaml.cs` | Created | Minimal code-behind with navigation to SignUpPage |
| `ViewModels/UserAccount/LoginViewModel.cs` | Created | MVVM ViewModel with Username, Password, Message properties and LoginCommand |
| `AppShell.xaml` | Modified | Updated to start with LoginPage, namespace set to Views.UserAccount |
| `AppShell.xaml.cs` | Modified | Registered SignUpPage route for navigation |

## Key Design Decisions
- **MVVM pattern** — UI and logic are separated; all binding (Username, Password, Message, LoginCommand) is in the ViewModel
- **Compiled bindings** — `x:DataType` is set on ScrollView for better runtime performance
- **Validation** — Empty username shows "Please enter a username.", empty password shows "Please enter a password.", valid input shows green "Login successful!"
- **Message color** — Red for errors, Green for success (bound via `MessageColor` property)
- **Clean centered layout** — `VerticalStackLayout` centered on page with 30px padding, 20px spacing, max 400px width
- **Extensible structure** — `Views/` and `ViewModels/` folders for easy addition of new pages later

## Build & Run
```
dotnet build MariVshApp.csproj -f net9.0-windows10.0.19041.0
dotnet run --project MariVshApp.csproj -f net9.0-windows10.0.19041.0
```

## Status
- Build: Succeeded (0 warnings)
- Run: Tested successfully on Windows

---

## Update — Login Wired to SQLite Database

### Prompt
Update the Login page to authenticate using User ID and Password against the SQLite database (UserAccount table). Replace "Username" with "User ID" throughout.

### Changes Made

| File | Action | Description |
|------|--------|-------------|
| `ViewModels/UserAccount/LoginViewModel.cs` | Modified | Renamed `Username` → `UserId`, injected `DatabaseService` via constructor, `OnLoginAsync` queries DB to verify credentials, shows "Welcome, {FirstName}!" alert on success |
| `Views/UserAccount/LoginPage.xaml` | Modified | Changed placeholder from "Username" to "User ID", binding from `Username` to `UserId`, removed XAML BindingContext (now set via DI in code-behind) |
| `Views/UserAccount/LoginPage.xaml.cs` | Modified | Accepts `LoginViewModel` via constructor injection, sets `BindingContext = viewModel` |
| `MauiProgram.cs` | Modified | Registered `LoginViewModel` (transient) and `LoginPage` (transient) for DI |

### Login Flow
1. User enters **User ID** and **Password**
2. Validates both fields are not empty
3. Queries `UserAccount` table: `WHERE UserId == input AND Password == input`
4. If no match → "Invalid User ID or Password."
5. If match found → "Login successful!" + DisplayAlert "Welcome, Hello {FirstName}!"

### Status
- Build: Succeeded (0 warnings)
- Run: Tested successfully on Windows
