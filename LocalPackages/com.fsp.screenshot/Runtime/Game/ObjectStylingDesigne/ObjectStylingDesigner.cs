using System;
using System.Collections.Generic;
using fsp.assetbundlecore;
using fsp.modelshot.data;
using fsp.utility;
using UnityEngine;

namespace fsp.modelshot.Game.ObjectStylingDesigne
{
    // 管理策略们
    public class ObjectStylingDesigner : SingletonMonoBehavior<ObjectStylingDesigner>
    {
        public List<GameObject> curHoldGos = new List<GameObject>();
        public ObjectStylingStrategySO Config = null;
        
        private void Start()
        {
            initSO();
        }

        private void initSO()
        {
            if (Config != null) return;
            int hashCode = Utility.GetHashCodeByAssetPath(ResourcesPathSetting.CREATEOBJECTPATHSO_VIRTUAL_FILE_PATH);
            Config = ResourceLoaderProxy.instance.LoadAsset<ObjectStylingStrategySO>(hashCode);
        }
    }
}