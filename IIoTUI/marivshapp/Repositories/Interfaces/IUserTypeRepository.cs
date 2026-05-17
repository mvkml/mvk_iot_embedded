using MariVshApp.Models;

namespace MariVshApp.Repositories.Interfaces;

public interface IUserTypeRepository
{
    Task<List<UserType>> GetAllActiveAsync();
    Task<UserType?> GetByIdAsync(int typeId);

    // Future — Admin section
    Task<List<UserType>> GetAllAsync();
    Task<int> InsertAsync(UserType userType);
    Task<int> UpdateAsync(UserType userType);
    Task<int> DeleteAsync(int typeId);
}
