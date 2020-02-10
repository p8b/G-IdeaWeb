using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

    }
}
