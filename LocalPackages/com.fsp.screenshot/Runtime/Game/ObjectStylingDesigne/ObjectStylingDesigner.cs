using System;
using System.Collections.Generic;
using fsp.assetbundlecore;
using fsp.modelshot.data;
using fsp.utility;
using UnityEngine;

namespace fsp.modelshot.Game.ObjectStylingDesigne
{
    public class ObjectStylingDesigner : SingletonMonoBehavior<ObjectStylingDesigner>
    {
        public List<GameObject> curHoldGos = new List<GameObject>();
        public CreateObjectPathSO createObjectPathConfig = null;
        
        private void Start()
        {
            initSO();
        }

        private void initSO()
        {
            if (createObjectPathConfig != null) return;
            int hashCode = Utility.GetHashCodeByAssetPath(ResourcesPathSetting.CREATEOBJECTPATHSO_VIRTUAL_FILE_PATH);
            createObjectPathConfig = ResourceLoaderProxy.instance.LoadAsset<CreateObjectPathSO>(hashCode);
        }
    }
}