using System.Threading.Tasks;

namespace QuizAppMobile.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(string username, string email, string password);

        Task<(bool Succeded, string UserId)> LoginAsync(string username, string password);
    }
}
