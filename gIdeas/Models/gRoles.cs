using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gIdeas.Models
{
    public class gRole : IdentityUserClaim<int>
    {
        #region *** Identity Claim *** 
        /// <summary>
        /// This value cannot be set as it is fixed to "Role"
        /// </summary>
        [NotMapped]
        public override string ClaimType { get { return "Role"; } set { } }
        /// <summary>
        /// The same as the Role Name
        /// </summary>
        [NotMapped]
        public override string ClaimValue { get { return Name; } set { Name = value; } }
        #endregion

        public string Name{ get; set; }
    }
}
