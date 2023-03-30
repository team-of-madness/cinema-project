namespace cinema_project.Models
{
    public class Seat
    {
        public int Id { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public int HallId { get; set; }
        
        public Hall Hall { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
