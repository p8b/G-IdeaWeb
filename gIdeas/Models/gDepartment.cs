using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gIdeas.Models
{
    public class gDepartment
    {
        [Key]
        public int Id { get; set; }

        #region *** Attributes: nvarchar(256), Required **
        [DataType("nvarchar(256)")]
        [Required(ErrorMessage ="Role name is required!")]
        #endregion
        public string Name { get; set; }

        /// <summary>
        /// 1:m relationship with User entity with the specified foreign key to be used as composite key in 
        /// the link table between the category and department
        /// </summary>
        [ForeignKey("DepartmentId"), Column(Order = 1)]
        public ICollection<gCategoriesToDepartment> gCategoriesToDepartments { get; set; }
    }
}
