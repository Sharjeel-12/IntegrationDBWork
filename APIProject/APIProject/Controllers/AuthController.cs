using APIProject.Dtos;
using APIProject.Models;
using APIProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _configuration = config;
        }
        [HttpPost("/signUp")]
        public async Task<IActionResult> SignUp(SignUpDto dto)
        {
            CreateUserDto createUserDto = new CreateUserDto();  
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            createUserDto.PasswordHash = new PasswordHasher<SignUpDto>().HashPassword(dto, dto.Password);
            createUserDto.Email = dto.Email;
            createUserDto.UserName = dto.UserName;
            createUserDto.Role = dto.Role;
            await _userService.RegisterUserAsync(createUserDto);
            return Ok("signed up successfully");
        }
        [HttpPost("/login")]
        public async Task<ActionResult<string>> loginUser(LoginDto dto)
        {
            string token=string.Empty;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserModel UserFetched = await _userService.FetchUserByEmailAsync(dto.Email);
            if (UserFetched == null) { return BadRequest("No such user exists"); }
            if (new PasswordHasher<LoginDto>().VerifyHashedPassword(dto, UserFetched.PasswordHash, dto.Password) is PasswordVerificationResult.Failed)
            {
                return BadRequest("Incorrect Credentials");
            }
            token = GenerateToken(UserFetched);

            return Ok(token);
        }

        private string GenerateToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var creds= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
{
    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
    new Claim(ClaimTypes.Role,user.RoleId.ToString()),
    new Claim(ClaimTypes.Name, user.UserName!),
    new Claim(ClaimTypes.Email, user.Email!),
    new Claim(ClaimTypes.UserData, user.UserName!)
};

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
