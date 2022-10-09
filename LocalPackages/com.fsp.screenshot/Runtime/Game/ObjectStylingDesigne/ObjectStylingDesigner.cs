using System.Collections.Generic;
using fsp.assetbundlecore;
using fsp.modelshot.data;

namespace fsp.ObjectStylingDesigne
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

        protected virtual void initSO()
        {
            if (Config != null) return;
            Config = ResourceLoaderProxy.instance.LoadAsset<ObjectStylingStrategySO>(ResourcesPathSetting.CREATEOBJECTPATHSO_VIRTUAL_FILE_PATH);
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