using System.Windows.Input;
using Xamarin.Forms;

namespace QuizAppMobile.ViewModels
{
    public class StartVM
    {
        public ICommand Login { get; }

        public ICommand Register { get; }

        public StartVM()
        {
            Login = new Command(OnLogin);
            Register = new Command(OnRegister);
        }

        private void OnLogin()
        {
            Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        private void OnRegister()
        {
            Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}
