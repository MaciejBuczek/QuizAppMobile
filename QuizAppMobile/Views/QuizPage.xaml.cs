using QuizAppMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuizPage : ContentPage
    {
        public QuizPage(string lobbyCode)
        {
            InitializeComponent();

            var vm = new QuizVM
            {
                LobbyCode = lobbyCode,
                Title = "Test Quiz",
                Question = "Vestibulum at lectus rutrum, pulvinar nulla mattis, ornare purus. In condimentum consequat sodales. Cras nec auctor leo. Morbi cursus augue sit amet metus congue porttitor nec mollis nibh. Donec bibendum lectus eget tellus mattis."
            };

            BindingContext = vm;

        }
    }
}