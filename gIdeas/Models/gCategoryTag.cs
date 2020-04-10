using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gIdeas.Models
{
    public class gCategoryTag
    {
        [Key]
        public int Id { get; set; }

        [DataType("nvarchar(256)")]
        [Required(ErrorMessage ="Name is required!")]
        public string Name { get; set; }

        [NotMapped]
        public ICollection<gCategoriesToIdeas> Ideas { get; set; }
    }
}
