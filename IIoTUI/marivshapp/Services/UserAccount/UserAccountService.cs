using MariVshApp.Models;
using MariVshApp.Repositories.Interfaces;

namespace MariVshApp.Services.UserAccount;

public class UserAccountService
{
    private readonly IUserAccountRepository _userAccountRepository;

    public UserAccountService(IUserAccountRepository userAccountRepository)
    {
        _userAccountRepository = userAccountRepository;
    }

    public async Task<(bool Success, string Message)> SignUpAsync(
        string userId,
        string name,
        string password,
        string pin,
        string description,
        int userTypeId)
    {
        var existing = await _userAccountRepository.GetByUserIdAsync(userId);
        if (existing is not null)
            return (false, "User ID already exists. Please use a different one.");

        var existingPin = await _userAccountRepository.GetByPinAsync(pin);
        if (existingPin is not null)
            return (false, "PIN already in use. Please choose a different PIN.");

        var now = DateTime.Now;
        var user = new Models.UserAccount
        {
            UserId = userId,
            Name = name,
            Password = password,
            Pin = pin,
            Description = description,
            IsActive = true,
            UserTypeId = userTypeId,
            CreatedDate = now,
            UpdatedDate = now
        };

        await _userAccountRepository.InsertAsync(user);
        return (true, "Sign-up successful!");
    }

    public async Task<(bool Success, string Message, Models.UserAccount? User)> LoginAsync(
        string userId, string password)
    {
        var user = await _userAccountRepository.GetByUserIdAsync(userId);
        if (user is null || user.Password != password)
            return (false, "Invalid User ID or Password.", null);

        return (true, "Login successful!", user);
    }

    public async Task<(bool Success, string Message, Models.UserAccount? User)> LoginByPinAsync(string pin)
    {
        var user = await _userAccountRepository.GetByPinAsync(pin);
        if (user is null)
            return (false, "Invalid PIN.", null);

        return (true, "Login successful!", user);
    }
}
