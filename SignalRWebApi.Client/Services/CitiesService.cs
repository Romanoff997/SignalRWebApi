using Microsoft.AspNetCore.SignalR.Client;
using SignalRWebApi.Client.Models;
using SignalRWebApi.Shared.Interface;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace SignalRWebApi.Client.Services
{
    public class CitiesService: IDisposable
    {
        private bool disposed=false;
        private readonly HttpClient _httpClient;
        private readonly HubConnection _hubConnection;
        private readonly IJsonConverter _converter;

        public event Action<CityClient> CityCreated;
        public event Action<CityClient> CityUpdated;
        public event Action<Guid> CityDeleted;

        public CitiesService(HttpClient httpClient, HubConnection hubConnection, IJsonConverter converter)
        {
            _httpClient = httpClient;
            _hubConnection = hubConnection;
            _converter = converter;


            _hubConnection.On<CityClient>("CreateCity", item => CityCreated?.Invoke(item));
            _hubConnection.On<CityClient>("UpdateCity", item => CityUpdated?.Invoke(item));
            _hubConnection.On<Guid>("DeleteCity", id => CityDeleted?.Invoke(id));

            _hubConnection.StartAsync();
        }
        ~CitiesService() 
        {

            Dispose(false);
        }


        public async Task<List<CityClient>> GetListCity()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<CityClient>>("/api/city");
                return response;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<CityClient>();
            }
        }
        public async Task<CityClient> GetCity(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<CityClient>($"/api/city/{id}");
        }

        public async Task CreateCity(CityClient city)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            await _httpClient.PostAsync("/api/city", new StringContent(_converter.WriteJson(city), Encoding.UTF8, "application/json"));
        }

        public async Task UpdateCity(CityClient city)
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
