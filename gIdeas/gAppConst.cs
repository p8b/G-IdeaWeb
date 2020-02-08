using Microsoft.AspNetCore.Identity;
namespace gIdeas
{
    public class gAppConst
    {
        #region **** System Roles ****
        internal const string _Admin = "Admin";
        internal const string _QAManager = "QA Manager";
        internal const string _QACoordinator = "QA Coordinator";
        internal const string _Staff = "Staff";
        internal const string _All = "All";
        #endregion

        #region **** Claim Identifiers ****
        internal const string _ClaimUserId = "UserId";
        internal const string _ClaimRole = "Role";
        #endregion

        #region **** Scheme ****
        internal const string _AuthSchemeApplication = "Identity.App";
        #endregion

        #region **** Password Requirement ****
        internal static PasswordOptions _PasswordOptions
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
