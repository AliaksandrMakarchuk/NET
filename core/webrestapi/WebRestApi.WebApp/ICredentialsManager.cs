namespace WebRestApi.WebApp
{
    public interface ICredentialsManager
    {
        bool IsLoggedIn { get; }
    }
}