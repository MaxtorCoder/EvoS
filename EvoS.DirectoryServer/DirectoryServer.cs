using EvoS.Framework.Network.NetworkMessages;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace EvoS.DirectoryServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                .SuppressStatusMessages(true)
                .UseKestrel(koptions => koptions.Listen(IPAddress.Parse("127.0.0.1"), 6050))
                .UseStartup<DirectoryServer>()
                .Build();

            Console.CancelKeyPress += async (sender, @event) =>
            {
                await host.StopAsync();
                host.Dispose();
            };
            
            host.Run();
        }
    }

    public class DirectoryServer
    {
        public void Configure(IApplicationBuilder app)
        {
            var serverAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();
            Console.WriteLine("Started DirectoryServer on '0.0.0.0:6050'");
            
            app.Run((context) =>
            {
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync("{\"SessionInfo\":{\"AccountId\":1,\"UserName\":\"a@b.c\",\"BuildVersion\":\"STABLE-122-100\",\"ProtocolVersion\":\"b486c83d8a8950340936d040e1953493\",\"SessionToken\":1,\"ReconnectSessionToken\":0,\"ProcessType\":\"AtlasReactor\",\"ConnectionAddress\":\"127.0.0.1\",\"Handle\":\"maxtorcoder1#857\",\"IsBinary\":true,\"Region\":0,\"LanguageCode\":\"en\"},\"LobbyServerAddress\":\"ws://127.0.0.1:6060/\",\"Success\":true,\"RequestId\":0,\"ResponseId\":2}");
            });
        }
    }
}
