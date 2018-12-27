using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Process = System.Diagnostics.Process;

namespace YoAngularBasic
{
    public class WizardImplementation : IWizard 
    {
        private const string generatorName = "angular-basic";//https://github.com/MattJeanes/AngularBasic
        //private const string generatorName = "aspnet";//https://github.com/OmniSharp/generator-aspnet

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            try
            {
                // initialisation
                var regularProjectName = replacementsDictionary["$safeprojectname$"];
                var solutionDirectory = replacementsDictionary["$solutiondirectory$"];
                var solutionDirectoryInfo = new DirectoryInfo(solutionDirectory);
                var tempDirectory = Path.GetTempPath();

                // generate ui
                var userInputForm = new UserInputForm(solutionDirectory, tempDirectory, generatorName, regularProjectName);
                userInputForm.ShowDialog();

                // within a few milli-seconds the regular new project (in our case literally just an empty folder due to MinimalProjectTemplate.vstemplate having empty 'TemplateContent' node) is created

                // run batch file that gathers user input & performs downloads 
                GenerateYeomanProject(solutionDirectoryInfo.Parent.FullName);

                // now that yeoman has done its thing we are at the only point in code where we can try to archive the regular project, safe in the knowledge that enough time has passed to gaurantee it was created successfully
                ArchiveRegularProject(solutionDirectory, tempDirectory, solutionDirectoryInfo);
            }
            catch (Exception ex)
            {
                //gregt do some vsix logging here
                MessageBox.Show(ex.ToString());
            }
        }
       
        private void GenerateYeomanProject(string generationDirectory)
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            var yoBatchFile = $@"{assemblyDirectory}\yo.bat";
            var args = $"{generatorName} {generationDirectory}";

            //gregt cater for generationDirectory already exists 

            InvokeCommand(yoBatchFile, args);
        }

        private static void ArchiveRegularProject(string solutionDirectory, string tempDirectory, DirectoryInfo solutionDirectoryInfo)
        {
            //gregt cater for directory not exists / already exists

            var archiveLocation = $"{tempDirectory}\\{solutionDirectoryInfo.Name}";
            Directory.Move(solutionDirectory, archiveLocation);
        }

        private void InvokeCommand(string batchFileToBeOpened, string args)
        {
            var start = new ProcessStartInfo()
            {
                Arguments = args,
                CreateNoWindow = false,
                FileName = batchFileToBeOpened,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Normal,
            };

            try
            {
                using (Process.Start(start)) { }
            }
            catch (Exception ex)
            {
                //gregt do some vsix logging here
                throw (ex);
            }
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
