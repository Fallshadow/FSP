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
        public ObjectStylingStrategySO Config = null;
        private List<ObjectStylingStrategyBase> osBases = new List<ObjectStylingStrategyBase>();
        
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

        public ObjectStylingStrategyBase CreateOrGetStrategy(ObjectStylingType type)
        {
            foreach (var item in osBases)
            {
                if(item.curInfo.IdType != type) continue;
                return item;
            }
            
            foreach (var item in Config.ObjectPathStructs)
            {
                if (item.IdType != type) continue;
                ObjectStylingStrategyBase osStrategyBase = ObjectStylingFactory.CreateStrategy(item);
                osBases.Add(osStrategyBase);
                return osStrategyBase;
            }

            return null;
        }

        public void ReleaseStrategy(ObjectStylingType type)
        {
            ObjectStylingStrategyBase osStrategyBase = null;
            foreach (var item in osBases)
            {
                if(item.curInfo.IdType != type) continue;
                osStrategyBase = item;
                break;
            }

            if (osStrategyBase == null) return;
            osBases.Remove(osStrategyBase);
            osStrategyBase.Release();
        }
    }
}