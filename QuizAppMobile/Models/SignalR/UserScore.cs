using System;
using System.Collections.Generic;
using System.Text;

namespace QuizAppMobile.Models.SignalR
{
    public class UserScore
    {
        public string Username { get; set; }

        public double Score { get; set; }

        public bool IsModiefied { get; set; }
    }
}
