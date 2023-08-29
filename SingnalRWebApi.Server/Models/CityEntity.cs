using SignalRWebApi.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRWebApi.Server.Models
{
    public class CityEntity: City
    {

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid id { get; set; }


    }
}
