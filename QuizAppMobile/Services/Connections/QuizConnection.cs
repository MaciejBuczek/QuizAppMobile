using Microsoft.AspNetCore.SignalR.Client;
using QuizAppMobile.Constants;
using QuizAppMobile.Models.SignalR;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuizAppMobile.Services.Connections
{
    class QuizConnection
    {
        private readonly IHubConnectionProvider _hubConnectionProvider;
        private readonly HubConnection _hubConnection;

        public QuizConnection(Action<QuizInfo> initializeQuizAction, Action<List<UserScore>> updateScoreboardAction,
            Action<PersonalisedQuestion> loadQuestionAction, Action<string> displayErrorAction, Action requestQuestionAction, 
            Action redirectToSummaryAction, Action beginQuizAction)
        {
            _hubConnectionProvider = DependencyService.Resolve<IHubConnectionProvider>();
            _hubConnection = _hubConnectionProvider.GetConnection(APIConfigs.BaseURL + APIConfigs.QuizHubURL);

            _hubConnection.On<QuizInfo>("initalizeQuiz", initializeQuizAction);
            _hubConnection.On<List<UserScore>>("updateScoreboard", updateScoreboardAction);
            _hubConnection.On<PersonalisedQuestion>("loadQuestion", loadQuestionAction);
            _hubConnection.On<string>("displayError", displayErrorAction);
            _hubConnection.On("requestQuestion", requestQuestionAction);
            _hubConnection.On("redirectToSummary", redirectToSummaryAction);
            _hubConnection.On("beginQuiz", beginQuizAction);
        }

        public async Task Connect(string lobbyCode, string userId)
        {
            await _hubConnection.StartAsync();
            await _hubConnection.InvokeAsync("ConnectToQuizMobile", lobbyCode, userId);
        }

        public async Task Disconnect()
        {
            await _hubConnection.StopAsync();
        }
    }
}
