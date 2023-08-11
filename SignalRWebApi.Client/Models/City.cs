
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SignalRWebApi.Client.Models
{
    public class Response<T>
    {
        public T Result { get; set; }
        public bool IsCompletedSuccessfully { get; set; }
    }

    public class City
    {
        public Guid id { get; set; }
        [DisplayName ("Название города")]
        [Required, StringLength(50, ErrorMessage = "Введите название длинной не более 50 символов")]
        public string name { get; set; }
        [DisplayName("Колличество жителей")]
        [Required(ErrorMessage = "Пропещенно поле population")]
        public int population { get; set; }
        [DisplayName("Дата основания")]
        [Required(ErrorMessage = "Не заполнена дата основания")]
        public DateTime fondation { get; set; }
    }

}