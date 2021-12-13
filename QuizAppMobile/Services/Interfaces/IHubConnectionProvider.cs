using Microsoft.AspNetCore.SignalR.Client;

namespace QuizAppMobile.Services.Interfaces
{
    public interface IHubConnectionProvider
    {
        HubConnection GetConnection(string url);
    }
}
