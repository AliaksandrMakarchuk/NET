using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebRestApi.Service;
using WebRestApi.Service.Models;

namespace WebRestApi.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AbstractDbContext _dbContext;
        private IList<IdentityUser> _users = new List<IdentityUser>
        {
            new IdentityUser { Login = "admin", Password = "12345", Roles = Roles.ADMIN },
            new IdentityUser { Login = "user", Password = "abc", Roles = Roles.USER }
        };

        public AccountController(AbstractDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Token(string login, string password)
        {
            var identity = await GetIdentity(login, password);

            if (identity == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ErrorResponse { Message = "Invalid user name or password" });
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return new JsonResult(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == login && x.Password == password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}