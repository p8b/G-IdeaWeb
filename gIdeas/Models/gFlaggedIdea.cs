using System.ComponentModel.DataAnnotations;
namespace gIdeas.Models
{
    public class gFlaggedIdea
    {
        [Key]
        public int Id { get; set; }

        [DataType("nvarchar(256)")]
        [Required(ErrorMessage = "The type of flag is required")]
        public string Type { get; set; }

        [DataType("nvarchar(500)")]
        [Required(ErrorMessage = "Idea to be flagged is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Idea to be flagged is required.")]
        public int IdeaId { get; set; }

        [Required(ErrorMessage = "User is needed for submitting a flag report.")]
        public gUser User { get; set; }
    }
}
