using Newtonsoft.Json;
using QuizAppMobile.Constants;
using QuizAppMobile.Models.SignalR;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuizAppMobile.Services.Implementations
{
    public class SummaryAPIService : ISummaryService
    {
        private readonly IHttpClientProvider _httpClientProvider;

        public SummaryAPIService()
        {
            _httpClientProvider = DependencyService.Resolve<IHttpClientProvider>();
        }

        public async Task<(bool Succeded, List<UserScore> UsersScores)> GetSummaryInfo(string lobbyCode)
        {
            if (string.IsNullOrEmpty(lobbyCode))
                throw new ArgumentNullException(nameof(lobbyCode));

            var url = $"{APIConfigs.BaseURL}{APIConfigs.SummaryURL}?lobbyCode={lobbyCode}";
            var client = _httpClientProvider.GetClient();

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<List<UserScore>>(responseJson);
                return (true, responseObj);
            }
            return (false, null);
        }
    }
}
