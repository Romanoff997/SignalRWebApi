using Microsoft.AspNetCore.SignalR;
using MyClassLib.Interface;
using SingnalRWebApi.Controllers;
using SingnalRWebApi.Domain.Repositories;
using SingnalRWebApi.Models;
using System.Text.Json;

namespace SingnalRWebApi.Hubs
{
    public class CityNotificationHub: Hub
    {
        protected IHubContext<CityNotificationHub> _context { get; set; }
        public CityNotificationHub(IHubContext<CityNotificationHub> context)
        {
            _context = context;
        }
        public async Task CreateCity(City city)
        {
            await _context.Clients.All.SendAsync("CreateCity", city);
        }
        public async Task DeleteCity(Guid cityId)
        {
            await _context.Clients.All.SendAsync("DeleteCity", cityId);
        }
        public async Task UpdateCity(City city)
        {
            await _context.Clients.All.SendAsync("UpdateCity", city);
        }


    }
}
