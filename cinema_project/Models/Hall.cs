namespace cinema_project.Models
{
    public class Hall
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public List<Session>? Sessions { get; set; }

        public List<Seat>? Seats { get; set; }
    }
}
