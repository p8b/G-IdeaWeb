using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gIdeas.Models
{
    public class gUser : IdentityUser<int>
    {
        public gUser()
        {
            UserName = $"p8b{new Random().Next()}";
        }

        [Key, ForeignKey("Id")]
        public override int Id { get; set; }

        #region **** Attributes nvarchar(256), Required, StringLength 256 ****
        [Column(TypeName = "nvarchar(256)")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(256, ErrorMessage = "Name must be less than 256 Characters")]
        #endregion
        public string FirstName { get; set; }

        #region *** Attributes nvarchar(256), Required, StringLength 256 ***
        [Column(TypeName = "nvarchar(256)")]
        [Required(ErrorMessage = "Surname is required")]
        [StringLength(256, ErrorMessage = "Surname must be less than 256 Characters")]
        #endregion
        public string Surname { get; set; }

        #region *** Attributes DataType.Password, Required ***
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [JsonIgnore]
        #endregion
        public override string PasswordHash { get; set; }

        #region *** Attributes DataType.EmailAddress, Required, RegularExpression ***
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
            ErrorMessage = "Invalid Email")]
        #endregion
        public override string Email { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public gDepartment Department { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public gRole Role { get; set; }

        public bool IsBlocked { get; set; } = false;

        [JsonIgnore]
        public ICollection<gIdea> Ideas { get; set; }
        [JsonIgnore]
        public ICollection<gComment> Comments { get; set; }
        public ICollection<gFlaggedIdea> FlaggedIdeas { get; set; }
        [ForeignKey("UserId"), Column(Order = 0)]
        public ICollection<gLoginRecord> LoginRecords { get; set; }

        [NotMapped]
        public DateTime LastLoginDate { get; set; }
        [NotMapped]
        public string NewPassword { get; set; }
        [NotMapped]
        public int TotalNumberOfIdeas { get; set; }
        [NotMapped]
        public int TotalNumberOfComments { get; set; }


        #region *** Ignored properties in json conversion ***

        [JsonIgnore]
        public override DateTimeOffset? LockoutEnd { get; set; }
        [JsonIgnore]
        public override bool TwoFactorEnabled { get; set; }
        [JsonIgnore]
        public override bool PhoneNumberConfirmed { get; set; }
        [JsonIgnore]
        public override bool EmailConfirmed { get; set; }
        [JsonIgnore]
        public override string ConcurrencyStamp { get; set; }
        [JsonIgnore]
        public override string SecurityStamp { get; set; }
        [JsonIgnore]
        public override string NormalizedEmail { get; set; }
        [JsonIgnore]
        public override string NormalizedUserName { get; set; }
        [JsonIgnore]
        public override string UserName { get; set; }
        [JsonIgnore]
        public override bool LockoutEnabled { get; set; }
        [JsonIgnore]
        public override int AccessFailedCount { get; set; } 
        #endregion
    }
}
