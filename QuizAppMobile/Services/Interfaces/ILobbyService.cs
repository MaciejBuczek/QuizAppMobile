using QuizAppMobile.Models.API;
using System.Threading.Tasks;

namespace QuizAppMobile.Services.Interfaces
{
    public interface ILobbyService
    {
        Task<bool> ValidateLobbyCode(string lobbyCode);

        Task<JoinResponse> ConnectToLobby(string lobbyCode);
    }
}
