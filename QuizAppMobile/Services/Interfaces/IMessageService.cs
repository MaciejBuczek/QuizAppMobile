using System.Threading.Tasks;

namespace QuizAppMobile.Services.Interfaces
{
    public interface IMessageService
    {
        Task DisplayErrorMessage(string message);

        Task DisplaySuccessMessage(string message);

    }
}
