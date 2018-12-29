using Common;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AngularBasicVsix
{
    public class WizardImplementation : IWizard 
    {
        /// <summary>
        /// https://github.com/MattJeanes/AngularBasic
        /// </summary>
        private const string generatorName = "angular-basic";//gregt move to config file or similar, include url in dialog ?

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            try
            {
                var yoProcessor = new YoProcessor(generatorName);
                var dto = yoProcessor.Initialise(replacementsDictionary);
                var userInputForm = new UserInputForm(dto.SolutionDirectory, dto.TempDirectory, generatorName, dto.RegularProjectName);

                var dialogResult = userInputForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    yoProcessor.Generate();
                }
                else
                {
                    //gregt archive the regular project
                }
            }
            catch (Exception ex)
            {
                // No need to perform logging here - any exceptions are displayed in UI to user
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
