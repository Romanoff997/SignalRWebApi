namespace SingnalRWebApi.Shared.Models
{
    public class City
    {
        #if SERVER
                [Key]
                [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        #elif CLIENT
                [DisplayName("Название города")]
                [Required, StringLength(50, ErrorMessage = "Введите название длинной не более 50 символов")]
        #endif
        public Guid id { get; set; }

        #if CLIENT
                [DisplayName("Название города")]
                [Required, StringLength(50, ErrorMessage = "Введите название длинной не более 50 символов")]
        #endif
        public string name { get; set; }

        #if CLIENT
                [DisplayName("Колличество жителей")]
                [Required(ErrorMessage = "Пропещенно поле population")]
        #endif
        public int population { get; set; }

        #if CLIENT
                [DisplayName("Дата основания")]
                [Required(ErrorMessage = "Не заполнена дата основания")]
        #endif
        public DateTime fondation { get; set; }

    }

}
