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
    public class gUser: IdentityUser<int>
    {
        #region **** Attributes nvarchar(256), Required, StringLength 256 ****
        [Column(TypeName = "nvarchar(256)")]
        [Required(ErrorMessage = "Name Required")]
        [StringLength(256, ErrorMessage = "Name must be less than 256 Characters")]
        #endregion
        public string FirstName { get; set; }

        #region **** Attributes nvarchar(256), Required, StringLength 256 ****
        [Column(TypeName = "nvarchar(256)")]
        [Required(ErrorMessage = "Surname Required")]
        [StringLength(256, ErrorMessage = "Surname must be less than 256 Characters")]
        #endregion
        public string Surname { get; set; }

        #region **** Attributes DataType.Password, Required ****
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Required")]
        #endregion
        public override string PasswordHash { get; set; }

        #region **** Attributes DataType.EmailAddress, Required, RegularExpression ****
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email Required")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
            ErrorMessage = "Invalid Email")]
        #endregion
        public override string Email { get; set; }

        [Required(ErrorMessage ="User must be assigned to a department.")]
        public gDepartment Department { get; set; }

        [Required(ErrorMessage ="User must be assigned to a role")]
        public gRole Role { get; set; }
    }
}
