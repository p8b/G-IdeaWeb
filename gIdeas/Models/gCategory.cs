using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gIdeas.Models
{
    public class gCategory
    {
        [Key]
        public int Id { get; set; }

        #region *** Attributes: nvarchar(256), Required **
        [DataType("nvarchar(256)")]
        [Required(ErrorMessage ="Category name is required!")]
        #endregion
        public string Name { get; set; }

        #region **** Attributes: nvarchar(30), Required
        [DataType("nvarchar(20)")]
        [Required(ErrorMessage = "Category name is required!")]
        #endregion
        public string ShortDescription { get; set; }

        [ForeignKey("CategoryId"), Column(Order = 1)]
        public ICollection<gCategoriesToDepartment> Departments { get; set; }
        
        [ForeignKey("CategoryId"), Column(Order = 1)]
        public ICollection<gCategoriesToIdeas> Ideas { get; set; }
    }
}
