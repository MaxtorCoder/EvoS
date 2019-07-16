using System;

namespace EvoS.Framework.Network.WebSocket
{
    [Serializable]
    public abstract class WebSocketResponseMessage : WebSocketMessage
    {
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; } = "";
    }
}
