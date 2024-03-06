namespace Net.Core.Authentication
{
    public interface IAuthenticationService
    {
        public Task SignInAsync(string username, string password);

        public Task SignOutAsync();
    }
}
