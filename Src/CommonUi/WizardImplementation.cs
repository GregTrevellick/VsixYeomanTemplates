using CommonUi;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using SysConf = System.Configuration;

namespace VsixCommonWizard
{
    public class WizardImplementation : IWizard 
    {
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            try
            {
                var generatorName = GetAppSetting("GeneratorName");
                var popUpDialog = new PopUpDialog(generatorName, replacementsDictionary);
                popUpDialog.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string GetAppSetting(string appSettingKey)//gregt move to separate file to overcome SysConf namespace collisions
        {
            //https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager.openmappedexeconfiguration?redirectedfrom=MSDN&view=netframework-4.7.2#overloads
            //https://stackoverflow.com/questions/505566/loading-custom-configuration-files
            var assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();
            var assemblyLocation = assemblyInfo.Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
            var exeConfigurationFileMap = new SysConf.ExeConfigurationFileMap
            {
                ExeConfigFilename = assemblyDirectory + @"\App.config"
            };
            var configuration = SysConf.ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, SysConf.ConfigurationUserLevel.None);
            var generatorName = configuration.AppSettings.Settings[appSettingKey].Value;
            return generatorName;
        }

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            // This method is called before opening any item that has the OpenInEditor attribute.  
        }

        public void ProjectFinishedGenerating(Project project)
        {
            // This method not called when we create an empty directory only as our regular project.
        }

        public void RunFinished()
        {
            // This method is called after the project is created.  
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            // This method is only called for item templates, not for project templates.  
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            // This method is only called for item templates, not for project templates.  
            return true;
        }
    }
}
