using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Constants.Enums;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;


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
                MemoryStream ms = new MemoryStream();
                context.Request.Body.CopyTo(ms);
                ms.Position = 0;
                string requestBody = new StreamReader(ms).ReadToEnd(); ;
                ms.Dispose();

                AssignGameClientRequest request = JsonConvert.DeserializeObject<AssignGameClientRequest>(requestBody);
                AssignGameClientResponse response = new AssignGameClientResponse();
                response.RequestId = request.RequestId;
                response.ResponseId = request.ResponseId;
                response.Success = true;
                response.ErrorMessage = "";

                response.SessionInfo = request.SessionInfo;
                response.SessionInfo.ConnectionAddress = "127.0.0.1";

                response.SessionInfo.ProcessCode = "This is the process code";
                response.LobbyServerAddress = "127.0.0.1";
                response.SessionInfo.FakeEntitlements = "";

                LobbyGameClientProxyInfo proxyInfo = new LobbyGameClientProxyInfo();
                proxyInfo.AccountId = request.SessionInfo.AccountId;
                proxyInfo.SessionToken = request.SessionInfo.SessionToken;
                proxyInfo.AssignmentTime = 1565574095;
                proxyInfo.Handle = request.SessionInfo.Handle;
                proxyInfo.Status = ClientProxyStatus.Assigned;

                response.ProxyInfo = proxyInfo;

                return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            });
        }
    }
}
