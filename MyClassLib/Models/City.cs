
namespace SignalRWebApi.Shared.Models
{
    public class City
    {
        public virtual Guid id { get; set; }

        public virtual string name { get; set; }

        public virtual int population { get; set; }

        public virtual DateTime fondation { get; set; }
    }

}
