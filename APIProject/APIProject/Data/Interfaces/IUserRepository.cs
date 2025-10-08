using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsersAsync();
        //Task<User> GetUserAsync(int UserId);
        Task<UserModel> GetUserByEmailAsync(string Email);
        Task AddUserAsync(CreateUserDto userDto);
        //Task UpdateUserAsync(UpdateUserDto userDto);
        //Task DeleteUserAsync(int UserId);
    }


}
