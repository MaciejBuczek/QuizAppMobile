using System;
using System.Collections.Generic;
using System.Text;

namespace QuizAppMobile.Models.API
{
    public class JoinResponse
    {
        public double Rating { get; set; }

        public int TotalQuestions { get; set; }

        public int TotalTime { get; set; }

        public double TotalPoints { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}
