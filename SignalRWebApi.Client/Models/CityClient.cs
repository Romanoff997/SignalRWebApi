using SignalRWebApi.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SignalRWebApi.Client.Models
{
    public class CityClient: City
    {

        [DisplayName("Название города")]
        [Required, StringLength(50, ErrorMessage = "Введите название длинной не более 50 символов")]

        public override string name { get; set; }


        [DisplayName("Колличество жителей")]
        [Required(ErrorMessage = "Пропещенно поле population")]

        public override int population { get; set; }


        [DisplayName("Дата основания")]
        [Required(ErrorMessage = "Не заполнена дата основания")]

        public override DateTime fondation { get; set; }
    }
}
