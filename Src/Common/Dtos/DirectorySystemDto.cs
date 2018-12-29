using System.IO;

namespace Common.Dtos
{
    public class DirectorySystemDto 
    {
        public DirectoryInfo SolutionDirectoryInfo { get; set; }
        public FileSystemDto FileSystemDtoBase { get; set; }
    }
}