using APIProject.Data.Interfaces;
using APIProject.Dtos;
using APIProject.Models;
using APIProject.Services.Interfaces;
namespace APIProject.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _repo; 
        public UserService(IUserRepository  repo) { _repo = repo; }
        public async Task<List<UserModel>> FetchAllUsersAsync() {return await _repo.GetAllUsersAsync(); }
        public async Task<UserModel> FetchUserByEmailAsync(string Email) { return await _repo.GetUserByEmailAsync(Email); }
        public async Task RegisterUserAsync(CreateUserDto userDto)
        {
            await _repo.AddUserAsync(userDto);
        }
    }
}
