﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CommonData.Properties {
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
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CommonData.Properties.Resources", typeof(Resources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Великий (20х15).
        /// </summary>
        public static string MicroSlotTypes_Large {
            get {
                return ResourceManager.GetString("MicroSlotTypes_Large", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Середній (25х25).
        /// </summary>
        public static string MicroSlotTypes_Middle {
            get {
                return ResourceManager.GetString("MicroSlotTypes_Middle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Маленький (30х40).
        /// </summary>
        public static string MicroSlotTypes_Small {
            get {
                return ResourceManager.GetString("MicroSlotTypes_Small", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Помилка, слот має показник {0} а потрібено менше або дорівнює 2.
        /// </summary>
        public static string Msg_Error {
            get {
                return ResourceManager.GetString("Msg_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Все добре, слот без помилок.
        /// </summary>
        public static string Msg_Norma {
            get {
                return ResourceManager.GetString("Msg_Norma", resourceCulture);
            }
        }
    }
}