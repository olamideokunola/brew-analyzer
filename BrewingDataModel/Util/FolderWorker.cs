using System;
using System.IO;

namespace Util
{
    public class FolderWorker
    {
        public FolderWorker()
        {
        }

        public static bool IsSubDirectory(string parentFolderPath, string childFolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(parentFolderPath);

            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                if (subDir.Name == childFolderName)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool FileInFolder(string folderPath, string fileName)
        {
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Name == fileName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
