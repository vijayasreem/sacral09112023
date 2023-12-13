
using System.Collections.Generic;
using System.Threading.Tasks;
using sacral09112023.DTO;

namespace sacral09112023.Service
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> GetByIdAsync(int id);
        Task<int> CreateAsync(UserModel user);
        Task<bool> UpdateAsync(UserModel user);
        Task<bool> DeleteAsync(int id);
    }
}
