﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SharedClassLibrary.Configuration;
using SharedClassLibrary.Connections;
using SharedClassLibrary.Ellipse.Connections;
using SharedClassLibrary.Utilities;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace SharedClassLibrary.Ellipse
{
    public class Settings : SharedClassLibrary.Configuration.ISettings
    {
        public static Settings CurrentSettings;
        public Settings()
        {
            Initialize();
        }

        public void Initialize()
        {
            var lastAssembly = SharedClassLibrary.Configuration.AssemblyItem.GetLastAssembly();
            ProgramTitle = new SharedClassLibrary.Configuration.AssemblyItem(lastAssembly).AssemblyTitle;
            //GeneralFolder
            DefaultLocalDataPath = @"c:\ellipse\";
            GeneralConfigFolder = @"addins\" + ProgramTitle;
            GeneralConfigFileName = "config.xml";
            DefaultRepositoryFilePath = @"\\lmnoas02\Shared\Sistemas\Mina\Proyecto Ellipse\Ellipse 8\ExcelAddIn_E8 (Loaders)\";

            //Windows Environment Variables
            ProgramEnvironmentHomeVariable = ProgramTitle + "Home";
            HomeEnvironmentVariable = "EllipseAddInsHome";
            ServicesEnvironmentVariable = "EllipseServiceUrlFile";
            SecondaryServicesEnvironmentVariable = "EllipseSecondaryServiceUrlFile";

            //Services & Databases Information
            ServicesForcedList = "EllipseServiceForcedList";
            ServicesConfigXmlFileName = "EllipseConfiguration.xml";
            TnsnamesFileName = "tnsnames.ora";
            DatabaseXmlFileName = "EllipseDatabases.xml";

            DefaultServiceFilePath = @"\\lmnoas02\SideLine\EllipsePopups\Ellipse8\";
            SecondaryServiceFilePath = @"\\pbvshr01\SideLine\EllipsePopups\Ellipse8\";
            DefaultTnsnamesFilePath = @"c:\oracle\product\11.2.0\client\network\ADMIN\";

            //StaticReference Through All the Project
            Debugger.LocalDataPath = LocalDataPath;
            CurrentSettings = this;
        }

        public string DefaultRepositoryFilePath { get; set; }
        public string HomeEnvironmentVariable { get; set; }
        public string ServicesEnvironmentVariable { get; set; }
        public string SecondaryServicesEnvironmentVariable { get; set; }
        public string ServicesForcedList { get; set; }
        public string ServicesConfigXmlFileName { get; set; }
        public string TnsnamesFileName { get; set; }
        public string DatabaseXmlFileName { get; set; }
        public string DefaultServiceFilePath { get; set; }
        public string SecondaryServiceFilePath { get; set; }
        public string DefaultTnsnamesFilePath { get; set; }
        public string DefaultLocalDataPath { get; set; }
        public string ProgramEnvironmentHomeVariable { get; set; }
        public string ProgramTitle { get; set; }
        public string GeneralConfigFileName { get; set; }
        public string GeneralConfigFolder { get; set; }

        #region -- CustomSettings Methods --
        private IOptions CustomSettings { get; set; }
        private IOptions DefaultCustomSettings { get; set; }
        public void LoadCustomSettings()
        {
            try
            {
                var path = LocalDataPath;

                if (path == null)
                    throw new NullReferenceException("No se puede cargar el archivo de configuración porque no se ha definido una ruta Local Predeterminada");
                var urlFile = Path.Combine(LocalDataPath, GeneralConfigFolder, GeneralConfigFileName);
                if (FileWriter.CheckFileExist(urlFile))
                {
                    var option = (Options) Utilities.MyUtilities.Xml.DeserializeXmlToObject(urlFile, typeof(Options));
                    CustomSettings = option;
                    return;
                }
                if (DefaultCustomSettings != null)
                    CustomSettings = DefaultCustomSettings;
            }
            catch (Exception ex)
            {
                if (DefaultCustomSettings != null)
                    CustomSettings = DefaultCustomSettings;
                throw new FileLoadException("Se ha producido un error al intentar cargar la configuración de " + ProgramTitle + ". Se continuará con la configuración predeterminada si esta existe. " + ex.Message);
            }
        }

        public void SaveCustomSettings()
        {
            try
            {
                if (CustomSettings == null)
                {
                    if (DefaultCustomSettings == null)
                        return;
                    CustomSettings = DefaultCustomSettings;
                }

                FileWriter.CreateDirectory(Path.Combine(LocalDataPath, GeneralConfigFolder));

                if (CustomSettings?.OptionsList != null)
                    MyUtilities.Xml.SerializeObjectToXml(Path.Combine(LocalDataPath, GeneralConfigFolder, GeneralConfigFileName), CustomSettings);
                else
                    throw new Exception("No hay opciones disponibles");
            }
            catch (Exception ex)
            {
                Debugger.LogError("SaveCustomSettings()",
                    "Error al intentar guardar las opciones. \n" + ex.Message);
                throw;
            }
        }

        public void SetDefaultCustomSettingValue(string key, string value)
        {
            if(DefaultCustomSettings == null)
                DefaultCustomSettings = new Options();

            DefaultCustomSettings.SetOption(key, value);
        }

        public string GetDefaultCustomSettingValue(string key)
        {
            return DefaultCustomSettings?.GetOptionValue(key);
        }

        public void SetCustomSettingValue(string key, string value)
        {
            if (CustomSettings == null)
                CustomSettings = new Options();

            CustomSettings.SetOption(key, value);
        }

        public string GetCustomSettingValue(string key)
        {
            string paramValue = null;
            if (CustomSettings != null)
                paramValue = CustomSettings.GetOptionValue(key);

            if (paramValue != null)
                return paramValue;

            if (DefaultCustomSettings != null)
                paramValue = DefaultCustomSettings.GetOptionValue(key);

            if (paramValue == null) return null;

            if(CustomSettings == null)
                CustomSettings = new Options();
            CustomSettings.SetOption(key, paramValue);

            return paramValue;
        }

        #endregion

        #region -- Variable Accessors --
        public bool IsServiceListForced
        {
            get
            {
                var varForced =
                    "" + Environment.GetEnvironmentVariable(ServicesForcedList, EnvironmentVariableTarget.User);
                var varForcedExpanded = Environment.ExpandEnvironmentVariables(varForced);
                return !string.IsNullOrWhiteSpace(varForcedExpanded) && varForcedExpanded.ToLower().Equals("true");
            }
            set
            {
                Environment.SetEnvironmentVariable(ServicesForcedList, value.ToString(),
                    EnvironmentVariableTarget.User);
            }
        }

        public string LocalDataPath
        {
            get
            {
                var varHome = "" + Environment.GetEnvironmentVariable(HomeEnvironmentVariable,
                                  EnvironmentVariableTarget.User);
                var varHomeExpanded = Environment.ExpandEnvironmentVariables(varHome);
                return string.IsNullOrWhiteSpace(varHomeExpanded) ? DefaultLocalDataPath : varHomeExpanded;
            }
            set
            {
                var currentVar = Environment.GetEnvironmentVariable(HomeEnvironmentVariable, EnvironmentVariableTarget.User);
                //no existe y es igual a _origen -> no hace nada
                if (string.IsNullOrWhiteSpace(currentVar) && value.Equals(DefaultLocalDataPath))
                    return;

                //existe y es igual a environment -> no hace nada
                if (!string.IsNullOrWhiteSpace(currentVar) && value.Equals(currentVar))
                    return;

                //no existe y es diferente a _origen -> actualiza
                if (string.IsNullOrWhiteSpace(currentVar) && !value.Equals(DefaultLocalDataPath))
                    Environment.SetEnvironmentVariable(HomeEnvironmentVariable, value, EnvironmentVariableTarget.User);

                //existe y es diferente a environment -> actualiza
                else if (!string.IsNullOrWhiteSpace(currentVar) && !value.Equals(currentVar))
                    Environment.SetEnvironmentVariable(HomeEnvironmentVariable, value, EnvironmentVariableTarget.User);
            }
        }

        public string BackUpServiceFilePath
        {
            get
            {
                var varService = "" + Environment.GetEnvironmentVariable(SecondaryServicesEnvironmentVariable,
                                     EnvironmentVariableTarget.User);
                var varServiceExpanded = Environment.ExpandEnvironmentVariables(varService);
                return string.IsNullOrWhiteSpace(varServiceExpanded) ? SecondaryServiceFilePath : varServiceExpanded;
            }
            set
            {
                var currentVar = Environment.GetEnvironmentVariable(SecondaryServicesEnvironmentVariable,
                    EnvironmentVariableTarget.User);
                //no existe y es igual a _origen -> no hace nada
                if (string.IsNullOrWhiteSpace(currentVar) && value.Equals(SecondaryServiceFilePath))
                    return;
                //existe y es igual a environment -> no hace nada
                if (!string.IsNullOrWhiteSpace(currentVar) && value.Equals(currentVar))
                    return;
                //no existe y es diferente a _origen -> actualiza
                if (string.IsNullOrWhiteSpace(currentVar) && !value.Equals(SecondaryServiceFilePath))
                    Environment.SetEnvironmentVariable(SecondaryServicesEnvironmentVariable, value, EnvironmentVariableTarget.User);
                //existe y es diferente a environment -> actualiza
                else if (!string.IsNullOrWhiteSpace(currentVar) && !value.Equals(currentVar))
                    Environment.SetEnvironmentVariable(SecondaryServicesEnvironmentVariable, value, EnvironmentVariableTarget.User);
            }
        }

        public string ServiceFilePath
        {
            get
            {
                var varService = "" + Environment.GetEnvironmentVariable(ServicesEnvironmentVariable,
                                     EnvironmentVariableTarget.User);
                var varServiceExpanded = Environment.ExpandEnvironmentVariables(varService);
                return string.IsNullOrWhiteSpace(varServiceExpanded) ? DefaultServiceFilePath : varServiceExpanded;
            }
            set
            {
                var currentVar = Environment.GetEnvironmentVariable(ServicesEnvironmentVariable,
                    EnvironmentVariableTarget.User);
                //no existe y es igual a _origen -> no hace nada
                if (string.IsNullOrWhiteSpace(currentVar) && value.Equals(DefaultServiceFilePath))
                    return;
                //existe y es igual a environment -> no hace nada
                if (!string.IsNullOrWhiteSpace(currentVar) && value.Equals(currentVar))
                    return;
                //no existe y es diferente a _origen -> actualiza
                if (string.IsNullOrWhiteSpace(currentVar) && !value.Equals(DefaultServiceFilePath))
                    Environment.SetEnvironmentVariable(ServicesEnvironmentVariable, value, EnvironmentVariableTarget.User);
                //existe y es diferente a environment -> actualiza
                else if (!string.IsNullOrWhiteSpace(currentVar) && !value.Equals(currentVar))
                    Environment.SetEnvironmentVariable(ServicesEnvironmentVariable, value, EnvironmentVariableTarget.User);
            }
        }

        public string TnsnamesFilePath
        {
            get { return RuntimeConfigSettings.GetTnsUrlValue(); }
            set
            {
                if (value.Equals(RuntimeConfigSettings.GetTnsUrlValue()))
                    return;
                RuntimeConfigSettings.UpdateTnsUrlValue(value);
            }
        }
        #endregion
        #region -- Configuration Files Generation --
        public void GenerateConfigurationXmlFile(string targetUrl)
        {
            var xmlFile = "";

            xmlFile += @"<?xml version=""1.0"" encoding=""UTF-8""?>";
            xmlFile += @"<ellipse>";
            xmlFile += @"  <env>test</env>";
            xmlFile += @"  <url>";
            xmlFile += @"    <ellprod>" + UrlPost.UrlProductivo + "</ellprod>";
            xmlFile += @"    <ellcont>" + UrlPost.UrlContingencia + "</ellcont>";
            xmlFile += @"    <elldesa>" + UrlPost.UrlDesarrollo + "</elldesa>";
            xmlFile += @"    <elltest>" + UrlPost.UrlTest + "</elltest>";
            xmlFile += @"  </url>";
            xmlFile += @"  <webservice>";
            xmlFile += @"    <ellprod>" + WebService.UrlProductivo + "</ellprod>";
            xmlFile += @"    <ellcont>" + WebService.UrlContingencia + "</ellcont>";
            xmlFile += @"    <elldesa>" + WebService.UrlDesarrollo + "</elldesa>";
            xmlFile += @"    <elltest>" + WebService.UrlTest + "</elltest>";
            xmlFile += @"  </webservice>";
            xmlFile += @"</ellipse>";

            try
            {
                //iniciamos las variables de directorio y archivos
                var configFilePath = FileWriter.NormalizePath(targetUrl, true);
                var defaultFilePath = FileWriter.NormalizePath(CurrentSettings.DefaultServiceFilePath, true);
                var configFileName = CurrentSettings.ServicesConfigXmlFileName;

                //comprobamos que la ruta no corresponda a la ruta predeterminada
                if (configFilePath.Equals(defaultFilePath))
                    throw new Exception("No se puede reemplazar el archivo " + configFileName + " del sistema. Si desea modificarlo, comuníquese con el administrador del sistema");

                //creamos una copia de seguridad si el archivo existe
                if (FileWriter.CheckFileExist(Path.Combine(configFilePath, configFileName)))
                {
                    var backupFileName = configFileName + "_" + System.DateTime.Today.Year + System.DateTime.Today.Month + System.DateTime.Today.Day + ".BAK";
                    if (!FileWriter.CheckFileExist(Path.Combine(configFilePath, backupFileName)))
                        FileWriter.MoveFileToDirectory(configFileName, configFilePath, backupFileName, configFilePath);
                }


                FileWriter.CreateDirectory(configFilePath);
                FileWriter.WriteTextToFile(xmlFile, configFileName, configFilePath);
            }
            catch (Exception ex)
            {
                Debugger.LogError("GenerateEllipseConfigurationXmlFile",
                    "No se puede crear el archivo de configuración\n" + ex.Message);
                throw;
            }
        }

        public void GenerateConfigurationXmlFile(string sourceUrl, string targetUrl)
        {
            try
            {
                //iniciamos las variables de directorio y archivos
                var configFilePath = FileWriter.NormalizePath(targetUrl, true);
                var defaultFilePath = FileWriter.NormalizePath(CurrentSettings.DefaultServiceFilePath, true);
                var sourceFilePath = FileWriter.NormalizePath(sourceUrl, true);
                var configFileName = CurrentSettings.ServicesConfigXmlFileName;

                //comprobamos que la ruta no corresponda a la ruta predeterminada
                if (configFilePath.Equals(defaultFilePath))
                    throw new Exception("No se puede reemplazar el archivo " + configFileName + " del sistema. Si desea modificarlo, comuníquese con el administrador del sistema");

                //creamos una copia de seguridad si el archivo existe
                if (FileWriter.CheckFileExist(Path.Combine(configFilePath, configFileName)))
                {
                    var backupFileName = configFileName + "_" + System.DateTime.Today.Year + System.DateTime.Today.Month + System.DateTime.Today.Day + ".BAK";
                    if (!FileWriter.CheckFileExist(Path.Combine(configFilePath, backupFileName)))
                        FileWriter.MoveFileToDirectory(configFileName, configFilePath, backupFileName, configFilePath);
                }

                //realizamos la acción
                FileWriter.CreateDirectory(configFilePath);
                FileWriter.CopyFileToDirectory(configFileName, sourceFilePath, configFilePath);
            }
            catch (Exception ex)
            {
                Debugger.LogError("GenerateEllipseConfigurationXmlFile",
                    "No se puede crear el archivo de configuración\n" + ex.Message);
                throw;
            }
        }

        public void GenerateDatabaseFile()
        {
            throw new NotImplementedException();
        }

        public void GenerateTnsnamesFile(string targetUrl)
        {
            throw new NotImplementedException();
        }

        public void DeleteConfigurationXmlFile()
        {
            try
            {
                //iniciamos las variables de directorio y archivos
                var configFilePath = FileWriter.NormalizePath(CurrentSettings.ServiceFilePath, true);
                var defaultFilePath = FileWriter.NormalizePath(CurrentSettings.DefaultServiceFilePath, true);
                var configFileName = CurrentSettings.ServicesConfigXmlFileName;

                //comprobamos que la ruta no corresponda a la ruta predeterminada
                if (configFilePath.Equals(defaultFilePath))
                    throw new Exception("No se puede reemplazar el archivo " + configFileName + " del sistema. Si desea modificarlo, comuníquese con el administrador del sistema");

                //realizamos la acción
                FileWriter.DeleteFile(configFilePath, configFileName);
                //restablecemos al valor predeterminado
                CurrentSettings.ServiceFilePath = CurrentSettings.DefaultServiceFilePath;
            }
            catch (Exception ex)
            {
                Debugger.LogError("DeleteEllipseConfigurationXmlFile()", "No se puede eliminar el archivo de configuración. " + ex.Message);
                throw;
            }
        }

        public void GenerateEllipseTnsnamesFile(string targetUrl)
        {
            try
            {
                //iniciamos las variables de directorio y archivos
                var configFilePath = FileWriter.NormalizePath(targetUrl, true);
                var defaultFilePath = FileWriter.NormalizePath(CurrentSettings.DefaultTnsnamesFilePath, true);
                var configFileName = CurrentSettings.TnsnamesFileName;

                //comprobamos que la ruta no corresponda a la ruta predeterminada
                if (configFilePath.Equals(defaultFilePath))
                    throw new Exception("No se puede reemplazar el archivo " + configFileName + " del sistema. Si desea modificarlo, comuníquese con el administrador del sistema");

                //creamos una copia de seguridad si el archivo existe
                if (FileWriter.CheckFileExist(Path.Combine(configFilePath, configFileName)))
                {
                    var backupFileName = configFileName + "_" + System.DateTime.Today.Year + System.DateTime.Today.Month + System.DateTime.Today.Day + ".BAK";
                    if (!FileWriter.CheckFileExist(Path.Combine(configFilePath, backupFileName)))
                        FileWriter.MoveFileToDirectory(configFileName, configFilePath, backupFileName, configFilePath);
                }

                //realizamos la acción
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "EllipseCommonsClassLibrary.Resources.tnsnames.txt";
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                using (var reader = new StreamReader(stream))
                {
                    var tnsFileText = reader.ReadToEnd();
                    FileWriter.CreateDirectory(configFilePath);
                    FileWriter.WriteTextToFile(tnsFileText, configFileName, configFilePath);
                }
            }
            catch (Exception ex)
            {
                Debugger.LogError("GenerateEllipseTnsnamesFile(string)",
                    "No se puede crear el archivo de configuración\n" + ex.Message);
                throw;
            }
        }

        public void GenerateTnsnamesFile(string sourceUrl, string targetUrl)
        {
            try
            {
                //iniciamos las variables de directorio y archivo
                var configFilePath = FileWriter.NormalizePath(targetUrl, true);
                var defaultFilePath = FileWriter.NormalizePath(CurrentSettings.DefaultTnsnamesFilePath, true);
                var sourceFilePath = FileWriter.NormalizePath(sourceUrl, true);
                var configFileName = CurrentSettings.TnsnamesFileName;

                //comprobamos que la ruta no corresponda a la ruta predeterminada
                if (configFilePath.Equals(defaultFilePath))
                    throw new Exception("No se puede reemplazar el archivo " + configFileName + " del sistema. Si desea modificarlo, comuníquese con el administrador del sistema");

                //creamos una copia de seguridad si el archivo existe
                if (FileWriter.CheckFileExist(Path.Combine(configFilePath, configFileName)))
                {
                    var backupFileName = configFileName + "_" + System.DateTime.Today.Year + System.DateTime.Today.Month + System.DateTime.Today.Day + ".BAK";
                    if (!FileWriter.CheckFileExist(Path.Combine(configFilePath, backupFileName)))
                        FileWriter.MoveFileToDirectory(configFileName, configFilePath,backupFileName, configFilePath);
                }

                //realizamos la acción
                FileWriter.CreateDirectory(configFilePath);
                FileWriter.CopyFileToDirectory(configFileName, sourceFilePath, configFilePath);
            }
            catch (Exception ex)
            {
                Debugger.LogError("GenerateEllipseTnsnamesFile(string, string)",
                    "No se puede crear el archivo de configuración\n" + ex.Message);
                throw;
            }
        }




        public void GenerateDatabaseFile(string targetUrl = null)
        {
            var databaseList = new List<DatabaseItem>();
            databaseList.Add(new DatabaseItem("Productivo", "EL8PROD", "SIGCON", "ventyx", "ELLIPSE", null, null));
            databaseList.Add(new DatabaseItem("Contingencia", "EL8PROD", "SIGCON", "ventyx", "ELLIPSE", null, null));
            databaseList.Add(new DatabaseItem("Desarrollo", "EL8DESA", "SIGCON", "ventyx", "ELLIPSE", null, null));
            databaseList.Add(new DatabaseItem("Test", "EL8TEST", "SIGCON", "ventyx", "ELLIPSE", null, null));
            databaseList.Add(new DatabaseItem("ellprod", "EL8PROD", "SIGCON", "ventyx", "ELLIPSE", null, null));
            databaseList.Add(new DatabaseItem("ellcont", "EL8PROD", "SIGCON", "ventyx", "ELLIPSE", null, null));
            databaseList.Add(new DatabaseItem("elldesa", "EL8DESA", "SIGCON", "ventyx", "ELLIPSE", null, null));
            databaseList.Add(new DatabaseItem("elltest", "EL8TEST", "SIGCON", "ventyx", "ELLIPSE", null, null));
            databaseList.Add(new DatabaseItem("SCADARDB", "PBVFWL01", "SCADARDBADMINGUI", "momia2011", "SCADARDB.DBO",
                null, "SCADARDB"));
            databaseList.Add(new DatabaseItem("SIGCOR", "SIGCOPRD", "CONSULBO", "consulbo", "@DBLELLIPSE8", "ELLIPSE", null));
            databaseList.Add(
                new DatabaseItem("SIGCOPRD", "SIGCOPRD", "CONSULBO", "consulbo", "@DBLELLIPSE8", "ELLIPSE", null));

            var xmlFile = "";

            xmlFile += @"<?xml version=""1.0"" encoding=""UTF-8""?>";
            xmlFile += @"<ellipse>";
            xmlFile += @"  <connections>";
            foreach (var item in databaseList)
                xmlFile += @"    <" + item.Name + " dbname='" + item.DbName + "' dbuser='" + item.DbUser +
                           "' dbpassword='' dbencodedpassword='" + item.DbEncodedPassword + "' dbreference='" +
                           item.DbReference + "' dblink='" + item.DbLink + "' " +
                           (string.IsNullOrWhiteSpace(item.DbCatalog) ? null : "dbcatalog='" + item.DbCatalog + "'") +
                           "/>";
            xmlFile += @"  </connections>";
            xmlFile += @"</ellipse>";

            try
            {
                //iniciamos las variables de directorio y archivo
                if (targetUrl == null)
                    targetUrl = CurrentSettings.LocalDataPath;
                var configFilePath = FileWriter.NormalizePath(targetUrl, true);
                var configFileName = CurrentSettings.DatabaseXmlFileName;
                
                //creamos una copia de seguridad si el archivo existe
                if (FileWriter.CheckFileExist(Path.Combine(configFilePath, configFileName)))
                {
                    var backupFileName = configFileName + "_" + System.DateTime.Today.Year + System.DateTime.Today.Month + System.DateTime.Today.Day + ".BAK";
                    if (!FileWriter.CheckFileExist(Path.Combine(configFilePath, backupFileName)))
                        FileWriter.MoveFileToDirectory(configFileName, configFilePath, backupFileName, configFilePath);
                }

                //realizamos la acción
                FileWriter.CreateDirectory(configFilePath);
                FileWriter.WriteTextToFile(xmlFile, configFileName, configFilePath);
            }
            catch (Exception ex)
            {
                Debugger.LogError("GenerateEllipseDatabaseFile",
                    "No se puede crear el archivo de bases de datos\n" + ex.Message);
                throw;
            }
        }
        public void DeleteDatabaseFile()
        {
            DeleteDatabaseFile(null);
        }
        public void DeleteDatabaseFile(string targetUrl = null)
        {
            try
            {
                //iniciamos las variables de directorio y archivo
                if (targetUrl == null)
                    targetUrl = CurrentSettings.LocalDataPath;
                var configFilePath = FileWriter.NormalizePath(targetUrl, true);
                var configFileName = CurrentSettings.DatabaseXmlFileName;
                
                FileWriter.DeleteFile(configFilePath, configFileName);
            }
            catch (Exception ex)
            {
                Debugger.LogError("No se puede eliminar el archivo de configuración", ex.Message);
                throw;
            }
        }

        #endregion
    }
}
