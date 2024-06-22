using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        private readonly UserManager<User> _userManager;

        public TokenService(IConfiguration config,UserManager<User> userManager)
        {
            _userManager = userManager;

            // Retrieve token key from configuration
            var tokenKey = config["TokenKey"];

            // Ensure the key is properly encoded to bytes
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            //_key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public async Task<string> CreateToken(User user)
        {
           var claims = new List<Claim>()
           {
              new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
              new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
              new Claim(JwtRegisteredClaimNames.Email,user.Email),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Example additional claim (JWT ID)
           };

           var roles =  await _userManager.GetRolesAsync(user);

           claims.AddRange(roles
            .Select(role => new Claim(ClaimTypes.Role,role)));

            var creds = new SigningCredentials(
                _key, SecurityAlgorithms.HmacSha512Signature
                );

            var tokenDescriptor =  new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
             //https://jwt.ms/ to decode to see information ....
        }
    }
}