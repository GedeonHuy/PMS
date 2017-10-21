using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PMS.Models;
using PMS.Persistence;

namespace PMS.Hubs
{
    public class PMSHub : Hub
    {

        public Task Send(string mess)
        {
            return Clients.All.InvokeAsync("Send", mess);
        }
    }
}