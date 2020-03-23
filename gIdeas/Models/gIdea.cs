using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gIdeas.Models
{/// <summary>
/// 
/// Model for the idea table
///  has a FK to the gUsers Model
/// 
/// </summary>
    public class gIdea
    {
        [Key]
        public int Id { get; set; }

        #region **** Attributes: nvarchar(42), Required ***
        [DataType("nvarchar(42)")]
        [Required(ErrorMessage = "Status is required!")]
        #endregion
        public string Status { get; set; }

        #region **** Attributes: nvarchar(265), Required ***
        [DataType("nvarchar(265)")]
        [Required(ErrorMessage = "Title is required!")]
        #endregion
        public string Title { get; set; }

        #region **** Attributes: nvarchar(500), Required ***
        [DataType("nvarchar(500)")]
        [Required(ErrorMessage = "Description is required!")]
        #endregion
        public string ShortDescription { get; set; }

        #region **** Attributes: nvarchar(30), Required ***
        [Column(TypeName = "nvarchar(30)")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage ="The idea must have a create date")]
        #endregion
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        #region **** Attributes: nvarchar(30), Required ***
        [Column(TypeName = "nvarchar(30)")]
        [DataType(DataType.DateTime)]
        #endregion
        public DateTime CloseDate { get; set; }

        #region **** Attributes: nvarchar(30), Required ***
        [Column(TypeName = "nvarchar(30)")]
        [DataType(DataType.DateTime)]
        #endregion
        public DateTime FinalClosureDate { get; set; }

        public bool DisplayAnonymous { get; set; } = false;

        [Required(ErrorMessage ="A user must be assigned to the idea")]
        public gUser Author { get; set; }

        public ICollection<gDocument> gDocuments { get; set; }

        public ICollection<gComment> gComments { get; set; }

        public ICollection<gFlaggedIdea> gFlaggedIdeas { get; set; }
        
        public ICollection<gVotes> gVotes { get; set; }

        [ForeignKey("IdeaId"), Column(Order = 0)]
        public ICollection<gCategoriesToIdeas> gCategoriesToIdeas { get; set; }

    }
}
