using System;

namespace cinema_project.Models
{
    public class Session
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Зовнішній ключ
        public int MovieId { get; set; }

        // Навігаційна властивість
        //public Movie Movie { get; set; }

        //public List<Ticket> Tickets { get; set; }
    }
}
