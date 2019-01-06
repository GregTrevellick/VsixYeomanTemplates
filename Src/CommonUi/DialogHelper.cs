using System;

namespace CommonUi
{
    public static class DialogHelper
    {
        public static string Ok = "Okay";//gregt use this to define the button text 

        public static string GetLabelText(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            return
                $"Click {Ok} to perform the following:{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $" - Create empty project '{regularProjectName}' at '{solutionDirectory}'{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $" - Run the command 'npm install -g yo generator-{generatorName}' to install the Yeoman generator{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $" - Run the command 'yo {generatorName}' to scaffold the Yeoman project{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $" - Open the {generatorName} .csproj file in a new instance of it's default program (typically Visual Studio){Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $" - Move '{regularProjectName}' to '{tempDirectory}'{Environment.NewLine} ";
        }
    }
}