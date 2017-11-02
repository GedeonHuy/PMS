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
        public Task LoadData()
        {
            return Clients.All.InvokeAsync("LoadData");
        }

    }
}