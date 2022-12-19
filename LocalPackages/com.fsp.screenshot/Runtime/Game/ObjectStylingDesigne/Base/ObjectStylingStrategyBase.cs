using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public virtual void RotateZeroTransY(Vector2 rotate)
        {
            if (objectWorldInfos.Count == 0 || Objects.Count == 0) return;
            Objects[0].transform.eulerAngles = Objects[0].transform.eulerAngles.AddY(rotate.x);
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
            GameObject go = Utility.InstantiateObject(prefab0);
            Animator animator = go.GetComponent<Animator>();
            if (animator != null)
            {
                animator.runtimeAnimatorController = null;
            }
            Objects.Add(go);
            stylingObejcts();
        }
        
        
        // 通过文件名称构建ObjectStringPath
        protected virtual ObjectStringPath getObjectStringPath(string modelName)
        {
            string assetPath = modelName.Replace(Application.dataPath, "");
            assetPath = assetPath.Replace('\\', '/');
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

            for (int layerIndex = 0; layerIndex < 3; layerIndex++)
            {
                if (objectWorldInfos.Count < layerIndex + 1) break;
                Transform parent = osSkeleton.GetLayerTransform(objectWorldInfos[layerIndex].SkeletonLayer);
                if(parent == null) continue;
                parent.position = Vector3.zero;
                parent.localScale = Vector3.one;
            }
            
            for (int index = 0; index < Objects.Count; index++)
            {
                if (objectWorldInfos.Count < index + 1) break;
                GameObject go = Objects[index];
                go.transform.SetParent(osSkeleton.GetLayerTransform(objectWorldInfos[index].SkeletonLayer));
                go.transform.position = objectWorldInfos[index].Position;
                go.transform.eulerAngles = objectWorldInfos[index].Rotation;
                go.transform.localScale = objectWorldInfos[index].Scale;
                if (objectWorldInfos[index].IsScaleObj)
                {
                    Bounds bounds = Utility.GetGoRendererBounds(go);
                    if (bounds.size != Vector3.zero)
                    {
                        float minf = Mathf.Min(Mathf.Min(1 / bounds.extents.x, 1 / bounds.extents.y), 1 / bounds.extents.z);
                        var parent = go.transform.parent;
                        parent.localScale = new Vector3(minf, minf, minf);
                        Bounds newBounds = Utility.GetGoRendererBounds(go);
                        parent.position -= newBounds.center;
                    }
                }
            }
        }
        
        protected void addOSP(string mobDirectory, List<ObjectStringPath> listPath)
        {
            IEnumerable<string> filePaths = Directory.GetFiles(mobDirectory, "*.*",
                SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".FBX") || s.EndsWith(".fbx"));

            foreach (var item in filePaths)
            {
                string filePath = item.Replace('\\', '/');
                if (!File.Exists(filePath)) continue;
                ObjectStringPath objectStringPath = getObjectStringPath(filePath);
                listPath.Add(objectStringPath);
            }
        }

    }
}