using System;
using System.Collections.Generic;
using fsp.utility;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace fsp.ObjectStylingDesigne
{
    // 物体创造的策略基类，属于整体大策略，里面会有自己的物体摆放细节
    // 简单来说做这样几件事：
    // 创建策略信息
    // 供给外部获取策略信息
    // 创建骨架并在骨架上安放自己的物体！随后撒手不管
    // 创建骨架并在骨架上安放自己的物体！记录这次的骨架引用
    // 销毁某次骨架
    // 销毁策略信息
    public abstract class ObjectStylingStrategyBase
    {
        public List<String> ObjectNames = new List<string>();
        public List<String> SubStrategyNames = new List<string>();
        // 另开的子策略，以备特殊需求
        public List<String> Sub2StrategyNames = new List<string>();
        protected int curSubStategyIndex = -1;
        protected int curSub2StategyIndex = -1;
        public List<GameObject> Objects = new List<GameObject>();
        public ObjectStylingStrategyInfo curInfo = null;

        protected ObjectStylingStrategySkeleton osSkeleton = new ObjectStylingStrategySkeleton();
        protected List<ObjectStylingWorldTransInfo> objectWorldInfos = new List<ObjectStylingWorldTransInfo>();
        
        protected ObjectStylingStrategyBase(ObjectStylingStrategyInfo info)
        {
            curInfo = info;
            Init();
        }

        public virtual void Init()
        {
            osSkeleton.Init(curInfo.MaxLayer);
        }

        public virtual void Release()
        {
            osSkeleton.Release();
        }

        // 使用哪种套路的骨骼信息
        public abstract void ApplySubStrategy(int subStategyIndex);
        public abstract void ApplySub2Strategy(int sub2StategyIndex);
        
        // 加载具体哪种objects信息
        public virtual void LoadObject(string objectFilePath)
        {
            Object prefab0 = null;
            foreach (var item in Objects)
            {
                Object.DestroyImmediate(item);
            }
            Objects.Clear();
            prefab0 = AssetDatabase.LoadAssetAtPath<Object>(objectFilePath);
            if (objectWorldInfos == null || prefab0 == null) return;
            Objects.Add(Utility.InstantiateObject(prefab0));
            stylingObejcts();
        }
        
        
        // 通过文件名称构建ObjectStringPath
        protected virtual ObjectStringPath getObjectStringPath(string modelName)
        {
            string assetPath = modelName.Replace(Application.dataPath, "");
            int lastIndex = assetPath.LastIndexOf("/", StringComparison.Ordinal);
            string lastString = assetPath.Substring(lastIndex + 1, assetPath.Length - lastIndex - 1);
            return new ObjectStringPath()
            {
                FilePath = assetPath,
                FilterName = lastString
            };
        }
        
        protected virtual void stylingObejcts()
        {
            if (objectWorldInfos.Count == 0 || Objects.Count == 0) return;
            for (int index = 0; index < Objects.Count; index++)
            {
                if (objectWorldInfos.Count < index + 1) break;
                Objects[index].transform.SetParent(osSkeleton.GetLayerTransform(objectWorldInfos[index].SkeletonLayer));
                Objects[index].transform.position = objectWorldInfos[index].Position;
                Objects[index].transform.eulerAngles = objectWorldInfos[index].Rotation;
                Objects[index].transform.localScale = objectWorldInfos[index].Scale;
            }
        }
    }
}