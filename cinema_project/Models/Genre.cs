namespace cinema_project.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }

        // Навігаційна властивість
        //public List<Movie> Movies { get; set; }
    }
}
