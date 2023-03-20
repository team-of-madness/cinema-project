namespace cinema_project.Models
{
    public class Hall
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RowNumber { get; set; }

        public int RowPlaces { get; set; }

        public List<Session> Sessions { get; set; }

        public List<Place> Places { get; set; }
    }
}
