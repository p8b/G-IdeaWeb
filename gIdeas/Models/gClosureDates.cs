using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gIdeas.Models
{
    public class gClosureDates
    {
        [Key]
        public int Year { get; set; }

        [Required]
        public int FirstClosure { get; set; }

        [Required]
        public int FinalClosure { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [DataType(DataType.DateTime)]
        public DateTime TimeStampLastModified { get; set; } = DateTime.UtcNow;
    }
}
