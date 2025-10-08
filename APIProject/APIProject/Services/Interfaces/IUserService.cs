using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> FetchAllUsersAsync();
        Task<UserModel> FetchUserByEmailAsync(string Email);
        Task RegisterUserAsync(CreateUserDto userDto);

    }
}
