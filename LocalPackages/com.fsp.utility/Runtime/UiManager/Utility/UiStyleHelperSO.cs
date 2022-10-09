using fsp.assetbundlecore;
using fsp.data;
using UnityEngine;

namespace fsp.ui.utility
{
    [CreateAssetMenu(fileName = "UiStyleHelperSO", menuName = "FSPUI/UiStyleHelper")]
    public class UiStyleHelperSO : ScriptableObject
    {
        private static UiStyleHelperSO _instance;
        public static UiStyleHelperSO Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = ResourceLoaderProxy.instance.LoadAsset<UiStyleHelperSO>(ResourcesPathSetting.UI_STYLE_HELP_SO);
                }
                return _instance;
            }
        }
        
        public Material GrayscaleMat;
        public Material GrayscaleGlowMat = null;
    }
}