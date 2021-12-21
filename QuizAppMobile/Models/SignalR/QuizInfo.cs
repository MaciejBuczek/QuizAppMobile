using System;
using System.Collections.Generic;
using System.Text;

namespace QuizAppMobile.Models.SignalR
{
    public class QuizInfo
    {
        public string QuizTitle { get; set; }

        public List<UserScore> UsersScores { get; set; }
    }
}
