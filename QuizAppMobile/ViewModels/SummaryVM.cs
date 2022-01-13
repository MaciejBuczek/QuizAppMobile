using QuizAppMobile.Constants;
using QuizAppMobile.Models;
using QuizAppMobile.Models.SignalR;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace QuizAppMobile.ViewModels
{
    public class SummaryVM : BindableObject
    {
        private readonly ISummaryService _summaryService;
        private readonly IMessageService _messageService;

        private double _score;

        private string _place;

        public string LobbyCode { get; set; }

        public ObservableCollection<UserScorePlace> UsersScoresPlaces { get; set; } = new ObservableCollection<UserScorePlace>();

        public double Score
        {
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
            get
            {
                return _score;
            }
        }

        public string Place
        {
            set
            {
                _place = value;
                OnPropertyChanged(nameof(Place));
            }
            get
            {
                return _place;
            }
        }

        public SummaryVM()
        {
            _summaryService = DependencyService.Resolve<ISummaryService>();
            _messageService = DependencyService.Resolve<IMessageService>();
        }

        public async void GetSummaryInfo()
        {
            var response = await _summaryService.GetSummaryInfo(LobbyCode);

            if (response.Succeded)
            {
                Score = GetScore(response.UsersScores);
                Place = GetPlace(response.UsersScores);

                for (int i = 1; i <= response.UsersScores.Count; i++)
                {
                    UsersScoresPlaces.Add(new UserScorePlace()
                    {
                        Username = response.UsersScores[i - 1].Username,
                        Score = response.UsersScores[i - 1].Score,
                        Place = i
                    });
                }
            }
            else
            {
                await _messageService.DisplayErrorMessage("Lost connection to the server");
                await Application.Current.MainPage.Navigation.PopAsync();
            }   
        }

        private string GetPlace(List<Models.SignalR.UserScore> userScores)
        {
            if (userScores == null || userScores.Count == 0)
                return string.Empty;

            var username = (string)Application.Current.Properties[Properties.Username];
            int place = 0;
            for (int i = userScores.Count - 1; i >= 0; i--)
            {
                if (userScores[i].Username == username)
                    place = i;
            }
            place++;
            switch (place)
            {
                case 1:
                    return "1st";
                case 2:
                    return "2nd";
                case 3:
                    return "3rd";
                default:
                    return $"{place}th";
            }
        }

        private double GetScore(List<Models.SignalR.UserScore> userScores)
        {
            if (userScores == null || userScores.Count == 0)
                return 0;

            var username = (string)Application.Current.Properties[Properties.Username];
            return userScores.Where(us => us.Username == username).FirstOrDefault().Score;
        }
    }
}
