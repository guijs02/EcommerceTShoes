using Microsoft.AspNetCore.Components.Authorization;

namespace EcommerceWeb.Services.AuthClient
{
    public class AuthManager
    {
        private readonly AuthenticationStateProvider _stateProvider;

        public AuthManager(AuthenticationStateProvider stateProvider)
        {
            _stateProvider = stateProvider;
        }

        public async Task<bool> IsTokenExpiration()
        {
            var authState = await _stateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var expClaim = user.FindFirst("exp");
                if(expClaim is not null)
                {
                    var expirationTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim.Value));
                    return expirationTime < DateTimeOffset.UtcNow;
                }
            }
            return false;
        }
    }
}
