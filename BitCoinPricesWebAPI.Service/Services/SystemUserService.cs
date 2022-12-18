using BitCoinPricesWebAPI.Core.Models;
using BitCoinPricesWebAPI.Repo.Interface;
using BitCoinPricesWebAPI.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace BitCoinPricesWebAPI.Service.Services
{
    public class SystemUserService : ISystemUserService
    {
        private ISystemUserRepository repository;
        private IConfiguration configuration;
        public SystemUserService(ISystemUserRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }
        public async Task<string> AuthenticateUser(UserModel user)
        {
            string jwttoken = string.Empty;
            var _user = await repository.AuthenticateUser(user);
            if (_user != null)
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        
                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);
                 jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
               
                
                return jwttoken;
            }
            else
            {
                return "";
            }
        }
    }
}
