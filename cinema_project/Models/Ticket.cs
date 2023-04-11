namespace cinema_project.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        // Зовнішній ключ
        public int SessionId { get; set; }
        public int PlaceId { get; set; }

        // Навігаційна властивість

        public Seat? Seat { get; set; }
        public Session? Session { get; set; }
    }
}
