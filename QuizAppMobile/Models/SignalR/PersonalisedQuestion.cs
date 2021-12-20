using System;
using System.Collections.Generic;
using System.Text;

namespace QuizAppMobile.Models.SignalR
{
    public class PersonalisedQuestion
    {
        public string Question { get; set; }

        public List<string> Answers { get; set; }

        public int Time { get; set; }
    }
}
