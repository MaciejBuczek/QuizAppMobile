using Newtonsoft.Json;
using QuizAppMobile.Constants;
using QuizAppMobile.Models.API;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuizAppMobile.Services.Implementations
{
    public class LobbyAPIService : ILobbyService
    {
        private IHttpClientProvider _httpClientProvider;

        public LobbyAPIService()
        {
            _httpClientProvider = DependencyService.Resolve<IHttpClientProvider>();
        }

        public async Task<JoinResponse> ConnectToLobby(string lobbyCode)
        {
            if (string.IsNullOrEmpty(lobbyCode))
                throw new ArgumentNullException(lobbyCode);

            var client = _httpClientProvider.GetClient();

            var url = APIConfigs.BaseURL + APIConfigs.JoinURL;
            url += $"?lobbyCode={lobbyCode}";

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<JoinResponse>(responseJson);
                return responseObj;
            }
            return null;
        }

        public async Task<bool> ValidateLobbyCode(string lobbyCode)
        {
            if (string.IsNullOrEmpty(lobbyCode))
                throw new ArgumentNullException(lobbyCode);

            var client = _httpClientProvider.GetClient();

            var url = APIConfigs.BaseURL + APIConfigs.CheckLobbyCodeURL;
            url += $"?lobbyCode={lobbyCode}";

            var response = await client.GetAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
