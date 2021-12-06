using QuizAppMobile.Models.API;
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
    public partial class LobbyPage : ContentPage
    {
        public LobbyPage(JoinResponse obj, string lobbyCode)
        {
            InitializeComponent();

            var vm = new LobbyVM()
            {
                LobbyCode = lobbyCode,
                Title = obj.Title,
                Description = obj.Description,
                CreatedBy = obj.CreatedBy,
                CreatedOn = obj.CreatedOn,
                Rating = obj.Rating,
                TotalQuestions = obj.TotalQuestions,
                TotalPoints = obj.TotalPoints,
                TotalTime = obj.TotalTime.ToString(),
                RatingBox = RatingBox
            };

            BindingContext = vm;

            vm.RenderRatings();
            vm.StartConnection();
        }
    }
}