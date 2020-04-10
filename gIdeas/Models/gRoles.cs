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
    public class gRole
    {
        [Key]
        public int Id { get; set; } = 0;

        #region **** Attributes nvarchar(256), Required, StringLength 256 ****
        [Column(TypeName = "nvarchar(256)")]
        [Required(ErrorMessage = "Role Name Required")]
        [StringLength(256, ErrorMessage = "Name must be less than 256 Characters")]
        #endregion
        public string Name{ get; set; }

        #region **** Attributes: nvarchar(50), Required, StringLength 50, ValidateAccessClaim ****
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "AccessClaimType Name Required")]
        [StringLength(256, ErrorMessage = "AccessClaimType must be less than 50 Characters")]
        #endregion
        public string AccessClaim { get; set; }
    }
}
