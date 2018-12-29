using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Common
{
    public class YeomanProcessor
    {
        private DirectorySystemDto _directorySystemDto;
        private FileSystemDto _fileSystemDto;
        private string _generatorName;

        public YeomanProcessor(string generatorName)
        {
            _generatorName = generatorName;
        }

        public FileSystemDto Initialise(Dictionary<string, string> replacementsDictionary)
        {
            var regularProjectName = replacementsDictionary["$safeprojectname$"];
            var solutionDirectory = replacementsDictionary["$solutiondirectory$"];
            var solutionDirectoryInfo = new DirectoryInfo(solutionDirectory);
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
                SolutionDirectoryInfo = solutionDirectoryInfo
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

            var yoBatchFile = $@"{assemblyDirectory}\yo.bat";
            var args = $"{_generatorName} {generationDirectory}";

            //gregt cater for generationDirectory already exists 

            InvokeCommand(yoBatchFile, args);
        }

        private static void ArchiveRegularProject(string solutionDirectory, string tempDirectory, DirectoryInfo solutionDirectoryInfo)
        {
            //gregt cater for directory not exists / already exists

            var archiveLocation = $"{tempDirectory}\\{solutionDirectoryInfo.Name}";
            Directory.Move(solutionDirectory, archiveLocation);
        }

        private void InvokeCommand(string batchFileToBeOpened, string args)
        {
            var start = new ProcessStartInfo()
            {
                Arguments = args,
                CreateNoWindow = false,
                FileName = batchFileToBeOpened,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Normal,
            };

            try
            {
                using (Process.Start(start)) { }
            }
            catch (Exception ex)
            {
                //gregt do some vsix logging here
                throw (ex);
            }
        }
    }
}