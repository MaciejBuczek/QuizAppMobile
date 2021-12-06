using Microsoft.AspNetCore.SignalR.Client;
using QuizAppMobile.Constants;
using QuizAppMobile.Models.SignalR;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuizAppMobile.Services.Implementations
{
    class LobbyConnection
    {
        private readonly IHubConnectionProvider _hubConnectionProvider;
        private readonly HubConnection _hubConnection;

        public LobbyConnection(Action<Lobby> initializeUsersAction, Action<string> addUserAction,
            Action closeLobbyAction, Action kickAction, Action<string> RedirectToQuizAction)
        {
            _hubConnectionProvider = DependencyService.Resolve<IHubConnectionProvider>();
            _hubConnection = _hubConnectionProvider.GetConnection(APIConfigs.BaseURL + APIConfigs.LobbyHubURL);

            _hubConnection.On<Lobby>("initializeUsers", initializeUsersAction);
            _hubConnection.On<string>("addUser", addUserAction);
            _hubConnection.On("closeLobby", closeLobbyAction);
            _hubConnection.On("kick", kickAction);
            _hubConnection.On("redirectToQuiz", RedirectToQuizAction);
        }

        public async Task Connect(string lobbyCode)
        {
            try
            {
                await _hubConnection.StartAsync();
            }catch (Exception e)
            {

            }
            await _hubConnection.InvokeAsync("ConnectToLobby", lobbyCode);
        }
    }
}
