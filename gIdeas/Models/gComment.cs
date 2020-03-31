using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gIdeas.Models
{
    public class gComment
    {
        [Key]
        public int Id { get; set; }

        [DataType("nvarchar(500)")]
        [Required(ErrorMessage ="Comment is required.")]
        public string Description { get; set; }

        public bool IsAnonymous { get; set; }

        [Required(ErrorMessage ="Idea is require for commenting")]
        public int IdeaId { get; set; }

        [Required(ErrorMessage ="User is require for commenting")]
        public gUser User { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [DataType(DataType.DateTime)]
        public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;

    }
}
