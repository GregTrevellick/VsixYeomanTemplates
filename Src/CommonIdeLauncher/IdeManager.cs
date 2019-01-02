using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CommonIdeLauncher
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
            var start = new ProcessStartInfo()
            {
                CreateNoWindow = false,
                FileName = fileToBeOpened,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal,
            };

            //gregt try/catch?
            using (Process.Start(start)) { }
        }
    }
}