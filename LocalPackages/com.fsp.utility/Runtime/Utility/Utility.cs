using System.IO;

namespace fsp.utility
{
    // 静态通用方法
    public static partial class Utility
    {
        public static void CreateDirectoryFolderPath(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
        }
        
        public static string CreateDirectoryFolderPathAndReturn(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            return FolderPath;
        }
    }
}