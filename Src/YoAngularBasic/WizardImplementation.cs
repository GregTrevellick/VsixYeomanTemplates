using Common;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YoAngularBasic
{
    public class WizardImplementation : IWizard 
    {
        /// <summary>
        /// https://github.com/MattJeanes/AngularBasic
        /// </summary>
        private const string generatorName = "angular-basic";

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            try
            {
                var yeomanProcessor = new YeomanProcessor(generatorName);
                var dto = yeomanProcessor.Initialise(replacementsDictionary);
                var userInputForm = new UserInputForm(dto.SolutionDirectory, dto.TempDirectory, generatorName, dto.RegularProjectName);

                userInputForm.ShowDialog();

                yeomanProcessor.Generate();
            }
            catch (Exception ex)
            {
                //gregt do some vsix logging here
                MessageBox.Show(ex.ToString());
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
