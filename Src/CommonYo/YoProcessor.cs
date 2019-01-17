using CommonYo.Dtos;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace CommonYo
{
    public class YoProcessor : IDisposable
    {
        private readonly string _captionPrefix = $"New {_generatorName} Yeoman Project";
        private string _checkGenerationDirectory { get { return $"Check {_newProjectDirectory} to see if your {_generatorName} project was created successfully."; } }
        private DirectorySystemDto _directorySystemDto;
        private int _exitCode;
        private FileSystemDto _fileSystemDto;
        private static string _generatorName;
        private string _newProjectDirectory;
        private int _processId;
        private string _processName;
        private DirectoryInfo _solutionDirectoryInfo;
        private readonly string _unexpectedError = "An unexpected error has occurred.";
        private readonly int _yoCommandTimeOutSeconds = 15 * 60;
        private int _yoCommandTimeOutMilliSeconds { get { return _yoCommandTimeOutSeconds * 1000; } }

        public string LineBreak = $"{Environment.NewLine}{Environment.NewLine}";
        public string ProjectNotCreated = "Yeoman project was not created.";

        public YoProcessor(string generatorName)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
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
        }

        private void GenerateYeomanProject(string generationDirectory)
        {
            _newProjectDirectory = generationDirectory;

            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            // Generator's should already cater for the ang-bas/omni folder already existing - we don't need to check anything here
            var yoBatchFile = $@"{assemblyDirectory}\yo.bat";
            
            // Wrap each arg in quotes so that batch file caters for spaces in path names etc
            var args = $"\"{_generatorName}\" \"{generationDirectory}\" \"{assemblyDirectory}\"";

            RunYoBatchFile(yoBatchFile, args, generationDirectory);
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

        private void RunYoBatchFile(string batchFileToBeOpened, string args, string generationDirectory)
        {
            var processStartInfo = new ProcessStartInfo()
            {
                Arguments = args,
                CreateNoWindow = false,
                FileName = batchFileToBeOpened,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal, 
            };

            var startTime = DateTime.UtcNow;
            var process = Process.Start(processStartInfo);

            _processId = process.Id;
            _processName = process.ProcessName;

            process.WaitForExit(_yoCommandTimeOutMilliSeconds);

            _exitCode = process.ExitCode;

            if (process.HasExited)
            {
                HandleNormalExit(generationDirectory);
            }
            else
            {
                HandleAbnormalExit(generationDirectory, startTime, process);
            }

        }

        private void HandleNormalExit(string generationDirectory)
        {
            switch (_exitCode)
            {
                // Happy path
                case 0:
                    ShowMessageBoxSuccess($"Yeoman {_generatorName} project was successfully created at {generationDirectory}");
                    break;

                // TO DEBUG TEST: add 'exit /b 1' as first command in yo.bat 
                case 1:
                    ShowMessageBoxError($"{ProjectNotCreated}{LineBreak}{GetProcessDetails()}");
                    break;

                // TO DEBUG TEST: manually close command prompt (cross in top RHS corner)
                case -1073741510:
                    ShowMessageBoxWarning($"{ProjectNotCreated}");
                    break;

                // TO DEBUG TEST: add 'exit /b 2' as first command in yo.bat
                default:
                    ShowMessageBoxError($"{_unexpectedError}{LineBreak}{GetProcessDetails()}");
                    break;
            }
        }

        private void HandleAbnormalExit(string generationDirectory, DateTime startTime, Process process)
        {
            if (process.Responding)
            {
                HandleResponsiveAbnormalExit(generationDirectory, startTime, process);
            }
            else
            {
                HandleNonResponsiveAbnormalExit(process);
            }
        }

        private void HandleResponsiveAbnormalExit(string generationDirectory, DateTime startTime, Process process)
        {
            process.CloseMainWindow();

            var endTime = DateTime.UtcNow;
            var duration = (endTime - startTime).TotalMilliseconds;

            if (duration > _yoCommandTimeOutMilliSeconds)
            {
                // TO DEBUG TEST: set the timeout to, say, 10 secs, wait for 10 secs to expire (no need to create a project)
                var yoCommandTimeOutText = _yoCommandTimeOutSeconds <= 60 ?
                    $"{_yoCommandTimeOutSeconds} seconds" :
                    $"{_yoCommandTimeOutSeconds / 60} minutes";

                ShowMessageBoxWarning($"Creation of your Yeoman {_generatorName} project was interupted as it exceeded the timeout of {yoCommandTimeOutText}.{LineBreak}{_checkGenerationDirectory }");
            }
            else
            {
                // TO DEBUG TEST: as above but drag cursor to here
                ShowMessageBoxError($"Creation of your Yeoman  {_generatorName} project unexpectedly ended. Please try again.{LineBreak}{_checkGenerationDirectory }");
            }
        }

        private void HandleNonResponsiveAbnormalExit(Process process)
        {
            process.Kill();

            // TO DEBUG TEST: as above but drag cursor to here
            ShowMessageBoxError($"{_unexpectedError} {GetProcessDetails()}");
        }

        private string GetProcessDetails()
        {
            return $"Process '{_processName}' with id {_processId} ended with exit code {_exitCode}.";
        }

        public void ShowMessageBoxError(string messageBoxText)
        {
            ShowMessageBox(messageBoxText, "Error", MessageBoxImage.Error);
        }

        private void ShowMessageBoxSuccess(string messageBoxText)
        {
            ShowMessageBox(messageBoxText, "Success", MessageBoxImage.Information);
        }

        private void ShowMessageBoxWarning(string messageBoxText)
        {
            ShowMessageBox(messageBoxText, "Warning", MessageBoxImage.Warning);
        }

        private void ShowMessageBox(string messageBoxText, string caption, MessageBoxImage messageBoxImage)
        {
            MessageBox.Show(
                messageBoxText,
                $"{_captionPrefix} {caption}",
                MessageBoxButton.OK,
                messageBoxImage);
        }

        public void Dispose()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
        }
    }
}