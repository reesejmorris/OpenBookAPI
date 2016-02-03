using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
namespace OpenBookAPI.SignalR.Hubs{
    [HubName("logHub")]
    public class LogHub : Hub
    {
        public void broadcastLogEvent(Serilog.Sinks.SignalR.Data.LogEvent log){
            Clients.All.broadcastLogEvent(log);
        }
    }
}