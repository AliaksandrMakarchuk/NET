namespace WebRestApi
{
    public class IdentityUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Roles Roles { get; set; }
    }
}