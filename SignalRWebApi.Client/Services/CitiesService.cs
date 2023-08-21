using Microsoft.AspNetCore.SignalR.Client;
using SingnalRWebApi.Shared.Interface;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using SingnalRWebApi.Shared.Models;

namespace SignalRWebApi.Client.Services
{
    public class CitiesService: IDisposable
    {
        private bool disposed=false;
        private readonly HttpClient _httpClient;
        private readonly HubConnection _hubConnection;
        private readonly IJsonConverter _converter;

        public event Action<City> CityCreated;
        public event Action<City> CityUpdated;
        public event Action<Guid> CityDeleted;

        public CitiesService(HttpClient httpClient, HubConnection hubConnection, IJsonConverter converter)
        {
            _httpClient = httpClient;
            _hubConnection = hubConnection;
            _converter = converter;


            _hubConnection.On<City>("CreateCity", item => CityCreated?.Invoke(item));
            _hubConnection.On<City>("UpdateCity", item => CityUpdated?.Invoke(item));
            _hubConnection.On<Guid>("DeleteCity", id => CityDeleted?.Invoke(id));

            _hubConnection.StartAsync();
        }
        ~CitiesService() 
        {

            Dispose(false);
        }


        public async Task<List<City>> GetListCity()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<City>>("/api/city");
                return response;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<City>();
            }
        }
        public async Task<City> GetCity(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<City>($"/api/city/{id}");
        }

        public async Task CreateCity(City city)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            await _httpClient.PostAsJsonAsync("/api/city", city);
        }

        public async Task UpdateCity(City city)
        {
            await _httpClient.PutAsJsonAsync($"/api/city/{city.id}", city);
        }

        public async Task DeleteCity(Guid id)
        {
            await _httpClient.DeleteAsync($"/api/city/{id}");
        }

        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                _hubConnection.StopAsync();
            }
            disposed = true;
        }
    }

}
