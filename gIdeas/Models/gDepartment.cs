using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gIdeas.Models
{
    public class gDepartment
    {
        [Key]
        public int Id { get; set; } = 0;

        [DataType("nvarchar(256)")]
        [Required(ErrorMessage ="Name is required.")]
        public string Name { get; set; }

        [NotMapped]
        public int TotalNumberOfIdeas { get; set; }
        [NotMapped]
        public int TotalPercentageOfIdeas { get; set; }
        [NotMapped]
        public int TotalNumberOfContributors { get; set; }

    }
}
