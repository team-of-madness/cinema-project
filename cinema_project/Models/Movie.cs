using System.ComponentModel.DataAnnotations;

namespace cinema_project.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required!!")]
        [StringLength(40, ErrorMessage = "This name is too long!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required!!")]
        [StringLength(350, ErrorMessage = "Description can not be more than 350 characters long!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required!!")]
        [Range(20, 300, ErrorMessage = "The duration can not be less than 20 minutes and can not be more than 300!")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "This field is required!!")]
        [Range(0, 18, ErrorMessage = "The age restrictions can not be more than 18+")]
        public int MinAge { get; set; }


        [Required(ErrorMessage = "This field is required!!")]
        [StringLength(35, ErrorMessage = "This name is too long!")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "The producer name can contain only letter characters!!")]
        public string Producer { get; set; }

		// Зовнішній ключ
		[Required(ErrorMessage = "This field is required!!")]
		public int GenreId { get; set; }

        // Навігаційна властивість
        public Genre? Genre { get; set; }
        public List<Session>? Sessions { get; set; }
    }
}