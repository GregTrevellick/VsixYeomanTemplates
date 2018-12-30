using System;

namespace Common
{
    public static class DialogHelper
    {
        public static string Ok = "OK";

        public static string GetLabelText(string solutionDirectory, string tempDirectory, string generatorName, string regularProjectName)
        {
            return 
                $"The following will happen:" + Environment.NewLine +
                $"{Environment.NewLine}" +
                $" - A new project named '{regularProjectName}' will be created at {solutionDirectory}{Environment.NewLine}" +
                $" - A command prompt window will open and run the following commands{Environment.NewLine}" +
                $"    - npm install -g yo generator-{generatorName}{Environment.NewLine}" +
                $"    - yo {generatorName}{Environment.NewLine}" +
                $" - The new yeoman generated '{generatorName}' project will be launched in a NEW instance of Visual Studio{Environment.NewLine}" +
                $" - The '{regularProjectName}' project (which is actually just an empty folder) will be moved{Environment.NewLine}" +
                $"        from    {solutionDirectory}{Environment.NewLine}" +
                $"        to      {tempDirectory} (albeit with a unique date/time identifier suffix){Environment.NewLine} " +
                $"{Environment.NewLine}" + 
                $"{Environment.NewLine}Click {Ok} to proceed.";
        }
    }
}