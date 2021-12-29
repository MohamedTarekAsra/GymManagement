using GymAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace GymAPI.Classes
{
    public class TokenManager
    {
        HMACSHA256 hmac;
        static string key;
        public TokenManager()
        {
            hmac = new HMACSHA256();
            key = Convert.ToBase64String(hmac.Key);
        }
        /// <summary>
        /// Use the below code to generate symmetric Secret Key

        /// </summary>
        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        public static AuthonticationResponse GenerateToken(int ID, string username, int? UserType, string FirstName,string LastName, string Email, string MobileNumber, string ProfileImage,
                                           int Status, int? Gender, int? GymId , string Address, int? TenantId, int expireMinutes = 60)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[] {
                        new Claim(ClaimTypes.Name, username),
                        new Claim("Email",Email),
                        new Claim("Gender", Gender.HasValue ? Gender.Value.ToString() : ""),
                        new Claim( "MobilePhone", MobileNumber),
                        new Claim("ID", ID.ToString()),
                        new Claim("StreetAddress", string.IsNullOrEmpty(Address) ? "" :Address ),
                        new Claim( "FirstName", FirstName),
                        new Claim( "LastName", LastName),
                        new Claim( "UserType", UserType.HasValue ? UserType.Value.ToString() : ""),
                        new Claim( "Status", Status.ToString()),
                        new Claim( "ProfileImage", string.IsNullOrEmpty(ProfileImage) ? "" : ProfileImage),
                        new Claim( "GymId", GymId.HasValue ? GymId.Value.ToString() : ""),
                        new Claim( "TenantId", TenantId.HasValue ? TenantId.Value.ToString() : ""),
                }),
                //
                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                //
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            //
            return new AuthonticationResponse() {  access_token = token, refresh_token = generateRefreshToken()};
        }
        private static string generateRefreshToken()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
                
            }
        }
        public static bool CheckUser(string username, string password)
        {
            // should check in the database
            return true;
        }
        private static bool ValidateToken(string token, out string username)
        {
            username = null;

            var simplePrinciple = GetPrincipal(token);//JwtManager.
            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
                return false;

            // More validate to check whether username exists in system

            return true;
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            string username;

            if (ValidateToken(token, out username))
            {
                // based on username to get more information from database 
                // in order to build local identity
                var claims = new List<Claim>
                {
            new Claim(ClaimTypes.Name, username)
                    // Add more claims if needed: Roles, ...
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }
        //
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                    //...your setting

                    // set ClockSkew is zero
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch (Exception)
            {
                //should write log
                return null;
            }
        }
    }
}