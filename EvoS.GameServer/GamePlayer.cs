using EvoS.GameServer.Network.Messages.GameManager;
using EvoS.GameServer.Network.Messages.Unity;

namespace EvoS.GameServer.Network
{
    public class GamePlayer
    {
        public ClientConnection Connection { get; }
        public LoginRequest LoginRequest { get; }
        public AddPlayerMessage AddPlayerMessage { get; }

        public GamePlayer(ClientConnection connection, LoginRequest loginRequest, AddPlayerMessage addPlayerMessage)
        {
            Connection = connection;
            LoginRequest = loginRequest;
            AddPlayerMessage = addPlayerMessage;
        }
    }
}
