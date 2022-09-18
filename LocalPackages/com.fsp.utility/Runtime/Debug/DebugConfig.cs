using UnityEngine;

namespace fsp.debug
{
    public class DebugConfig : SingletonMonoBehavior<DebugConfig>
    {
        [Tooltip("AB包加载开关")]
        public bool IsDisableAssetBundle = false;
    }
}