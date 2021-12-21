using QuizAppMobile.Models.SignalR;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace QuizAppMobile.ViewModels
{
    public class SummaryVM
    {
        private readonly ISummaryService _summaryService;
        private readonly IMessageService _messageService;

        public string LobbyCode { get; set; }

        public ObservableCollection<UserScore> UsersScores { get; set; }

        public SummaryVM()
        {
            UsersScores = new ObservableCollection<UserScore>();
            _summaryService = DependencyService.Resolve<ISummaryService>();
            _messageService = DependencyService.Resolve<IMessageService>();

            GetSummaryInfo();
        }

        private async void GetSummaryInfo()
        {
            var response = await _summaryService.GetSummaryInfo(LobbyCode);

            if (response.Succeded)
            {
                response.UsersScores.ForEach(us => UsersScores.Add(us));
            }
            else
            {
                await _messageService.DisplayErrorMessage("Lost connection to the server");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
