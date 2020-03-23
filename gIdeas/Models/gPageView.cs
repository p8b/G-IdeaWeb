using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gIdeas.Models
{
    public class gPageView
    {
        [Key]
        public int Id { get; set; }

        #region **** Attributes nvarchar(256), Required, StringLength 256 ****
        [Column(TypeName = "nvarchar(256)")]
        [Required(ErrorMessage = "Page Name Required")]
        [StringLength(256, ErrorMessage = "Name must be less than 256 Characters")]
        #endregion
        public string PageName{ get; set; }

        [Required(ErrorMessage = "Count required")]
        public int PageCount { get; set; }
    }
}
