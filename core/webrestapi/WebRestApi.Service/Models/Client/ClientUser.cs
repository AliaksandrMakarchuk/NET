using System;

namespace WebRestApi.Service.Models.Client
{
    public class ClientUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}