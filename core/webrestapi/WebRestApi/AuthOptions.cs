using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebRestApi
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthSecret";
        public const string AUDIENCE = "MyAuthClient";
        private const string KEY = "mysupersecret_secret!123";
        public const int LIFETIME = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}