using WebRestApi.Service.Models;
using WebRestApi.Service.Models.Client;

namespace WebRestApi.Controllers {
    /// <summary>
    /// 
    /// </summary>
    public static class ClientExtensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static ClientUser ToClientUser(this User user)
        {
            return new ClientUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleName = user.Role.Name
            };
        }
    }
}