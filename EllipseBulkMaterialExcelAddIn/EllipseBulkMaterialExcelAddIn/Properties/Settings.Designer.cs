﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EllipseBulkMaterialExcelAddIn.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ews-el8prod.lmnerp01.cerrejon.com/ews/services")]
        public string EllipseProductivo {
            get {
                return ((string)(this["EllipseProductivo"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ews-el8prod.lmnerp02.cerrejon.com/ews/services")]
        public string EllipseContingencia {
            get {
                return ((string)(this["EllipseContingencia"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ews-el8desa.lmnerp03.cerrejon.com/ews/services")]
        public string EllipseDesarrollo {
            get {
                return ((string)(this["EllipseDesarrollo"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ews-el8test.lmnerp03.cerrejon.com/ews/services")]
        public string EllipseTest {
            get {
                return ((string)(this["EllipseTest"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ews-el8prod.lmnerp02.cerrejon.com/ews/services/BulkMaterialUsageSheetServi" +
            "ce")]
        public string EllipseBulkMaterialExcelAddIn_BulkMaterialUsageSheetService_BulkMaterialUsageSheetService {
            get {
                return ((string)(this["EllipseBulkMaterialExcelAddIn_BulkMaterialUsageSheetService_BulkMaterialUsageShee" +
                    "tService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ews-el8prod.lmnerp02.cerrejon.com/ews/services/BulkMaterialUsageSheetItemS" +
            "ervice")]
        public string EllipseBulkMaterialExcelAddIn_BulkMaterialUsageSheetItemService_BulkMaterialUsageSheetItemService {
            get {
                return ((string)(this["EllipseBulkMaterialExcelAddIn_BulkMaterialUsageSheetItemService_BulkMaterialUsage" +
                    "SheetItemService"]));
            }
        }
    }
}
