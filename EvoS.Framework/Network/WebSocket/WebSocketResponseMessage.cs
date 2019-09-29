using System;

namespace EvoS.Framework.Network.WebSocket
{
    [Serializable]
    public abstract class WebSocketResponseMessage : WebSocketMessage
    {
        public string ErrorMessage { get; set; }
        public bool Success { get; set; } = true;
    }
}
