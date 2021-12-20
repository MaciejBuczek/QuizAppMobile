using Microsoft.AspNetCore.SignalR.Client;
using QuizAppMobile.Services.Interfaces;
using System.Net.Http;

namespace QuizAppMobile.Services.Connections
{
    class UnsafeConnectionProvider : IHubConnectionProvider
    {
        public HubConnection GetConnection(string url)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl(url, (opts) =>
                {
                    opts.HttpMessageHandlerFactory = (message) =>
                    {
                        if (message is HttpClientHandler clientHandler)
                            clientHandler.ServerCertificateCustomValidationCallback +=
                                (sender, certificate, chain, sslPolicyErrors) => { return true; };
                        return message;
                    };
                })
                .Build();

            return connection;
        }
    }
}
