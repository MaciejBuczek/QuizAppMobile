using QuizAppMobile.Services.Implementations;
using QuizAppMobile.Services.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizAppMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SetDependencies();

            MainPage = new NavigationPage(new StartPage());
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
        }
    }
}
