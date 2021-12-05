using FontAwesome;
using QuizAppMobile.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QuizAppMobile.ViewModels
{
    public class LobbyVM : BindableObject
    {
        private string _totalTime;

        public string Title { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public double Rating { get; set; }

        public int TotalQuestions { get; set; }

        public string TotalTime 
        {
            get
            {
                return $"{_totalTime}s";
            }
            set
            {
                _totalTime = value;
            } 
        }

        public double TotalPoints { get; set; }

        public StackLayout RatingBox { get; set; }

        public void RenderRatings()
        {
            var filledStars = Rating;
            var emptyStars = 5 - filledStars;

            for (int i = 0; i < filledStars; i++)
            {
                var filledStarLabel = new Label
                {
                    Text = FontAwesomeIcons.Star,
                    FontFamily = Device.RuntimePlatform == Device.Android ? AndroidFonts.Solid : string.Empty
                };
                RatingBox.Children.Add(filledStarLabel);
            }
            for (int i = 0; i < emptyStars; i++)
            {
                var emptyStarLabel = new Label
                {
                    Text = FontAwesomeIcons.Star,
                    FontFamily = Device.RuntimePlatform == Device.Android ? AndroidFonts.Regular : string.Empty
                };
                RatingBox.Children.Add(emptyStarLabel);
            }
        }
    }
}
