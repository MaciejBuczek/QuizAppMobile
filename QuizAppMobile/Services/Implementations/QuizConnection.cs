using Microsoft.AspNetCore.SignalR.Client;
using QuizAppMobile.Constants;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QuizAppMobile.Services.Implementations
{
    public class QuizConnection
    {
        private readonly IHubConnectionProvider _hubConnectionProvider;
        private readonly HubConnection _hubConnection;

        public QuizConnection()
        {
            _hubConnectionProvider = DependencyService.Resolve<IHubConnectionProvider>();
            _hubConnection = _hubConnectionProvider.GetConnection(APIConfigs.BaseURL + APIConfigs.LobbyHubURL);
        }
    }
}
