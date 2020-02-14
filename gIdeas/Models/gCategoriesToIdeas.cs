using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace gIdeas.Models
{
    public class gCategoriesToIdeas
    {
        [Key, Column(Order = 0)]
        [Required(ErrorMessage = "Category needed to create a link with Idea")]
        public int CategoryId { get; set; }

        [Key, Column(Order = 0)]
        [Required(ErrorMessage = "Idea is needed to create a link with category")]
        public int IdeaId { get; set; }
    }
}
