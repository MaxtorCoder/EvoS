using EvoS.Framework.Network;
using EvoS.Framework.Network.Game;
using EvoS.Framework.Network.Game.Messages;
using EvoS.Framework.Network.Unity.Messages;

namespace EvoS.Framework.Game
{
    public class GamePlayer
    {
        public ClientConnection Connection { get; }
        public LoginRequest LoginRequest { get; }
        public AddPlayerMessage AddPlayerMessage { get; }

        public bool IsLoading;

        public GamePlayer(ClientConnection connection, LoginRequest loginRequest, AddPlayerMessage addPlayerMessage)
        {
            Connection = connection;
            LoginRequest = loginRequest;
            AddPlayerMessage = addPlayerMessage;
            IsLoading = true;
        }
    }
}
