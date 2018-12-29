using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Common
{
    public class YoProcessor
    {
        private DirectoryInfo _solutionDirectoryInfo;
        private DirectorySystemDto _directorySystemDto;
        private FileSystemDto _fileSystemDto;
        private string _generatorName;

        public YoProcessor(string generatorName)
        {
            _generatorName = generatorName;
        }

        public FileSystemDto Initialise(Dictionary<string, string> replacementsDictionary)
        {
            var regularProjectName = replacementsDictionary["$safeprojectname$"];
            var solutionDirectory = replacementsDictionary["$solutiondirectory$"];
            _solutionDirectoryInfo = new DirectoryInfo(solutionDirectory);
            var tempDirectory = Path.GetTempPath();

            _fileSystemDto = new FileSystemDto
            {
                RegularProjectName = regularProjectName,
                SolutionDirectory = solutionDirectory,
                TempDirectory = tempDirectory
            };

            _directorySystemDto = new DirectorySystemDto
            {
                FileSystemDtoBase = _fileSystemDto,
                SolutionDirectoryInfo = _solutionDirectoryInfo
            };

            return _fileSystemDto;
        }

        public void Generate()
        {
            GenerateYeomanProject(_directorySystemDto.SolutionDirectoryInfo.Parent.FullName);

            // now that yeoman has done its thing we are at the only point in code where we can try to archive the regular project, safe in the knowledge that enough time has passed to gaurantee it was created successfully
            ArchiveRegularProject(_fileSystemDto.SolutionDirectory, _fileSystemDto.TempDirectory, _directorySystemDto.SolutionDirectoryInfo);
        }

        private void GenerateYeomanProject(string generationDirectory)
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            // Matt's code already caters for the ang-bas folder already existing - we don't need to check anything here
            var yoBatchFile = $@"{assemblyDirectory}\yo.bat";
            var args = $"{_generatorName} {generationDirectory}";
            CreateYoProjectOnDisc(yoBatchFile, args);

            OpenNewlyCreatedYoProject();
        }

        private void OpenNewlyCreatedYoProject()
        {
            //gregt
            //build "array" of.csproj files in sub-folders
            //sort by newest date
            //Open the last .csproj which is the.csproj file we just created

            //ideally we would open the newly generated project in the existing instance of Visual Studio, but the closest I got to this was 
            //"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe" / edit C:\temp\j2\j2.csproj 
            //which opens the.csproj file in existing VS instance(not the experimental one), but as a text file not as a project file
        }

        public void ArchiveRegularProject(string solutionDirectory, string tempDirectory)
        {
            ArchiveRegularProject(solutionDirectory, tempDirectory, _solutionDirectoryInfo);
        }

        private void ArchiveRegularProject(string solutionDirectory, string tempDirectory, DirectoryInfo solutionDirectoryInfo)
        {
            var archiveLocation = GetArchiveLocation(tempDirectory, solutionDirectoryInfo);
            Directory.Move(solutionDirectory, archiveLocation);
        }

        private string GetArchiveLocation(string tempDirectory, DirectoryInfo solutionDirectoryInfo)
        {
            var dateTime = GetUniqueDateTime();
            var archiveLocation = $"{tempDirectory}\\{solutionDirectoryInfo.Name}_{dateTime}";
            if (Directory.Exists(archiveLocation))
            {
                archiveLocation = GetUniqueLocation(archiveLocation);
            }
            return archiveLocation;
        }

        private string GetUniqueLocation(string location)
        {
            var random = new Random();
            return $"{location}_{random.Next(1, 999999)}";
        }

        private string GetUniqueDateTime()
        {
            return $"{DateTime.UtcNow.Year}{DateTime.UtcNow.Month}{DateTime.UtcNow.Day}_{DateTime.UtcNow.Hour}{DateTime.UtcNow.Minute}{DateTime.UtcNow.Second}_{DateTime.UtcNow.Millisecond}";
        }

        private void CreateYoProjectOnDisc(string batchFileToBeOpened, string args)
        {
            var start = new ProcessStartInfo()
            {
                Arguments = args,
                CreateNoWindow = false,
                FileName = batchFileToBeOpened,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Normal,
            };

            // No need for a try/catch here - any exceptions are caught by the calling UserForm and displayed in the dialog to user
            using (Process.Start(start)) { }
        }
    }
}