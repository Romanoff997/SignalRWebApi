using Microsoft.AspNetCore.SignalR.Client;
using SignalRWebApi.Client.Models;
using SignalRWebApi.Shared.Interface;
using System;
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

        public event Action<CityViewModel> CityCreated;
        public event Action<CityViewModel> CityUpdated;
        public event Action<Guid> CityDeleted;

        public CitiesService(HttpClient httpClient, HubConnection hubConnection, IJsonConverter converter)
        {
            _httpClient = httpClient;
            _hubConnection = hubConnection;
            _converter = converter;


            _hubConnection.On<CityViewModel>("CreateCity", item => CityCreated?.Invoke(item));
            _hubConnection.On<CityViewModel>("UpdateCity", item => CityUpdated?.Invoke(item));
            _hubConnection.On<Guid>("DeleteCity", id => CityDeleted?.Invoke(id));

            _hubConnection.StartAsync();
        }
        ~CitiesService() 
        {

            Dispose(false);
        }


        public async Task<List<CityViewModel>> GetListCity()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<CityViewModel>>("/api/city");
                return response;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<CityViewModel>();
            }
        }
        public async Task<CityViewModel> GetCity(Guid id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/city/{id}");
            string responseBody = await response.Content.ReadAsStringAsync();
            return _converter.ReadJson<CityViewModel>(responseBody);
        }

        public async Task CreateCity(CityViewModel city)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            await _httpClient.PostAsync("/api/city", new StringContent(_converter.WriteJson(city), Encoding.UTF8, "application/json"));
        }

        public async Task UpdateCity(CityViewModel city)
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
