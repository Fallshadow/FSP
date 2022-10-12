using System.Collections.Generic;
using System.IO;
using fsp.utility;
using UnityEditor;
using UnityEngine;

namespace fsp.ObjectStylingDesigne
{
    public class ObjectStylingStrategyFreeScreenShot : ObjectStylingStrategyBase
    {
        public List<ObjectStringPath> ObjectNameList  = new List<ObjectStringPath>();
        
        public ObjectStylingStrategyFreeScreenShot(ObjectStylingStrategyInfo info) : base(info)
        {
            
        }

        public override void ApplySubStrategy(int subStategyIndex)
        {
            curSubStategyIndex = subStategyIndex;
            objectWorldInfos = ObjectWorldInfoSO.Instance.GetObjectStylingWorldTransInfos("默认方案");
        }

        public override void ApplySub2Strategy(int sub2StategyIndex)
        {

        }

        public override void LoadObject(string objectFilePath)
        {
            Object prefab0 = null;
            objectFilePath = objectFilePath.Replace('\\', '/');
            prefab0 = AssetDatabase.LoadAssetAtPath<Object>(objectFilePath);
            if (objectWorldInfos == null || prefab0 == null) return;
            Objects.Add(Utility.InstantiateObject(prefab0, Vector3.zero, Quaternion.identity, null));
            stylingObejcts();
            ObjectNameList.Add(getObjectStringPath(objectFilePath));
        }

        public int GetObjectIndex(ObjectStringPath path)
        {
            for (int index = 0; index < ObjectNameList.Count; index++)
            {
                if (ObjectNameList[index].Equals(path))
                {
                    return index;
                }
            }

            return -1;
        }
        
        public void DestoryObjectByData(ObjectStringPath data)
        {
            DestoryObjectByIndex(GetObjectIndex(data));
        }
        
        public void DestoryObjectByIndex(int index)
        {
            if (index >= Objects.Count || index <= 0) return;
            Object prefab0 = Objects[index];
            Object.DestroyImmediate(prefab0);
            Objects.RemoveAt(index);
            stylingObejcts();
        }

        public void DestoryAllObjects()
        {
            foreach (var item in Objects)
            {
                Object.DestroyImmediate(item);
            }
            Objects.Clear();
        }

        public GameObject GetObjectByData(ObjectStringPath data)
        {
            int index = GetObjectIndex(data);
            if (index >= Objects.Count || index <= 0) return null;
            return Objects[index];
        }
    }
}