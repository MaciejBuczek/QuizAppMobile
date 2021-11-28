using QuizAppMobile.Services.Interfaces;
using System.Threading.Tasks;

namespace QuizAppMobile.Services.Implementations
{
    public class AlertMessageService : IMessageService
    {
        public Task DisplayErrorMessage(string message)
        {
            return App.Current.MainPage.DisplayAlert("Error", message, "Ok");
        }

        public Task DisplaySuccessMessage(string message)
        {
            return App.Current.MainPage.DisplayAlert("Success", message, "Ok");
        }
    }
}
