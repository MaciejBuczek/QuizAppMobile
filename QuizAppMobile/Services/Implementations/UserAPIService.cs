using Newtonsoft.Json;
using QuizAppMobile.Configs;
using QuizAppMobile.Models.API;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppMobile.Services.Implementations
{
    public class UserAPIService : IUserService
    {

        private readonly HttpClient _httpClient = new HttpClient();

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

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            var client = new HttpClient(handler);

            var url = APIConfigs.BaseURL + APIConfigs.RegisterURL;
            HttpContent httpContent = JsonContent.Create(request);

            var response = await client.PostAsync(url, httpContent);
            return response.IsSuccessStatusCode;
        }
    }
}
