namespace QuizAppMobile.Constants
{
    public static class APIConfigs
    {
        public static readonly string BaseURL = "http://10.0.2.2:50756/";

        public static readonly string RegisterURL = "User/Register";

        public static readonly string LoginURL = "User/Login";

        public static readonly string CheckLobbyCodeURL = "Lobby/CheckCode";

        public static readonly string JoinURL = "Lobby/JoinAPI";

        public static readonly string LobbyHubURL = "hubs/lobby";

        public static readonly string QuizHubURL = "hubs/quiz";
    }
}
