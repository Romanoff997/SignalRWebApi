using Microsoft.AspNetCore.SignalR;
using SignalRWebApi.Server.Models;

namespace SignalRWebApi.Hubs
{
    public class CityNotificationHub: Hub
    {
        protected readonly IHubContext<CityNotificationHub> _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string currUser
        { 
            get 
            { 
                return _httpContextAccessor.HttpContext.User.Identity.Name;
            } 
        }
        public CityNotificationHub(IHubContext<CityNotificationHub> context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task CreateCity(CityEntity city)
        {
            await _context.Clients.AllExcept(currUser).SendAsync("CreateCity", city);
        }
        public async Task DeleteCity(Guid cityId)
        {
            await _context.Clients.AllExcept(currUser).SendAsync("DeleteCity", cityId);
        }
        public async Task UpdateCity(CityEntity city)
        {
            await _context.Clients.AllExcept(currUser).SendAsync("UpdateCity", city);
        }


    }
}
