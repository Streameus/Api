﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Streameus.App_GlobalResources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Translation {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Translation() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Streameus.App_GlobalResources.Translation", typeof(Translation).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have to add a card before trying to pay.
        /// </summary>
        internal static string AddCardFirst {
            get {
                return ResourceManager.GetString("AddCardFirst", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Conference not found!.
        /// </summary>
        internal static string ConferenceNotFound {
            get {
                return ResourceManager.GetString("ConferenceNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You can not subscribe to a conference in the past..
        /// </summary>
        internal static string ErrorSuscribePastConference {
            get {
                return ResourceManager.GetString("ErrorSuscribePastConference", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File not found!.
        /// </summary>
        internal static string FileNotFound {
            get {
                return ResourceManager.GetString("FileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You do not have the rights to update this conference.
        /// </summary>
        internal static string ForbiddenConfUpdate {
            get {
                return ResourceManager.GetString("ForbiddenConfUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You can only delete your account.
        /// </summary>
        internal static string ForbiddenUserDelete {
            get {
                return ResourceManager.GetString("ForbiddenUserDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Id provided is invalid.
        /// </summary>
        internal static string InvalidId {
            get {
                return ResourceManager.GetString("InvalidId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No author.
        /// </summary>
        internal static string NoResultExceptionAuthor {
            get {
                return ResourceManager.GetString("NoResultExceptionAuthor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No events are present.
        /// </summary>
        internal static string NoResultExceptionEvent {
            get {
                return ResourceManager.GetString("NoResultExceptionEvent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The owner of the conference cannot suscribe to its own conference.
        /// </summary>
        internal static string OwnerCannotSuscribeToItsConf {
            get {
                return ResourceManager.GetString("OwnerCannotSuscribeToItsConf", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The conference has not been started.
        /// </summary>
        internal static string TheConferenceIsNotStarted {
            get {
                return ResourceManager.GetString("TheConferenceIsNotStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have already suscribed to this conference.
        /// </summary>
        internal static string UserHasAlreadySuscribed {
            get {
                return ResourceManager.GetString("UserHasAlreadySuscribed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You haven&apos;t suscribed to this conference.
        /// </summary>
        internal static string UserIsNotEnlisted {
            get {
                return ResourceManager.GetString("UserIsNotEnlisted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User with the same email already exists.
        /// </summary>
        internal static string UserWithSameEmailAlreadyExists {
            get {
                return ResourceManager.GetString("UserWithSameEmailAlreadyExists", resourceCulture);
            }
        }
    }
}
