using System.ComponentModel.DataAnnotations;

namespace cinema_project.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="This field is required!!")]
        [StringLength(20, ErrorMessage = "This name is too long!")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "The genre name can contain only letter characters!!") ]
        public string GenreName { get; set; }

        // Навігаційна властивість
        public List<Movie>? Movies { get; set; }
    }
}
