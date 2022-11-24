using System.Collections.Generic;
using UnityEngine;

namespace fsp.ObjectStylingDesigne
{
    // 结构 root -> layer_0 和 所有layer_0的物体 -> layer_1 和 所有layer_1的物体……
    public class ObjectStylingStrategySkeleton
    {
        public GameObject Root;
        private List<Transform> layerTranses = new List<Transform>();
        
        public void Init(int layerCount)
        {
            layerTranses.Clear();
            
            Root = new GameObject("ObjectStylingStrategySkeletonRoot");
            layerTranses.Add(Root.transform);
            for (int index = 0; index < layerCount; index++)
            {
                GameObject subLayerGO = new GameObject($"Layer_{index}");
                subLayerGO.transform.SetParent(layerTranses[index]);
            }
        }

        public Transform GetLayerTransform(int layerIndex)
        {
            return layerIndex >= layerTranses.Count ? null : layerTranses[layerIndex];
        }

        public void Release()
        {
            if (layerTranses.Count == 0) return;
            if (layerTranses[0] != null)
            {
                Object.Destroy(layerTranses[0]?.gameObject);
            }
            layerTranses.Clear();
        }

        public void RotateRootY(float y)
        {
            Root.transform.eulerAngles = Root.transform.eulerAngles.AddY(y);
        }
    }
}