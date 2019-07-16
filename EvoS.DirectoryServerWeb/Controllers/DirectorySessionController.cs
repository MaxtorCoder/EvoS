using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Shared;
using EvoS.Framework.Network.Static;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net.Mime;
using System.IO;

namespace EvoS.DirectoryServerWeb.Controllers
{
    [Route("DirectorySessionManager"), ApiController]
    public class DirectorySessionController : ControllerBase
    {
        [HttpPost]
        public JsonResult Post()
        {
            StreamReader sr = new StreamReader(Request.Body);
            string requestJson = sr.ReadToEnd();

            var request = JsonConvert.DeserializeObject<AssignGameClientRequest>(requestJson);
            DateTime dateTime = DateTime.Now;
            DateTimeOffset dto = new DateTimeOffset(dateTime);
            long assignedTime = dto.ToUnixTimeSeconds();

            request.SessionInfo.IsBinary = false;
            request.SessionInfo.SessionToken = 1;

            var response = new AssignGameClientResponse()
            {
                SessionInfo             = request.SessionInfo,
                ProxyInfo = new LobbyGameClientProxyInfo()
                {
                    AccountId           = request.SessionInfo.AccountId,
                    SessionToken        = request.SessionInfo.SessionToken,
                    AssignmentTime      = assignedTime,
                    Handle              = request.SessionInfo.Handle,
                    Status              = ClientProxyStatus.Assigned
                },
                LobbyServerAddress      = "ws://127.0.0.1:6060/",
            };
            response.Success        = true;
            response.ErrorMessage   = "";
            response.RequestId      = request.RequestId;
            response.ResponseId     = request.ResponseId;

            JsonResult result = new JsonResult(response);

            return result;
        }
    }
}
