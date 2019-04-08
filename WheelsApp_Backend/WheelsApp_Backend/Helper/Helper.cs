using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WheelsApp_Backend.Models;

namespace WheelsApi.Helper
{
    public static class Helper
    {

        public static string Hash(string input)
        {
            return string.Join("", (new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input))).Select(x => x.ToString("X2")).ToArray());
        }


        public static bool IsTaken(string takenInput) {
            return (takenInput == null) ? false : true;
        }

        public static JwtSecurityToken Token(string issuer, string audience, SymmetricSecurityKey signinKey, string email, string Id, Role role)
        {
            var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("user_id", Id),
                        new Claim("user_email", email),
                        new Claim("user_role", role.ToString())
                    };

            var token = new JwtSecurityToken
            (
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMonths(45),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
   

    }
}
