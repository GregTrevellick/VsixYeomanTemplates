using System.IO;
using SysConf = System.Configuration;

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
            //
            //gregt tempry comment
            //
            //always ang-bas, even tho ang-bas & omni both exist, both with correct app.config files
            //switches to omni after ;
            //reseting experimental instance & rebuilding
            //
            //so this is what happens
            //
            //the commonui doesnt exist on disc
            //we build ang-bas, which creates commonui on disc
            //we run ang-bas, which puts commonui & app.config into experimental folder for ang-bas successfully AND uses the commonui in this ang-bas folder
            //
            //we delete the commonui off disc
            //we build omni, which recreates commonui on disc
            //we run omni, which puts commonui & app.config into experimental folder for omni successfully BUT uses the commonui in the ANG-BAS folder, not the omni folder
            //
            //presumably because experimental uses the first it finds with correct signature (even when recreated from scratch)
            //
            //System.Reflection.Assembly.GetExecutingAssembly() always looks up commonui.dll & finds the first one ever created in experimental!
            //
            //if confighelper.cs was in the vsix itself the GetExecutingAssembly() will get the angbas.dll or omni.dll so in theory will pick up correct app.config
            //

            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            var exeConfigurationFileMap = new SysConf.ExeConfigurationFileMap
            {
                ExeConfigFilename = assemblyDirectory + @"\App.config"
            };
            var configuration = SysConf.ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, SysConf.ConfigurationUserLevel.None);

            var generatorName = configuration.AppSettings.Settings[appSettingKey].Value;
            return generatorName;
        }
    }
}
