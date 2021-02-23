namespace WebRestApi.Service
{
    public class UserRole
    {
        private readonly string _roleName;
        public string RoleName => _roleName;
        private UserRole(string roleName)
        {
            _roleName = roleName;
        }

        public static UserRole ADMIN { get; } = new UserRole("admin");
        public static UserRole USER { get; } = new UserRole("user");
    }
}