using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gIdeas.Models
{
    public class gCategoriesToDepartment
    {
        [Key]
        [Required(ErrorMessage ="Department needed to create a link with category")]
        public gDepartment Department { get; set; }
        [Key]
        [Required(ErrorMessage ="Category needed to create a link with Department")]
        public gCategory Category { get; set; }
    }
}
