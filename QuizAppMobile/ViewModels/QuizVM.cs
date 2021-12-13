using QuizAppMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace QuizAppMobile.ViewModels
{
    class QuizVM : BindableObject
    {
        public string LobbyCode { get; set; }

        public string Title { get; set; }

        public string Question { get; set; }

        public ObservableCollection<Answer> Answers {get; set;}

        public ObservableCollection<UserScore> Users { get; set; }

        public QuizVM()
        {
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
    }
}
