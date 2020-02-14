﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gIdeas.Models
{
    public class gDocument
    {
        [Key]
        public int Id { get; set; }

        [DataType("nvarchar(Max)")]
        [Required(ErrorMessage ="The document path is required")]
        public string Path { get; set; }

        [Required(ErrorMessage = "Idea is required for current document.")]
        public gIdeas Idea { get; set; }
    }
}
