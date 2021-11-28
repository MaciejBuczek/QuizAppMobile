using QuizAppMobile.Configs;
using QuizAppMobile.Models.API;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuizAppMobile.Services.Implementations
{
    public class UserAPIService : IUserService
    {
        private readonly IHttpClientProvider _httpClientProvider;

        public UserAPIService()
        {
            _httpClientProvider = DependencyService.Resolve<IHttpClientProvider>();
        }

        public Task<bool> LoginAsync(string username, string email)
        {
            throw new NotImplementedException();
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
