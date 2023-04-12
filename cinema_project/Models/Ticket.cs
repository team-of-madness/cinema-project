using System.ComponentModel.DataAnnotations;

namespace cinema_project.Models
{
    public class Ticket
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "This field is required!!")]
		[StringLength(30, ErrorMessage = "This username is too long!")]
		[RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "The user name can contain only letter characters and spaces!!")]
		public string UserName { get; set; }

		// Зовнішній ключ
		[Required(ErrorMessage = "This field is required!!")]
		public int SessionId { get; set; }

		[Required(ErrorMessage = "This field is required!!")]
		public int PlaceId { get; set; }

        // Навігаційна властивість

        public Seat? Seat { get; set; }
        public Session? Session { get; set; }
    }
}
