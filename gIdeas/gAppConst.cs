using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Four Levels of access policies within the system.<br /> 
        /// </summary>
        public struct AccessPolicies
        {
            /// <summary>
            /// * Level One includes Admin
            /// </summary>
            public const string LevelOne = "AccessLevelOne";
            /// <summary>
            /// * Level Two includes Admin and QA Manager  <br/>
            /// </summary>
            public const string LevelTwo = "AccessLevelTwo";
            /// <summary>
            /// * Level Three includes Admin, QA Manager and QA Coordinator <br/>
            /// </summary>
            public const string LevelThree = "AccessLevelThree";
            /// <summary>
            /// * Level Three includes Admin, QA Manager, QA Coordinator and Staff <br/>
            /// </summary>
            public const string LevelFour = "AccessLevelFour";
        }

        /// <summary>
        /// valid idea status
        /// </summary>
        public struct IdeaStatus
        {
            public const string Pending = "Pending";
            public const string FirstClosure = "FirstClosure";
            public const string Closed = "Closed";
            public const string Blocked = "Blocked";
        }

        /// *** Claim Identifiers ***
        internal const string _ClaimUserId = "UserId";
        internal const string _ClaimRole = "Role";


        /// *** Scheme ***
        internal const string _AuthSchemeApplication = "Identity.Application";

        /// *** Password Requirement ***
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

        /// <summary>
        /// This is used to get all the records for the specified API parameter call
        /// </summary>
        internal static string AllRecords = "***GET-ALL***";

        /// <summary>
        /// This method is used to extract the errors form model state
        /// </summary>
        /// <param name="ModelState">The instance of model state which contains the errors</param>
        /// <param name="errorList">the reference of error list to add the errors to </param>
        public static void ExtractErrors(ModelStateDictionary ModelState, ref List<gError> errorList)
        {
            int count = 0;
            // Loop through the Model state values
            foreach (var prop in ModelState.Values)
            {
                /// add each error in the model state to the error list
                Error(ref errorList,
                      prop.Errors.FirstOrDefault().ErrorMessage,
                      ModelState.Keys.ToArray()[count]);
                count++;
            }
        }

        /// <summary>
        /// Used to create a anonymous type and adding it to the referenced error list
        /// </summary>
        /// <param name="Key">Id of the error</param>
        /// <param name="Message">The error message</param>
        /// <param name="errors">Reference of the error list used to add the error</param>
        public static void Error(ref List<gError> errors, string value, string key = "")
        {
            errors.Add(new gError(key, value));
        }

        public static void GetBrowserDetails(string userAgent, out string browserName)
        {
            browserName = "Others";
            
            /// Get the OS Name
            //if (userAgent.Contains("Mobile", StringComparison.CurrentCultureIgnoreCase))
            //    browserName = "Mobile - ";
            //else if (userAgent.Contains("Windows", StringComparison.CurrentCultureIgnoreCase))
            //    browserName = "Windows - ";
            //else if (userAgent.Contains("Macintosh", StringComparison.CurrentCultureIgnoreCase))
            //    browserName = "Macintosh - ";
            //else
            //    browserName = "Others - ";

            /// Get the browser name
            if (userAgent.Contains("Edge", StringComparison.CurrentCultureIgnoreCase))
                browserName = "Edge";
            else if (userAgent.Contains("chrome", StringComparison.CurrentCultureIgnoreCase))
                browserName = "Chrome";
            else if (userAgent.Contains("FireFox", StringComparison.CurrentCultureIgnoreCase))
                browserName = "FireFox";
            else if (userAgent.Contains("Safari", StringComparison.CurrentCultureIgnoreCase))
                browserName = "Safari";
            else
                browserName = "Others";
        }
    }
    /// <summary>
    /// Method to create an error object to be send to client side
    /// </summary>
    public class gError
    {
        public gError()
        {
        }

        /// <summary>
        /// Constructor to create a new instance of error object
        /// </summary>
        /// <param name="key">The key value of the error (ID)</param>
        /// <param name="value">"The Value of the error (Message)"</param>
        public gError(string key, string value)
        {
            Key = key;
            Value = value;
        }
        /// <summary>
        /// Error Key (ID)
        /// </summary>
        public string Key { get; set; } = "0";
        /// <summary>
        /// Error Value (Message)
        /// </summary>
        public string Value { get; set; }
    }
}
