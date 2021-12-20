using QuizAppMobile.Constants;
using QuizAppMobile.Models;
using QuizAppMobile.Models.SignalR;
using QuizAppMobile.Services.Connections;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizAppMobile.ViewModels
{
    public class QuizVM : BindableObject
    {

        private readonly IMessageService _messageService;

        private readonly QuizConnection _quizConnection;

        private string _title;

        private string _question;

        public string LobbyCode { get; set; }

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

        public string Question
        {
            get
            {
                return _question;
            }
            set
            {
                _question = value;
                OnPropertyChanged(nameof(Question));
            }
        }

        public int QuestionTime { get; set; }

        public ObservableCollection<Answer> Answers {get; set;} = new ObservableCollection<Answer>();

        public ObservableCollection<Models.SignalR.UserScore> Users { get; set; } = new ObservableCollection<Models.SignalR.UserScore>();

        public Action<int> StartTimerAction { get; set; }

        public Action StopTimerAction { get; set; }

        public ICommand SubmitAnswersCommand { get; set; }

        public QuizVM()
        {
            _messageService = DependencyService.Resolve<IMessageService>();
            SubmitAnswersCommand = new Command(new Action(SubmitAnswers));

            _quizConnection = new QuizConnection
                (
                    InitializeQuiz,
                    UpdateScoreboard,
                    LoadQuestion,
                    new Action<string>(async (message) => await DisplayError(message)),
                    new Action(async () => await RequestQuestion()),
                    new Action(async () => await RedirectToSummary()),
                    new Action(async () => await BeginQuiz())
                );
        }

        private void InitializeQuiz(QuizInfo quizInfo)
        {
            Title = quizInfo.QuizTitle;
            Users.Clear();
            quizInfo.UsersScores.ForEach(us => Users.Add(us));
        }

        private void UpdateScoreboard(List<Models.SignalR.UserScore> userScores)
        {

        }

        private void LoadQuestion(PersonalisedQuestion question)
        {
            Question = question.Question;
            Answers.Clear();
            question.Answers.Select(a => new Answer
            {
                Content = a,
                Selected = false
            }).
            ToList()
            .ForEach(a => Answers.Add(a));
            StartTimerAction(question.Time);
        }

        private async Task DisplayError(string message)
        {
            await _messageService.DisplayErrorMessage(message);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task RequestQuestion()
        {
            await _quizConnection.GetQuestion();
        }

        private async Task RedirectToSummary()
        {

        }

        private async Task BeginQuiz()
        {
            await RequestQuestion();
        }

        public async void SubmitAnswers()
        {
            StopTimerAction();
            var selectedAnswers = new List<int>();
            for(int i = 0; i < Answers.Count; i++)
            {
                if (Answers[i].Selected)
                    selectedAnswers.Add(i);
            }
            await _quizConnection.SubmitAnswers(selectedAnswers.ToArray());
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
    }
}
