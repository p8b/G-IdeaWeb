using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        /// 1:m relationship with User entity
        /// User Not Required
        /// </summary>
        public ICollection<gCategory> Categories { get; set; }
    }
}
