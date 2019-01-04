using CommonUi;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.Windows;

namespace VsixCommonWizard
{
    public class WizardImplementation : IWizard 
    {
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            try
            {
                var generatorName = ConfigHelper.GetAppSetting(CommonUi.Constants.GeneratorNameKey);
                var popUpDialog = new PopUpDialog(generatorName, replacementsDictionary);
                popUpDialog.Show();
            }
            catch (Exception ex)
            {
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
