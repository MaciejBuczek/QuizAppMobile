using Newtonsoft.Json;
using QuizAppMobile.Constants;
using QuizAppMobile.Models.API;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuizAppMobile.Services.Connections
{
    public class UserAPIService : IUserService
    {
        private readonly IHttpClientProvider _httpClientProvider;

        public UserAPIService()
        {
            _httpClientProvider = DependencyService.Resolve<IHttpClientProvider>();
        }

        public async Task<(bool Succeded, string UserId)> LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(password);

            var request = new LoginRequest
            {
                Username = username,
                Password = password
            };
            var url = APIConfigs.BaseURL + APIConfigs.LoginURL;

            HttpContent httpContent = JsonContent.Create(request);

            var client = _httpClientProvider.GetClient();

            var response = await client.PostAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return (true, responseString);
            }
            return (false, null);
        }

        public async Task<bool> RegisterAsync(string username, string email, string password)
        {
            var request = new RegisterRequest
            {
                Username = username,
                Email = email,
                Password = password
            };
            var url = APIConfigs.BaseURL + APIConfigs.RegisterURL;
            HttpContent httpContent = JsonContent.Create(request);

            var client = _httpClientProvider.GetClient();

            var response = await client.PostAsync(url, httpContent);
            return response.IsSuccessStatusCode;
        }
    }
}
