using System;
using System.Collections.Generic;
using fsp.assetbundlecore;
using fsp.modelshot.data;
using UnityEngine;

namespace fsp.ObjectStylingDesigne
{
    [CreateAssetMenu]
    public class ObjectWorldInfoSO : ScriptableObject
    {
        private static ObjectWorldInfoSO _instance;
        public static ObjectWorldInfoSO Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = ResourceLoaderProxy.instance.LoadAsset<ObjectWorldInfoSO>(ResourcesPathSetting.OBJECT_WORLD_INFO_FILE_PATH);
                }
                return _instance;
            }
        }
        
        public List<ObjectWorldInfo> WorldInfos = new List<ObjectWorldInfo>();

        public List<ObjectStylingWorldTransInfo> GetObjectStylingWorldTransInfos(string searchStr)
        {
            foreach (var item in WorldInfos)
            {
                if (!string.Equals(item.WorldInfoName, searchStr, StringComparison.Ordinal)) continue;
                return item.CInfos;
            }

            return null;
        }
    }
}