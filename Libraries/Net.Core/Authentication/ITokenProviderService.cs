namespace Net.Core.Authentication
{
    public interface ITokenProviderService
    {
        public Task<bool> IsValidTokenAsync(string token);

        public Task<string> GenerateTokenAsync(string userName, string password);
    }
}
