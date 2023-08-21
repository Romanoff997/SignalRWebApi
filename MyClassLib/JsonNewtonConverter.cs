
using SingnalRWebApi.Shared.Interface;
using Newtonsoft.Json;

namespace SingnalRWebApi.Shared.Services
{
    public class JsonNewtonConverter: IJsonConverter
    {
        public string WriteJson<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public T ReadJson<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
