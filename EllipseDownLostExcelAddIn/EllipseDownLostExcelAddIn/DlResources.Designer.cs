﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EllipseDownLostExcelAddIn {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DlResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DlResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EllipseDownLostExcelAddIn.DlResources", typeof(DlResources).Assembly);
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
        ///   Looks up a localized string similar to DELETE DOWN &amp; LOST.
        /// </summary>
        internal static string Delete_Title {
            get {
                return ResourceManager.GetString("Delete_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This action will delete the Down &amp; Lost existing records. Do you want to continue?.
        /// </summary>
        internal static string Delete_Warning {
            get {
                return ResourceManager.GetString("Delete_Warning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Comment creation error for selected Lost registers.
        /// </summary>
        internal static string Error_LostCommentCreationFailed {
            get {
                return ResourceManager.GetString("Error_LostCommentCreationFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Lost record found to create comment.
        /// </summary>
        internal static string Error_NoLostForComment {
            get {
                return ResourceManager.GetString("Error_NoLostForComment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not get response from server.
        /// </summary>
        internal static string Error_NoServerResponse {
            get {
                return ResourceManager.GetString("Error_NoServerResponse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Collection.
        /// </summary>
        internal static string Field_CollectionCamelcase {
            get {
                return ResourceManager.GetString("Field_CollectionCamelcase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to COLLECTION.
        /// </summary>
        internal static string Field_CollectionUppercase {
            get {
                return ResourceManager.GetString("Field_CollectionUppercase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USERNAME.
        /// </summary>
        internal static string Field_UsernameUppercase {
            get {
                return ResourceManager.GetString("Field_UsernameUppercase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WorkOrder.
        /// </summary>
        internal static string Field_WorkOrderCamelcase {
            get {
                return ResourceManager.GetString("Field_WorkOrderCamelcase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WORKORDER.
        /// </summary>
        internal static string Field_WorkOrderUppercase {
            get {
                return ResourceManager.GetString("Field_WorkOrderUppercase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Input Originator Id.
        /// </summary>
        internal static string Input_InputOriginatorId {
            get {
                return ResourceManager.GetString("Input_InputOriginatorId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to NO ACTION EXECUTED.
        /// </summary>
        internal static string Result_NoActionExecuted {
            get {
                return ResourceManager.GetString("Result_NoActionExecuted", resourceCulture);
            }
        }
    }
}
