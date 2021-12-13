using QuizAppMobile.Services.Interfaces;
using QuizAppMobile.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizAppMobile.ViewModels
{
    public class HomeVM
    {
        private readonly IMessageService _messageService;
        private readonly ILobbyService _lobbyService;

        public HomeVM()
        {
            if (Application.Current.Properties.TryGetValue(Constants.Properties.Username, out var username))
            {
                WelcomeMessage = $"Hello {(string)username}!";
                Connect = new Command(async () => await OnConnect());

                _messageService = DependencyService.Resolve<IMessageService>();
                _lobbyService = DependencyService.Resolve<ILobbyService>();
            }
            else
                Application.Current.MainPage.Navigation.PushAsync(new StartPage());
        }

        public string WelcomeMessage { get; }

        public string LobbyCode { get; set; }

        public ICommand Connect { get; }

        private async Task OnConnect()
        {
            if (await _lobbyService.ValidateLobbyCode(LobbyCode))
            {
                var response = await _lobbyService.ConnectToLobby(LobbyCode);
                if (response != null)
                    await Application.Current.MainPage.Navigation.PushAsync(new LobbyPage(response, LobbyCode));
                else
                    await _messageService.DisplayErrorMessage("Cannot connect to lobby");
            }
            else
                await _messageService.DisplayErrorMessage("Invalid lobby code");
        }
    }
}
