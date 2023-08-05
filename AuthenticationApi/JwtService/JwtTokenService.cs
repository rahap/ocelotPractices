using OcelotCore.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationApi.JwtService
{
    public class JwtTokenService : IJWTManager
    {
        public UserModel AuthenticateUser(UserModel model)
        {
            UserModel user = null;

            if (model.Username == "sa" && model.Password == "123")
            {
                user = new UserModel { Name = "selcuk", Surname = "topcu" };
            }

            return user;
        }

        public string GenerateJSONWebToken(UserModel userInfo)
        {
            var securtyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTModel.Key));
            var credentials = new SigningCredentials(securtyKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userInfo.Name),
                new Claim(JwtRegisteredClaimNames.Sub,userInfo.Surname),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: JWTModel.Issuer,
                audience: JWTModel.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(30),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodetoken;
        }
    }
}
