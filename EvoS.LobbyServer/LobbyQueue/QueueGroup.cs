using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.LobbyServer.LobbyQueue
{
    class QueueGroup : QueueParticipant
    {
        public QueuePlayer[] players;

        public override bool IsGroup()
        {
            return true;
        }

        public bool IsGroupQueued()
        {
            bool queued = true;
            for (int i = 0; i < players.Length; i++)
            {
                if (!players[i].IsQueued)
                {
                    queued = false;
                    break;
                }
            }

            return queued;
        }

        public int GetSize()
        {
            return players.Length;
        }

        public void AddPlayerToGroup(ClientConnection client)
        {
            QueuePlayer player = new QueuePlayer(client);
            player.IsOnGroup = true;
        }
    }
}
