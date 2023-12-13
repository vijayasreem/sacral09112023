
using sacral09112023.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sacral09112023.Service
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(UserModel user);
        Task<UserModel> GetByIdAsync(int id);
        Task<List<UserModel>> GetAllAsync();
        Task<int> UpdateAsync(UserModel user);
        Task<int> DeleteAsync(int id);
    }
}
