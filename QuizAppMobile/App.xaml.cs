using QuizAppMobile.Services.Implementations;
using QuizAppMobile.Services.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QuizAppMobile.Constants;
using QuizAppMobile.Views;

namespace QuizAppMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SetDependencies();
            Current.Properties[Constants.Properties.Username] = "Wydra";
            Page nextPage;
            if (Current.Properties.ContainsKey(Constants.Properties.Username))
                nextPage = new HomePage();
            else
                nextPage = new StartPage();
            MainPage = new NavigationPage(nextPage);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void SetDependencies() 
        {
            DependencyService.Register<IMessageService, AlertMessageService>();
            DependencyService.Register<IUserService, UserAPIService>();
            DependencyService.Register<IHttpClientProvider, UnsafeHttpClientProvider>();
            DependencyService.Register<ILobbyService, LobbyAPIService>();
            DependencyService.Register<IHubConnectionProvider, UnsafeConnectionProvider>();
        }
    }
}
