using System.ComponentModel.DataAnnotations;

namespace cinema_project.Models
{
    public class Hall
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required!!")]
        [StringLength(20, ErrorMessage = "This name is too long!")]
        public string Name { get; set; }


        [Required(ErrorMessage = "This field is required!!")]
        [Range(1, 10, ErrorMessage = "The row amount should variate between 1 and 10")]
        public int Rows { get; set; }

        [Required(ErrorMessage = "This field is required!!")]
        [Range(1, 12, ErrorMessage = "The column amount should variate between 1 and 12")]

        public int Columns { get; set; }

        public List<Session>? Sessions { get; set; }

        public List<Seat>? Seats { get; set; }
    }
}
