using System.Collections.Generic;

namespace QuizAppMobile.Models.SignalR
{
    public class Lobby
    {
        public string OwnerUsername { get; set; }
        public int UsersConnectedAtStart { get; set; }
        public List<string> ConnectedUsers { get; set; } = new List<string>();
    }
}
