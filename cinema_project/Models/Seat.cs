using System.ComponentModel.DataAnnotations;

namespace cinema_project.Models
{
    public class Seat
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "This field is required!!")]
		public int Row { get; set; }

		[Required(ErrorMessage = "This field is required!!")]
		public int Column { get; set; }

		[Required(ErrorMessage = "This field is required!!")]
		public int HallId { get; set; }
        
        public Hall? Hall { get; set; }

        public List<Ticket>? Tickets { get; set; }
    }
}
