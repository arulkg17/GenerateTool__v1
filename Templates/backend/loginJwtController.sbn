using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using <Your Namespace>.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace <Your Namespace>.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class LoginJwtController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly IConfiguration _configuration;

    public LoginController(
        ILogger<LoginController> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            _logger.LogInformation("Login attempt for user: {UserName}", dto.UserName);

            if (!ModelState.IsValid)
                return BadRequest();

            HttpContext.Session.SetString("MyTest", "My set value test");

            var configUsername = _configuration["UserCredentials:Username"];
            var configPassword = _configuration["UserCredentials:Password"];

            if (dto.UserName != configUsername || dto.Password != configPassword)
            {
                _logger.LogWarning("Invalid login attempt for user: {UserName}", dto.UserName);
                return Unauthorized();
            }

            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var expiryMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryMinutes"]);

            if (string.IsNullOrEmpty(jwtKey))
                throw new Exception("JWT key is missing in configuration");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, dto.UserName),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(expiryMinutes);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: null,
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            _logger.LogInformation("Login successful for user: {UserName}", dto.UserName);

            return Ok(new
            {
                token = jwt,
                expiration = expiry
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during login");

            return StatusCode(500, new
            {
                message = "Login failed",
                error = ex.Message
            });
        }
    }
}
