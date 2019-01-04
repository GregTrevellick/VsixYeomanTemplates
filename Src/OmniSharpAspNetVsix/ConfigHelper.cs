using System.Configuration;
using System.IO;

namespace CommonUi
{
    public static class ConfigHelper
    {
        public static string GetAppSetting(string appSettingKey)
        {
            //https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager.openmappedexeconfiguration?redirectedfrom=MSDN&view=netframework-4.7.2#overloads
            //https://stackoverflow.com/questions/505566/loading-custom-configuration-files

            var assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();
            var assemblyLocation = assemblyInfo.Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            var exeConfigurationFileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = assemblyDirectory + @"\App.config"
            };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);

            var appSettingValue = configuration.AppSettings.Settings[appSettingKey].Value;
            return appSettingValue;
        }
    }
}
