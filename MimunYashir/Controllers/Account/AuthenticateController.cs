using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimunYashir.Models;
using MimunYashirCore.Interfaces;
using MimunYashirCore.Services;
using MimunYashirInfrastructure.Exceptions;
using MimunYashirInfrastructure.Identity;
using MimunYashirInfrastructure.Log;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MimunYashir.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : _baseController
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        private readonly IAppLogger<AuthenticateController> _logger;

        public AuthenticateController(WebAppContext context,
            IConfiguration configuration,
            IAppLogger<AuthenticateController> logger,
            IAccountService accountService) : base(context)
        {
            _configuration = configuration;
            _accountService = accountService;
            _logger = logger;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var id = await _accountService.GetCustomerIdByIdAsync(loginModel.Id);
                if (id == null)
                    return Unauthorized();

                var authClaims = new List<Claim>
                    {
                        new Claim(IdentityClaims.USER_CLAIM_ID, id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                var token = CreateToken(authClaims);
                return Ok(new LoginModel{
                    Id = loginModel.Id,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception ex)
            {
                _logger.Error(new ManagedException(ex, "Failed to login", AppModule.ACCOUNT, AppLayer.WEB_API));
                return BadRequest(ex.Message);
            }
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var jwtConfig = _configuration.GetJwtConfig();
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));

            var token = new JwtSecurityToken(
                issuer: jwtConfig.Issuer,
                audience: jwtConfig.Audience,
                expires: DateTime.Now.AddMinutes(jwtConfig.TokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
}
