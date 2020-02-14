﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gIdeas.Models
{
    public class gCategoriesToDepartment
    {
        [Key, Column(Order = 1)]
        [Required(ErrorMessage ="Category needed to create a link with Department")]
        public int CategoryId { get; set; }

        [Key, Column(Order = 0)]
        [Required(ErrorMessage ="Department needed to create a link with category")]
        public int DepartmentId { get; set; }
    }
}
