namespace cinema_project.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public int Duration { get; set; }
        public int MinAge { get; set; }
        public string Producer { get; set; }

        // Зовнішній ключ
        public int GenreId { get; set; }

        // Навігаційна властивість
        public Genre Genre { get; set; }
        public List<Session> Sessions { get; set; }
    }
}