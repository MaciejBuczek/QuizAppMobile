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
    public class SummaryVM
    {
        private readonly ISummaryService _summaryService;
        private readonly IMessageService _messageService;

        public string LobbyCode { get; set; }

        public ObservableCollection<UserScorePlace> UsersScores { get; set; }

        public double Score
        {
            get
            {
                if (UsersScores == null || UsersScores.Count == 0)
                    return 0;

                var username = (string) Application.Current.Properties[Properties.Username];
                return UsersScores.Where(us => us.Username == username).FirstOrDefault().Score;
            }
        }

        public string Place
        {
            get
            {
                if (UsersScores == null || UsersScores.Count == 0)
                    return string.Empty;

                var username = (string)Application.Current.Properties[Properties.Username];
                int place = 0;
                for(int i = UsersScores.Count - 1; i>=0; i--)
                {
                    if (UsersScores[i].Username == username)
                        place = i;
                }
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
        }

        public SummaryVM()
        {
            UsersScores = new ObservableCollection<UserScorePlace>();
            _summaryService = DependencyService.Resolve<ISummaryService>();
            _messageService = DependencyService.Resolve<IMessageService>();
        }

        public async void GetSummaryInfo()
        {
            var response = await _summaryService.GetSummaryInfo(LobbyCode);

            if (response.Succeded)
            {
                for(int i = 1; i <= response.UsersScores.Count; i++)
                {
                    UsersScores.Add(new UserScorePlace
                    {
                        Username = response.UsersScores[i].Username,
                        Score = response.UsersScores[i].Score,
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
    }
}
