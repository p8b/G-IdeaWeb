using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gIdeas.Models
{
    public class gFlaggedIdeas
    {
        [Key]
        public int Id { get; set; }

        [DataType("nvarchar(256)")]
        [Required]
        public string Type { get; set; }

        [DataType("nvarchar(500)")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Idea is require for comment on.")]
        public gIdeas Idea { get; set; }
    }
}
