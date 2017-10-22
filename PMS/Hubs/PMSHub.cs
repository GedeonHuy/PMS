using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using PMS.Models;
using PMS.Persistence;

namespace PMS.Hubs
{
    public class PMSHub : Hub
    {
        // private readonly static ConnectionMapping<string> _connections =
        //     new ConnectionMapping<string>();
        public Task Send(Student name)
        {
            return Clients.All.InvokeAsync("Send", name);
        }

        // / <summary>
        // / Connect user to hub
        // / </summary>
        // / <returns></returns>
        // public override Task OnConnectedAsync()
        // {
        //     IdentityOptions _options = new IdentityOptions();

        //     _connections.Add(_options.ClaimsIdentity.UserIdClaimType, Context.ConnectionId);

        //     return base.OnConnectedAsync();
        // }
    }
}