using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CommonIdeLauncher
{
    public class IdeManager
    {
        public void OpenNewlyCreatedYoProject(string generationDirectory)
        {
            try
            {
                var csprojFileName = GetCsprojFileName(generationDirectory);
                LaunchCsprojInIde(csprojFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error occured when attempting to launch the new project/solution in Visual Studio (or it's default program on your machine).");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        private string GetCsprojFileName(string generationDirectory)
        {
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

            using (Process.Start(start)) { }
        }
    }
}