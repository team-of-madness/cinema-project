using System.ComponentModel.DataAnnotations;

namespace cinema_project.Models
{
    public class Seat
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "This field is required!!")]
        [Range(0, 9, ErrorMessage = "The row number couldnt be more than 10")]
        public int Row { get; set; }

		[Required(ErrorMessage = "This field is required!!")]
        [Range(0, 11, ErrorMessage = "The column number couldnt be more than 10")]
        public int Column { get; set; }

		[Required(ErrorMessage = "This field is required!!")]
		public int HallId { get; set; }
        
        public Hall? Hall { get; set; }

        public List<Ticket>? Tickets { get; set; }
    }
}
