using MariVshApp.Models;

namespace MariVshApp.Repositories.Interfaces;

public interface IUserAccountRepository
{
    Task<int> InsertAsync(UserAccount user);
    Task<UserAccount?> GetByUserIdAsync(string userId);
    Task<UserAccount?> GetByPinAsync(string pin);

    // Retrieve — Admin section (future)
    Task<List<UserAccount>> GetAllAsync();
    Task<UserAccount?> GetByIdAsync(int id);

    // Update / Delete — Admin section (future)
    Task<int> UpdateAsync(UserAccount user);
    Task<int> DeleteAsync(int id);
}
