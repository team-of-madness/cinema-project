using System.ComponentModel.DataAnnotations;

namespace cinema_project.Models
{
    public class Session
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "This field is required!!")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "This field is required!!")]
		public DateTime EndDate { get; set; }

		// Зовнішній ключ

		[Required(ErrorMessage = "This field is required!!")]
		public int HallId { get; set; }

		[Required(ErrorMessage = "This field is required!!")]
		public int MovieId { get; set; }

        // Навігаційна властивість
        public Hall? Hall { get; set; }
        public Movie? Movie { get; set; }

        public List<Ticket>? Tickets { get; set; }
    }
}
