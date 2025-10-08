﻿namespace APIProject.Dtos
{
    public class CreateUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Password {  get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

    }
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
