using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gIdeas.Models
{
    public class gComments
    {
        [Key]
        public int Id { get; set; }

        [DataType("nvarchar(500)")]
        [Required(ErrorMessage ="Comment is required.")]
        public string Comment { get; set; }

        public bool AnonComment { get; set; }

        [Required(ErrorMessage ="Idea is require for comment on.")]
        public gIdeas Idea { get; set; }
    }
}
