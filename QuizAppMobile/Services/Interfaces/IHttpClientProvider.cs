using System.Net.Http;

namespace QuizAppMobile.Services.Interfaces
{
    public interface IHttpClientProvider
    {
        HttpClient GetClient();
    }
}
