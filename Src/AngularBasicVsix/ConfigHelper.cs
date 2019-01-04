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
            //switches to omni after reseting experimental instance & rebuilding
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
            //if confighelper.cs was in the vsix itself as shortcut to commonui the GetExecutingAssembly() will still get the commonui.dll 
            //if confighelper.cs was in the vsix itself the GetExecutingAssembly() will get the angbas.dll or omni.dll so in theory will pick up correct app.config
            //
            //the reason commonui.dll is used over ang-bas.dll / omni.dll in GetExecutingAssembly() is cos the 2 .vstemplate files directly cause commonui.dll to be run, and by definition as microsoft website commonui.dll gets installed in GAC !!!
            //this means the only way to fix the issue is 
            // -to have DIFFERENTLY NAMED ASSEMBLIES IN THE .VSTEMPLATE FILES
            // -force the directory name (via ctor param) rather than GetExecutingAssembly() - but we need to know ang-bas is in full with spaces, and we can't be 100% certain of that forever so FORGET THIS OPTION, GO WITH THE ONE ABOVE
            // -we can't use diff classes or namespaces in same commonui.dll as the dll is still shared in GAC
            // -we can't use settings.settings as these are referred to in the app.config file itself anyway
            // -cant alter vsixes to deploy the vsix dll, as GetExecutingAssembly() will still find commonui.dll
            // -cant pass genname to wizard as ide passes 'customParams' and we can only add to 'replacementsDictionary' not supply it
            // -cant WizardImplementation.CS as SHORTCUT to the v6 projects as GetExecutingAssembly() will still find commonui.dll
            //
            //
            //
            //so we need to 
            // -MOVE WizardImplementation.CS and/or ConfigHelper.cs directly TO THE VSIX PROJECTS THEMSELVES (where they first started)
            //OR
            // -move confighelper.cs into a CommonConfig project
            // -create CommonConfigAngBas.dll which references CommonConfig
            // -create CommonConfigOmni.dll which references CommonConfig.
            // -set the .vstemplate files to use their respective CommonConfigXxxxx.dll
            // -remove reference to commonui.dll from the vsix projects 
            // -set vsix projects to reference their respective CommonConfigXxxxx.dll
            //

            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            var exeConfigurationFileMap = new SysConf.ExeConfigurationFileMap
            {
                ExeConfigFilename = assemblyDirectory + @"\App.config"
            };
            var configuration = SysConf.ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, SysConf.ConfigurationUserLevel.None);

            var generatorName = configuration.AppSettings.Settings[appSettingKey].Value;


    //        var generatorName = SysConf.ConfigurationSettings.AppSettings.Get(appSettingKey);
            return generatorName;
        }
    }
}
