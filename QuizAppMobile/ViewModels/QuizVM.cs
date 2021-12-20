using QuizAppMobile.Models;
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
    class QuizVM : BindableObject
    {

        private readonly IMessageService _messageService;

        public string LobbyCode { get; set; }

        public string Title { get; set; }

        public string Question { get; set; }

        public int QuestionTime { get; set; }

        public ObservableCollection<Answer> Answers {get; set;}

        public ObservableCollection<UserScore> Users { get; set; }

        public string TimeLeft { get; set; }

        public ICommand StartTimerCommand { get; set; }

        public ICommand StopTimerCommand { get; set; }

        public Action<int> StartTimerAction { get; set; }

        public Action StopTimerAction { get; set; }

        public QuizVM()
        {
            _messageService = DependencyService.Resolve<IMessageService>();
            StartTimerCommand = new Command(StartTimer);
            StopTimerCommand = new Command(StopTimer);

            Answers = new ObservableCollection<Answer>()
            {
                new Answer
                {
                    Selected = false,
                    Content = "non blandit leo pharetra. Vivamus facilisis risus quis neque venenatis"
                },
                new Answer
                {
                    Selected = false,
                    Content = "quis suscipit dui euismod"
                },
                new Answer
                {
                    Selected = false,
                    Content = "Quisque efficitur tincidunt quam, vitae dictum libero condimentum sed. Etiam nulla sem, luctus sit amet varius non, venenatis nec magna. Mauris tempor egestas tellus, a commodo ante varius eget."
                }
            };
            Users = new ObservableCollection<UserScore>()
            {
                new UserScore
                {
                    Score = 5,
                    Username = "Wydra"
                },
                new UserScore
                {
                    Score = 4.5d,
                    Username = "Rosomak"
                }
            };
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
