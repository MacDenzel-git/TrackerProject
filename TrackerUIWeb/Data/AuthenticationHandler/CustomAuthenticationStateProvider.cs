using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;


namespace TrackerUIWeb.Data.AuthenticationHandler
{


    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly ISessionStorageService _sessionStorage;
        public CustomAuthenticationStateProvider(ILocalStorageService localStorage, ISessionStorageService sessionStorage)
        {
            _localStorage = localStorage;
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());

            // Retrieve the stored token from Local Storage (or a similar mechanism)
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrEmpty(token))
            {
                // Example: Add claims if the token exists (you can add more claims based on your app)

                string role = await _sessionStorage.GetItemAsync<string>("Role");
                int shopId = await _sessionStorage.GetItemAsync<int>("shopId");
                string Username = await _sessionStorage.GetItemAsync<string>("LoggedInUser");
                string shopName = await _sessionStorage.GetItemAsync<string>("shopName");

                if (Username is null)
                {
                    return null;
                }

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Username),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, shopId.ToString()),
                new Claim(ClaimTypes.Locality,shopName)
            };

                // Create the ClaimsIdentity and set the user
                var identity = new ClaimsIdentity(claims, "Bearer");
                user = new ClaimsPrincipal(identity);
            }

            // Create and return the AuthenticationState with the current user
            var authenticationState = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
            return authenticationState;
        }

        // You can also add a method for setting the authentication state when the user logs in
        public void MarkUserAsAuthenticated(string token)
        {
            // Store the token (for example in LocalStorage)
            _localStorage.SetItemAsync("authToken", token);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "Example User"),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("CustomClaim", "SomeValue")
        };

            var identity = new ClaimsIdentity(claims, "Bearer");
            var user = new ClaimsPrincipal(identity);

            var authenticationState = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        // Method to mark the user as logged out
        public void MarkUserAsLoggedOut()
        {
            _localStorage.RemoveItemAsync("authToken");

            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var authenticationState = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }
    }
}

