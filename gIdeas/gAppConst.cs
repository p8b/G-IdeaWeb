using Microsoft.AspNetCore.Identity;
namespace gIdeas
{
    public class gAppConst
    {
        /// <summary>
        /// Three Levels of access within the system.<br /> 
        /// * Admin<br/>
        /// * Manager<br/> 
        /// * Staff<br/> 
        /// * Customer  
        /// </summary>
        public struct AccessClaims
        {
            public const string Type = "Role";
            public const string Admin = "Admin";
            public const string QAManager = "QAManager";
            public const string QACoordinator = "QACoordinator";
            public const string Staff = "Staff";
            public static readonly string[] All =
                { Admin, QAManager, QACoordinator, Staff };
        }

        #region **** Claim Identifiers ****
        internal const string _ClaimUserId = "UserId";
        internal const string _ClaimRole = "Role";
        #endregion

        #region **** Scheme ****
        internal const string _AuthSchemeApplication = "Identity.App";
        #endregion

        #region **** Password Requirement ****
        internal static PasswordOptions PasswordOptions
        {
            get
            {
                return new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 3,
                    RequiredUniqueChars = 1,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = true,
                    RequireUppercase = true
                };
            }
        }
        #endregion
    }
}
