using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.Models;
using WebAPI.Ultility;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Prn231ComicCodeContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsersController(Prn231ComicCodeContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Email và Password không được để trống!");
            }

            string hashedPassword = Common.GetMd5Hash(loginDto.Password);
            var userLogin = _context.Users.FirstOrDefault(u =>
                u.Email.ToLower() == loginDto.Email.ToLower() &&
                u.Password == hashedPassword);

            if (userLogin == null)
            {
                return BadRequest("Wrong email or password!");
            }

            var token = Common.GenerateJwtToken(_configuration, userLogin);

            // Lưu token vào HttpOnly Cookie
            Response.Cookies.Append("JwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(1)
            });

            return Ok(new { message = "Login successful" });
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (string.IsNullOrEmpty(registerDto.Email) || string.IsNullOrEmpty(registerDto.Password))
            {
                return BadRequest("Email và mật khẩu không được để trống!");
            }

            if (registerDto.Password != registerDto.RePassword)
            {
                return BadRequest("Mật khẩu nhập lại không khớp!");
            }

            if (_context.Users.Any(u => u.Email.ToLower() == registerDto.Email.ToLower()))
            {
                return Conflict("Email đã tồn tại!");
            }

            string hashedPassword = Common.GetMd5Hash(registerDto.Password);

            var newUser = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = hashedPassword,
                CreateAt = DateTime.UtcNow,
                IsActive = true,
                RoleId = 3
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(new { message = "Đăng ký thành công!", userId = newUser.Id });
        }

        [HttpGet("get-user-info")]
        public IActionResult GetUserInfo()
        {
            var user = Common.IsCookieJwtValid(HttpContext, _configuration);
            if (user == null)
            {
                return Unauthorized("Invalid or expired token");
            }
            return Ok(user);
        }

        [HttpPost("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = Common.IsCookieJwtValid(HttpContext, _configuration);
            if (userId == 0)
            {
                return Unauthorized("Invalid or expired token.");
            }

            // Lấy user từ database
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            if (!existingUser.Password.Equals(Common.GetMd5Hash(model.OldPassword)))
            {
                return BadRequest("Old password is incorrect.");
            }

            if (!model.NewPassword.Equals(model.ReNewPassword))
            {
                return BadRequest("Re-enter password is not correct");
            }

            existingUser.Password = Common.GetMd5Hash(model.NewPassword);
            existingUser.UpdateAt = DateTime.Now;
            _context.Users.Update(existingUser);
            _context.SaveChanges();

            return Ok("Password changed successfully.");
        }

        [HttpGet("check-login")]
        public IActionResult CheckLogin()
        {
            var user = Common.IsCookieJwtValid(HttpContext, _configuration);
            if (user == null)
            {
                return Unauthorized("Bạn chưa đăng nhập");
            }

            return Ok(new { isAuthenticated = true, message = "Bạn đã đăng nhập!" });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (Request.Cookies["JwtToken"] != null)
            {
                Response.Cookies.Delete("JwtToken");
            }

            return Ok(new { message = "Đăng xuất thành công!" });
        }
    }
}
