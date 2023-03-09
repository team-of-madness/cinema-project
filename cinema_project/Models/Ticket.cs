namespace cinema_project.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int Place { get; set; }
        public int UserId { get; set; }

        // Зовнішній ключ
        public int SessionId { get; set; }

        // Навігаційна властивість
        public Session Session { get; set; }
    }
}
