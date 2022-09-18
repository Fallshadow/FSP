using System;
using UnityEngine;

namespace fsp.utility
{
    public static partial class Utility
    {
        public static void UnLoadAllUnusedAssets()
        {
            Resources.UnloadUnusedAssets();
        }
        
        public static void GcCollect()
        {
            GC.Collect();
        }
    }
}