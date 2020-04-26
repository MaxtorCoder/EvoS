using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralServer
{
    public delegate void EvosMessageDelegate<T>(T message) where T : WebSocketMessage;
}
