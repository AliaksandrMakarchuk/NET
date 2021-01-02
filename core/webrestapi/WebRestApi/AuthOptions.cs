using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebRestApi
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public const string ISSUER = "MyAuthSecret";
        /// <summary>
        /// 
        /// </summary>
        public const string AUDIENCE = "MyAuthClient";
        /// <summary>
        /// 
        /// </summary>
        private const string KEY = "mysupersecret_secret!123";
        /// <summary>
        /// 
        /// </summary>
        public const int LIFETIME = 1;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}