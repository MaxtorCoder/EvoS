using EvoS.Framework;
using EvoS.Framework.Logging;
using System;
using System.Net;
using System.Threading;
using vtortola.WebSockets;
using vtortola.WebSockets.Rfc6455;

namespace EvoS.LobbyServer
{
    class Program
    {
        static ILog Log = new Log();

        static void Main(string[] args)
        {
            var options = new WebSocketListenerOptions();
            options.Standards.RegisterRfc6455();

            var socket = new WebSocketListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6060), options);
            var listenThread = new Thread(Listen);
            listenThread.Start(socket);

            Console.CancelKeyPress += (sender, @event) =>
            {
                socket.StopAsync();
                socket.Dispose();
            };
        }

        private static async void Listen(object obj)
        {
            if (!(obj is WebSocketListener socket)) return;

            await socket.StartAsync();
            Log.Print(LogType.Server, "Started webserver on '0.0.0.0:6060'");

            try
            {
                while (true)
                {
                    var connection = await socket.AcceptWebSocketAsync(CancellationToken.None);
                    new Thread(Dispatch).Start(connection);
                }
            }
            catch (Exception ex)
            {
                Log.Print(LogType.Error, ex.ToString());
                socket.Dispose();
            }
        }

        private static async void Dispatch(object obj)
        {
            if (!(obj is WebSocket socket)) return;
            try
            {
                while (true)
                {
                    var message = await socket.ReadMessageAsync(CancellationToken.None);
                    Console.WriteLine($"RECV {message.MessageType} {(message.MessageType == WebSocketMessageType.Text ? message.ReadText() : message.ReadBinary())}");
                    message.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.Print(LogType.Error, ex.ToString());
                socket.Dispose();
            }
        }
    }
}
