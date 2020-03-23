using System.ComponentModel.DataAnnotations;
namespace gIdeas.Models
{
    public class FlaggedIdea
    {
        [Key]
        public int Id { get; set; }

        [DataType("nvarchar(256)")]
        [Required(ErrorMessage = "The type of flag is required")]
        public string Type { get; set; }

        [DataType("nvarchar(500)")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Idea to be flagged is required.")]
        public gIdea Idea { get; set; }

        [Required(ErrorMessage = "User is needed for submitting a flag report.")]
        public gUser Users { get; set; }
    }
}
