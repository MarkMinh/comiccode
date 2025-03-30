using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAPI.Models;

namespace WebAPI.Ultility
{
    public class Common
    {
        public readonly IConfiguration _config;

        public static ILogger<Common> _logger;

        public Common(ILogger<Common> logger)
        {
            _logger = logger;
        }

        public static JwtSecurityToken GenerateJwt(IConfiguration config, User user)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
        new Claim(JwtRegisteredClaimNames.GivenName, user.Username ?? string.Empty),
        new Claim("UserId", user.Id.ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddDays(1); // Token hết hạn sau 1 ngày

            // 🔹 Đảm bảo thêm "exp" claim theo chuẩn JWT
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(expires).ToUnixTimeSeconds().ToString()));

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: expires, // Cần có expires ở đây
                signingCredentials: signingCredentials
            );

            return token;
        }


        // Hàm trả về token dạng string
        public static string GenerateJwtToken(IConfiguration config, User user)
        {
            var token = GenerateJwt(config, user);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GetMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static int? IsCookieJwtValid(HttpContext context, IConfiguration config, string cookieName = "JwtToken")
        {
            var jwtToken = context.Request.Cookies[cookieName];
            if (string.IsNullOrEmpty(jwtToken))
            {
                return null;
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                // Lấy key đúng cách
                byte[] keyBytes;
                try
                {
                    keyBytes = Convert.FromBase64String(config["Jwt:Key"]);
                }
                catch (FormatException)
                {
                    keyBytes = Encoding.UTF8.GetBytes(config["Jwt:Key"]);
                }

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"], // Chỉ dùng một giá trị
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };

                Console.WriteLine("Jwt:" + jwtToken);
                Console.WriteLine("Issuer: " + config["Jwt:Issuer"]);
                Console.WriteLine("Audience: " + config["Jwt:Audience"]);
                Console.WriteLine("Server UTC Time: " + DateTime.UtcNow);
                Console.WriteLine("Server Local Time: " + DateTime.Now);
                var principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    // Ví dụ: lấy user id từ custom claim "UserId" (nếu bạn đã thêm nó)
                    var userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(e => e.Type == "UserId")?.Value;
                    // Nếu không có, bạn có thể thử lấy từ "sub":
                    if (string.IsNullOrEmpty(userIdClaim))
                    {
                        userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.Sub)?.Value;
                    }

                    if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                    {
                        Console.WriteLine("UserId không hợp lệ");
                        return null;
                    }
                    return userId;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token không hợp lệ: {ex.Message}");
                return null;
            }
        }

    }
}
