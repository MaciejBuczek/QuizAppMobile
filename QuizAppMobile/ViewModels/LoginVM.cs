using QuizAppMobile.Constants;
using QuizAppMobile.Services.Interfaces;
using QuizAppMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizAppMobile.ViewModels
{
    public class LoginVM
    {

        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public string Username { get; set; }

        public string Password { get; set; }

        public ICommand Login { get; }

        public LoginVM()
        {
            _userService = DependencyService.Get<IUserService>();
            _messageService = DependencyService.Get<IMessageService>();
            Login = new Command(async () => await OnLogin());
        }

        private async Task OnLogin()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) 
            {
                await _messageService.DisplayErrorMessage("Please fill all informations");
                return;
            }

            var response = await _userService.LoginAsync(Username, Password);

            if (response.Succeded)
            {
                Application.Current.Properties[Properties.Username] = Username;
                Application.Current.Properties[Properties.UserId] = response.UserId;
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
            }               
            else
            {
                await _messageService.DisplayErrorMessage("Invalid username or password");
                return;
            }
        }
    }
}
