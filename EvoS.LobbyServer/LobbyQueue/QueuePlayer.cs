using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.LobbyServer.LobbyQueue
{
    class QueuePlayer : QueueParticipant
    {
        public ClientConnection Client;
        public bool IsQueued;
        public bool IsGroupLeader;

        public QueuePlayer(ClientConnection client)
        {
            Client = client;
            IsGroupLeader = false;
        }

        public override bool IsGroup()
        {
            return false;
        }
    }
}
