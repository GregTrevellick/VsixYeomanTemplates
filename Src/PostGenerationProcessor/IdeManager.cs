using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PostGenerationProcessor
{
    public class IdeManager
    {
        public void OpenNewlyCreatedYoProject(string generationDirectory)
        {
            var csprojFileName = GetCsprojFileName(generationDirectory);
            LaunchCsprojInIde(csprojFileName);
        }
        private string GetCsprojFileName(string generationDirectory)
        {
            //gregt try/catch?
            var dirInfo = new DirectoryInfo(generationDirectory);
            var files = dirInfo.GetFiles("*.cSpRoJ", SearchOption.AllDirectories).OrderByDescending(p => p.CreationTime);
            return files.FirstOrDefault().FullName;
        }

        private void LaunchCsprojInIde(string fileToBeOpened)
        {
            var start = new ProcessStartInfo()//gregt dedupe ?
            {
                //Arguments = args,
                CreateNoWindow = false,
                FileName = fileToBeOpened,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Normal,
            };

            //gregt try/catch?
            using (Process.Start(start)) { }

            //ideally we would open the newly generated project in the existing instance of Visual Studio, but the closest I got to this was 
            //"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe" / edit C:\temp\j2\j2.csproj 
            //which opens the.csproj file in existing VS instance(not the experimental one), but as a text file not as a project file

        }
    }
}