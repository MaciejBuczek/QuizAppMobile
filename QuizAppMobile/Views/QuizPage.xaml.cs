using QuizAppMobile.Services.Connections;
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
    public partial class QuizPage : ContentPage
    {
        private bool _stopTimer = false;

        public QuizPage(string lobbyCode)
        {
            InitializeComponent();


            var vm = new QuizVM()
            {
                LobbyCode = lobbyCode,
                Title = "Test Quiz",
                Question = "Vestibulum at lectus rutrum, pulvinar nulla mattis, ornare purus. In condimentum consequat sodales. Cras nec auctor leo. Morbi cursus augue sit amet metus congue porttitor nec mollis nibh. Donec bibendum lectus eget tellus mattis.",
                StartTimerAction = StartTimer,
                StopTimerAction = StopTimer
            };

            BindingContext = vm;
        }

        private void StartTimer(int totalDuration)
        {
            int duration = 0;
            _stopTimer = false;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                TimerLabel.Text = GenerateFormattedTime(duration, totalDuration);
                duration++;

                if(_stopTimer || totalDuration == duration)
                {
                    return false;
                }
                return true;
            });
        }

        private void StopTimer()
        {
            Console.WriteLine("Stop!");
            _stopTimer = true;
        }

        private string GenerateFormattedTime(int duration, int totalDuration)
        {
            int minutes = (totalDuration - duration) / 60;
            int seconds = (totalDuration - duration) % 60;
            string minutesString = minutes < 10 ? $"0{minutes}" : minutes.ToString();
            string secondsString = seconds < 10 ? $"0{seconds}" : seconds.ToString();
            return $"{minutesString}:{secondsString}";
        }
    }
}