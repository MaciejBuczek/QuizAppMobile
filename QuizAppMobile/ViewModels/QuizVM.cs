using QuizAppMobile.Models;
using QuizAppMobile.Services.Implementations;
using QuizAppMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public Timer Timer { get; }

        public ObservableCollection<Answer> Answers {get; set;}

        public ObservableCollection<UserScore> Users { get; set; }

        public ICommand Test { get; set; }

        public QuizVM()
        {
            Timer = new Timer(async () => await SubmitAnswer());
            _messageService = DependencyService.Resolve<IMessageService>();
            Test = new Command(TestAct);

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

        public void TestAct()
        {
            Timer.Start(10);
        }

        public async Task SubmitAnswer()
        {
            await _messageService.DisplaySuccessMessage("test");
        }
    }
}
