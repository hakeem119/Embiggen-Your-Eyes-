using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NasaProject.Data;

namespace NasaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] DTOs.RegisterDTO registerDto)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == registerDto.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists.");
            }
            var user = new Models.User
            {
                Username = registerDto.Username,
                Password = registerDto.Password, 
                Email = registerDto.Email,
                Role = registerDto.Role
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] DTOs.RegisterDTO loginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(new { user.UserId, user.Username, user.Email, user.Role });
        }

    }
}
