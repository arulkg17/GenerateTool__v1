using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using InvoiceManagement.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InvoiceManagement.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class LoginJwtController : ControllerBase
{
    private readonly ILogger<LoginJwtController> _logger;
    private readonly IConfiguration _configuration;

    public LoginJwtController(
            ILogger<LoginJwtController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

    [AllowAnonymous]
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            _logger.LogInformation("Generating JWT token...");

            _logger.LogInformation("Login attempt for user: {UserName}", dto.UserName);

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid request"
                });
            }

            //HttpContext.Session.SetString("MyTest", "My set value test");

            var configUsername = _configuration["UserCredentials:Username"];
            var configPassword = _configuration["UserCredentials:Password"];

            if (dto.UserName != configUsername || dto.Password != configPassword)
            {
                _logger.LogWarning("Invalid login attempt for user: {UserName}", dto.UserName);
                return Unauthorized(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid username or password"
                });
            }

            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var expiryMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryMinutes"]);

            if (string.IsNullOrEmpty(jwtKey))
                throw new Exception("JWT key is missing in configuration");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, dto.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, dto.UserName),
                new Claim(JwtRegisteredClaimNames.Email, dto.UserName),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(expiryMinutes);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            _logger.LogInformation("Login successful for user: {UserName}", dto.UserName);

            return Ok(new ApiResponse<LoginResponseDto>
            {
                Success = true,
                Message = "Login successful",
                Data = new LoginResponseDto
                {
                    Token = jwt,
                    Expiration = expiry
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during login");

            return StatusCode(StatusCodes.Status500InternalServerError,
            new ApiResponse<string>
            {
                Success = false,
                Message = "Login failed",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }
}
