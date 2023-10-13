using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using app_authors.Dtos;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace app_authors.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        // private readonly SignInManager<IdentityUser> _signInManager;
        // private readonly HashService _hashService;
        private readonly IDataProtector _protector;

        public AccountController(UserManager<IdentityUser> userManager, IConfiguration configuration, IDataProtectionProvider protectionProvider)
        {
            _userManager = userManager;
            _configuration = configuration;
            // _signInManager = signInManager;
            // _hashService = hashService;
            _protector = protectionProvider.CreateProtector("valor_unico_y_quizas_secreto");
        }

        [HttpPost("register", Name = "RegisterUser")]
        public async Task<ActionResult<AuthenticationResponseDto>> RegisterUser(UserCredentialsDto userCredentialsDto)
        {
            var user = new IdentityUser { UserName = userCredentialsDto.Email, Email = userCredentialsDto.Email };

            var result = await _userManager.CreateAsync(user, userCredentialsDto.Password!);

            if (result.Succeeded)
            {
                return await BuildToken(userCredentialsDto);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        private async Task<AuthenticationResponseDto> BuildToken(UserCredentialsDto userCredentialsDto)
        {
            var claims = new List<Claim>() {
                new("email", userCredentialsDto.Email!),
                new("lo que yo quiera", "cualquier cosa")
            };

            var user = await _userManager.FindByEmailAsync(userCredentialsDto.Email!);

            var claimsDB = await _userManager.GetClaimsAsync(user!);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]!));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            return new AuthenticationResponseDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expires
            };
        }
    }
}