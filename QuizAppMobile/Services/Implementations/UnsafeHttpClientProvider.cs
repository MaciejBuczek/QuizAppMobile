using QuizAppMobile.Services.Interfaces;
using System.Net.Http;

namespace QuizAppMobile.Services.Connections
{
    public class UnsafeHttpClientProvider : IHttpClientProvider
    {
        public HttpClient GetClient()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return  new HttpClient(handler);
        }
    }
}
