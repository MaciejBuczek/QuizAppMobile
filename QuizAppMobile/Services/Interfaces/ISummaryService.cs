using QuizAppMobile.Models.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppMobile.Services.Interfaces
{
    public interface ISummaryService
    {
        Task<(bool Succeded, List<UserScore> UsersScores)> GetSummaryInfo(string lobbyCode);
    }
}
