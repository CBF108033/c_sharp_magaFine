using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace magaFine.Helpers
{
    public class Jwt_Helper
    {
        private static readonly string SecretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"]; // 設定密鑰
        //private static readonly string Issuer = "your_issuer";
        //private static readonly string Audience = "your_audience";

        /**
         * 生成token
         * @param string username 使用者名稱
         * @param int expireMinutes 有效時間
         */
        public static string GenerateToken(string username, int expireMinutes = 30)
        {
            var symmetricKey = Encoding.UTF8.GetBytes(SecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = now.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature),
                //Issuer = Issuer,
                //Audience = Audience
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }
}