using Microsoft.IdentityModel.Tokens;
using POSapi.Model.Data;
using POSapi.Model.Request;
using POSapi.Model.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POSapi
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "kskKhmerSlKhmerkskKhmerSlKhmerkskKhmerSlKhmerkskKhmerSlKhmerkskKhmerSlKhmerkskKhmerSlKhmerkskKhmerSlKhmerkskKhmerSlKhmerkskKhmerSlKhmerkskKhmerSlKhmerkskKhmerSlKhmerkskKhmerSlKhmerkskKhmerSlKhmerkskKhmerSlKhmer";
        private const int JWT_TOKEN_VALIDITY_MIN = 20;



        public JwtResponse GenrateToken(LoginRequest request, List<User> userList)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Password))
                return null;


            var user = userList.Where(x=>x.UserName == request.UserName && x.Password == request.Password).FirstOrDefault();
            if (user == null) return null;

            var tokenExpire = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MIN);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, request.UserName),
                new Claim("Role", ""+user.RoleId)
            });

            var signingCredential = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                );

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimIdentity,
                Expires = tokenExpire,
                SigningCredentials = signingCredential
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new JwtResponse
            {
                UserId = user.UserId,
                UserName = user.UserName,
                RoleId = user.RoleId,
                JwtToken = token,
                ExpireIn = (int)tokenExpire.Subtract(DateTime.Now).TotalSeconds,
            };

        }


    }
}
