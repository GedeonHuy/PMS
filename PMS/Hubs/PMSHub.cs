using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PMS.Models;
using PMS.Persistence;

namespace PMS.Hubs
{
    public class PMSHub : Hub
    {

        public Task PushToAllUsers()
        {
            return Clients.All.InvokeAsync("Send");
        }

        // private static IHubContext<PMSHub> GetClients(PMSHub hub)
        // {
        //     if (hub == null)
        //         return GlobalHost.ConnectionManager.GetHubContext<PMSHub>().Clients;
        //     else
        //         return hub.Clients;
        // }


        /// <summary>
        /// Connect user to hub
        /// </summary>
        /// <returns></returns>
        // public Task OnConnectedAsync()
        // {
        //     await _connections.Add(Context.User.Identity.Name, Context.ConnectionId);

        //     return base.OnConnectedAsync();
        // }

        // public Task OnDisconnectedAsync(bool stopCalled)
        // {
        //      _connections.Remove(Context.User.Identity.Name, Context.ConnectionId);
        //     return base.OnDisconnectedAsync(stopCalled);
        // }

    }
}