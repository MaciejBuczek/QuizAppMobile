using QuizAppMobile.Services.Implementations;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizAppMobile.ViewModels
{
    public class RegisterVM
    {
        IUserService _userService;
        IMessageService _messageService;

        public RegisterVM()
        {
            _userService = DependencyService.Get<IUserService>();
            _messageService = DependencyService.Get<IMessageService>();
            Register = new Command(async() => await OnRegister());
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public ICommand Register { get; }

        private async Task OnRegister()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                await _messageService.DisplayErrorMessage("Please provide all informations");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await _messageService.DisplayErrorMessage("Passwords do not match");
                return;
            }

            var response = await _userService.RegisterAsync(Username, Email, Password);

            if (response)
            {
                await _messageService.DisplaySuccessMessage("Account created");
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await _messageService.DisplayErrorMessage("Invalid input");
            }
        }
    }
}
