using APIProject.Data.Interfaces;
using APIProject.Dtos;
using APIProject.Models;
using System.Data;
using System.Data.SqlClient;

namespace APIProject.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;
        public UserRepository(IConfiguration config)
        {
            _configuration = config;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            List<UserModel> users = new List<UserModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("getAllUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        UserModel user = new UserModel();
                        user.Id = reader.GetInt32(0);
                        user.Email = reader.GetString(1);
                        user.UserName = reader.GetString(2);
                        user.PasswordHash = reader.GetString(3);
                        user.RoleId = reader.GetInt32(4);
                        users.Add(user);
                    }
                }
                await connection.CloseAsync();
            }
                return users;

        }

        public async Task<UserModel> GetUserByEmailAsync(string Email)
        {
            UserModel user = new UserModel();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("getUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", Email);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        
                        user.Id = reader.GetInt32(0);
                        user.Email = reader.GetString(1);
                        user.UserName = reader.GetString(2);
                        user.PasswordHash = reader.GetString(3);
                        user.RoleId = reader.GetInt32(4);
                        
                    }
                }
                await connection.CloseAsync();
            }
            return user;
        }



        public async Task AddUserAsync(CreateUserDto userDto)
        {
            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("AddUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", userDto.Email);
                    command.Parameters.AddWithValue("@username", userDto.UserName);
                    command.Parameters.AddWithValue("@userPassword", userDto.PasswordHash);
                    command.Parameters.AddWithValue("@userRole", userDto.Role);


                    await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
            }
        }







    }
}
