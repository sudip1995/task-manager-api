using System;
using System.Composition;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using TaskManager.Library.Ioc;
using TaskManager.Library.Models;

namespace TaskManager.Library.DataProviders
{
    [Export(typeof(IUserInfoProvider))]
    public class UserInfoProvider: IUserInfoProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        [ImportingConstructor]
        public UserInfoProvider(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }
        public User GetUser()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims.ToList();
            if (claims.Any() == false)
            {
                Console.WriteLine(
                    "Claims not found. Either IHttpContextAccessor is not present or ClaimsPrincipal is not present.");
            }
            var user = new User
            {
                UserName = claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Name)?.Value,
                //FirstName = claims.FirstOrDefault(c => c.Type.Contains(ClaimConstants.FirstNameClaim))?.Value,
                //LastName = claims.FirstOrDefault(c => c.Type.Contains(ClaimConstants.LastNameClaim))?.Value,
                Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
            };
            return user;
        }

    }
}
