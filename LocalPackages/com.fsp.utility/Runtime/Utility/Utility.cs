using System.IO;
using UnityEngine;

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

        public static Bounds GetGoRendererBounds(GameObject go)
        {
            Renderer[] mrs = go.GetComponentsInChildren<Renderer>();
            Vector3 center = go.transform.position;
            Bounds bounds = new Bounds(center, Vector3.zero);
            if (mrs.Length != 0)
            {
                foreach (Renderer mr in mrs)
                {
                    bounds.Encapsulate(mr.bounds);
                }
            }

            return bounds;
        }
    }
}