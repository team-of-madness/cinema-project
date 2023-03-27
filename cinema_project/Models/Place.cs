namespace cinema_project.Models
{
    public class Place
    {
        public int PlaceId { get; set; }

        public int RowNumber { get; set; }

        public int PlaceNumber { get; set; }

        public int HallId { get; set; }
        
        public Hall Hall { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
