using Microsoft.AspNet.SignalR;
using SpaceAlert.Business;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SpaceAlert.Web.Hubs
{
    public class PlayHub : AbstractHub
    {

        public void PopMenace(string gameId, string message)
        {
            Clients.Group(gameId).addChatMessage(message);
        }
    }
}