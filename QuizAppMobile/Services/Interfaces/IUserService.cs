using System.Threading.Tasks;

namespace QuizAppMobile.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(string username, string email, string password);

        Task<bool> LoginAsync(string username, string email);
    }
}
