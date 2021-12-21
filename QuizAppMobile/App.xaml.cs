using QuizAppMobile.Services.Connections;
using QuizAppMobile.Services.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QuizAppMobile.Constants;
using QuizAppMobile.Views;
using QuizAppMobile.Services.Implementations;

namespace QuizAppMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SetDependencies();
            Current.Properties[Constants.Properties.Username] = "Wydra";
            Current.Properties[Constants.Properties.UserId] = "913e7d37-6421-4220-8c78-9c36c90d8a4d";
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
            DependencyService.Register<ISummaryService, SummaryAPIService>();
        }
    }
}
