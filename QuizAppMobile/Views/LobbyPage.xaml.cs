using QuizAppMobile.Models.API;
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
    public partial class LobbyPage : ContentPage
    {
        public LobbyPage(JoinResponse obj)
        {
            InitializeComponent();
        }
    }
}