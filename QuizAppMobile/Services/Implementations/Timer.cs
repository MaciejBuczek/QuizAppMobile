using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuizAppMobile.Services.Implementations
{
    public class Timer : CancellationTokenSource
    {
        private readonly Action _finishAction;
        private int duration;

        public string TimerFormatted { 
            get 
            {
                int minutes = duration / 60;
                int seconds = duration % 60;
                string minutesString = minutes < 10 ? $"0{minutes}" : minutes.ToString();
                string secondsString = seconds < 10 ? $"0{seconds}" : seconds.ToString();
                return $"{minutesString}:{secondsString}";
            }
        }

        public Timer(Action finishingAction)
        {
            _finishAction = finishingAction;
        }


        public void Start(int totalDuration)
        {
            duration = 0;
            
            Task.Run(() =>
            {
                while(totalDuration > duration && !IsCancellationRequested)
                {
                    Task.Delay(1000, Token);
                }
                _finishAction();             
            });
        }

        public void Stop()
        {
            Cancel();
        }
    }
}
