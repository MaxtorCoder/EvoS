using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(395)]
    public class UpdateUIStateRequest : WebSocketMessage
    {
        public AccountComponent.UIStateIdentifier UIState;
        public int StateValue;
    }
}
