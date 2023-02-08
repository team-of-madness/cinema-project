using System;

namespace cinema_project.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int Id_movie { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
    }
}
