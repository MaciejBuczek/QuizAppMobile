using QuizAppMobile.Constants;
using QuizAppMobile.Models;
using QuizAppMobile.Models.SignalR;
using QuizAppMobile.Services.Connections;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizAppMobile.ViewModels
{
    public class QuizVM : BindableObject
    {

        private readonly IMessageService _messageService;

        private QuizConnection _quizConnection;

        public string LobbyCode { get; set; }

        private string _title;

        public string Title {
            get
            {
                return _title;
            } 
            set 
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            } 
        }

        public string Question { get; set; }

        public int QuestionTime { get; set; }

        public ObservableCollection<Answer> Answers {get; set;} = new ObservableCollection<Answer>();

        public ObservableCollection<Models.SignalR.UserScore> Users { get; set; } = new ObservableCollection<Models.SignalR.UserScore>();

        public string TimeLeft { get; set; }

        public Action<int> StartTimerAction { get; set; }

        public Action StopTimerAction { get; set; }

        public QuizVM()
        {
            _messageService = DependencyService.Resolve<IMessageService>();

            _quizConnection = new QuizConnection
                (
                    InitializeQuiz,
                    UpdateScoreboard,
                    LoadQuestion,
                    new Action<string>(async (message) => await DisplayError(message)),
                    new Action(async () => await RequestQuestion()),
                    new Action(async () => await RedirectToSummary()),
                    BeginQuiz
                );
        }

        private void InitializeQuiz(QuizInfo quizInfo)
        {
            Title = quizInfo.QuizTitle;
            quizInfo.UsersScores.ForEach(us => Users.Add(us));
        }

        private void UpdateScoreboard(List<Models.SignalR.UserScore> userScores)
        {

        }

        private void LoadQuestion(PersonalisedQuestion question)
        {

        }

        private async Task DisplayError(string message)
        {

        }

        private async Task RequestQuestion()
        {

        }

        private async Task RedirectToSummary()
        {

        }

        private void BeginQuiz()
        {

        }

        public async void Connect()
        {
            if (Application.Current.Properties.TryGetValue(Properties.UserId, out var userId))
            {
                await _quizConnection.Connect(LobbyCode, (string)userId);
            }
            return;
        }

        private void StartTimer()
        {
            StartTimerAction(5);
        }

        private void StopTimer()
        {
            StopTimerAction();
        }

        public async Task SubmitAnswer()
        {
            await _messageService.DisplaySuccessMessage("test");
        }      
    }
}
