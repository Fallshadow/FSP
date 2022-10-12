using System.Collections.Generic;
using fsp.assetbundlecore;
using fsp.modelshot.data;
using fsp.ObjectStylingDesigne;
using UnityEngine;

namespace fsp.CameraScheme
{
    [CreateAssetMenu]
    public class CameraSchemeSO : ScriptableObject
    {
        private static CameraSchemeSO _instance;
        public static CameraSchemeSO Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = ResourceLoaderProxy.instance.LoadAsset<CameraSchemeSO>(ResourcesPathSetting.CAMERA_SCHEME_INFO_FILE_PATH);
                }
                return _instance;
            }
        }
        public List<CameraSchemeInfo> M_LightDatas = new List<CameraSchemeInfo>();
    }
}