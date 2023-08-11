
using System.ComponentModel;
namespace SignalRWebClient.Models
{
    public class Responce<T>
    {
        public T Result { get; set; }
        public bool IsCompletedSuccessfully { get; set; }
    }

    public class City
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public int population { get; set; }
        public DateTime fondation { get; set; }
    }

}