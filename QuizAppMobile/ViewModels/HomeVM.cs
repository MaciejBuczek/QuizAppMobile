using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizAppMobile.ViewModels
{
    public class HomeVM
    {
        public HomeVM()
        {
            if (Application.Current.Properties.TryGetValue(Constants.Properties.Username, out var username))
            {
                WelcomeMessage = $"Hello {(string)username}!";
                Connect = new Command(async () => await OnConnect());
            }
            else
                Application.Current.MainPage.Navigation.PushAsync(new StartPage());
        }

        public string WelcomeMessage { get; }

        public ICommand Connect { get; }

        private async Task OnConnect()
        {

        }
    }
}
