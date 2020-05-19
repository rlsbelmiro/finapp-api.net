using FinAppApi.Components.ViewModel.UserViewModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinAppApi.Components.Security
{
    public class Token
    {
        public static string GenerateToken(EditorUserViewModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Key);
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] { 
                    new Claim(ClaimTypes.NameIdentifier,user.Login),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim("ClientId",user.ClientId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }

        public static int GetClientId(ClaimsPrincipal user)
        {
            try
            {
                return Convert.ToInt32(user.Claims.Where(x => x.Type.Equals("ClientId")).FirstOrDefault().Value);
            }catch(Exception e)
            {
                return 0;
            }
        }
    }
}
