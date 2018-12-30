using System;

namespace Common
{
    public static class DialogHelper
    {
        public static string Ok = "OK";

        public static string GetLabelText(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            return 
                $"The following will happen:{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $" - A new project called {regularProjectName} will be created at {solutionDirectory}{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $" - A command prompt window will open and run the following commands{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"      npm install -g yo generator-{generatorName}{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"      yo {generatorName}{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"   (this will scaffold your new project locally){Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $" - The newly generated {generatorName} project will be launched in a new instance of Visual Studio{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $" - The {regularProjectName} project (nothing more than an empty folder) will be moved{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"        from    {solutionDirectory}{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"        to       {tempDirectory} (with a unique date/time identifier suffix){Environment.NewLine} " +
                $"{Environment.NewLine}Click {Ok} to proceed.";
        }
    }
}