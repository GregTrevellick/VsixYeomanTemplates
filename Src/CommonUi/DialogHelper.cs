using System;

namespace CommonUi
{
    public static class DialogHelper
    {
        public static string Okay = "Okay";

        public static string GetLabelText(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            return
             $"Click {Okay} to run the following commands:{Environment.NewLine}{Environment.NewLine}" +
             $" - 'npm install -g yo generator-{generatorName}' (this will install the Yeoman generator){Environment.NewLine}{Environment.NewLine}" +
             $" - 'yo {generatorName}' (this will scaffold the project and open it in a new instance Visual Studio /default program){Environment.NewLine}{Environment.NewLine}" +
             $" - Create an empty project named '{regularProjectName}' and move it to '{tempDirectory}'";
            
            //return
            //    $"Click {Okay} to perform the following:{Environment.NewLine}" +
            //    $"{Environment.NewLine}" +
            //    $"{Environment.NewLine}" +
            //    $" - Run the command 'npm install -g yo generator-{generatorName}' to install the Yeoman generator{Environment.NewLine}" +
            //    $"{Environment.NewLine}" +
            //    $" - Run the command 'yo {generatorName}' to scaffold the Yeoman project{Environment.NewLine}" +
            //    $"{Environment.NewLine}" +
            //    $" - Open the {generatorName} .csproj file in a new instance of it's default program (typically Visual Studio){Environment.NewLine}" +
            //    $"{Environment.NewLine}" +
            //    $" - Move '{regularProjectName}' to '{tempDirectory}'{Environment.NewLine} ";
        }
    }
}