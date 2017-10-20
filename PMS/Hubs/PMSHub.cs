using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PMS.Hubs
{
    public class PMSHub : Hub
    {
        public Task Send(string data)
        {
            return Clients.All.InvokeAsync("Send", data);
        }
    }
}