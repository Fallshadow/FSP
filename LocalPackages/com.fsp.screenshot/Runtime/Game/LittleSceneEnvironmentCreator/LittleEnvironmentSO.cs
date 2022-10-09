using System;
using System.Collections.Generic;
using fsp.assetbundlecore;
using fsp.modelshot.data;
using fsp.utility;
using UnityEngine;

namespace fsp.LittleSceneEnvironment
{
    [CreateAssetMenu]
    public class LittleEnvironmentSO : ScriptableObject
    {
        private static LittleEnvironmentSO _instance;
        public static LittleEnvironmentSO Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = ResourceLoaderProxy.instance.LoadAsset<LittleEnvironmentSO>(ResourcesPathSetting.ENVIRONMENT_INFO_FILE_PATH);
                }
                return _instance;
            }
        }

        public List<LittleEnvironmentInfo> littleEnvironmentInfos = new List<LittleEnvironmentInfo>();

        public GameObject LoadEnvironment(string environmentName)
        {
            GameObject result = null;
            foreach (var item in littleEnvironmentInfos)
            {
                if (!String.Equals(item.environmentName, environmentName, StringComparison.Ordinal)) continue;
                result = Utility.LoadPrefab(item.environmentPath, null);
            }

            return result;
        }
    }
}