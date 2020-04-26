using EvoS.Framework.Network;
using EvoS.Framework.Network.Game;
using EvoS.Framework.Network.Game.Messages;
using EvoS.Framework.Network.Unity.Messages;

namespace EvoS.Framework.Game
{
    public class GamePlayer
    {
        public GameServerConnection Connection { get; }
        public int PlayerId;
        public long AccountId;
        //public SessionPlayerInfo PlayerInfo;
        public AddPlayerMessage AddPlayerMessage { get; }

        public bool IsLoading;

        public GamePlayer(GameServerConnection connection, int playerId, long accountID)
        {
            Connection = connection;
            IsLoading = true;
            PlayerId = playerId;
            AccountId = accountID;
        }
    }
}
