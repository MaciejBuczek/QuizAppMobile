using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuizAppMobile.Services.Implementations
{
    public class Timer : CancellationTokenSource
    {
        private readonly Action _finishAction;
        private readonly Action<int> _midAction;
        private int _duration;
        private int _totalDuration;

        public delegate void OnTick(string time);
        public OnTick OnTickDelegate;


        public Timer(Action finishingAction, Action<int> midAction)
        {
            _finishAction = finishingAction;
            _midAction = midAction;
        }


        public void Start(int totalDuration)
        {
            _totalDuration = totalDuration;
            _duration = 0;
            
            Task.Run(() =>
            {
                while(_totalDuration > _duration && !IsCancellationRequested)
                {
                    Task.Delay(1000, Token).Wait();
                    _duration++;
                    Console.WriteLine(_duration);
                    _midAction(_duration);
                }
                _finishAction();            
            });
        }

        public void Stop()
        {
            Cancel();
        }

        private string GenerateFormattedTime()
        {
            Console.WriteLine("testaaaaa");
            int minutes = (_totalDuration - _duration) / 60;
            int seconds = (_totalDuration - _duration) % 60;
            string minutesString = minutes < 10 ? $"0{minutes}" : minutes.ToString();
            string secondsString = seconds < 10 ? $"0{seconds}" : seconds.ToString();
            return $"{minutes}:{seconds}";
        }
    }
}
