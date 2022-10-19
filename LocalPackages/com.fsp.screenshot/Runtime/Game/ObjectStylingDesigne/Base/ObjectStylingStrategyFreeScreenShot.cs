using System;
using System.Collections.Generic;
using System.IO;
using fsp.utility;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace fsp.ObjectStylingDesigne
{
    public class ObjectStylingStrategyFreeScreenShot : ObjectStylingStrategyBase
    {
        public List<ObjectStringPath> ObjectNameList  = new List<ObjectStringPath>();
        public Dictionary<ObjectStringPath, GameObject> DictObjects  = new Dictionary<ObjectStringPath, GameObject>();
        
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

        public void FakeLoadObject(string objectFilePath)
        {
            Object prefab0 = null;
            objectFilePath = objectFilePath.Replace('\\', '/');
            prefab0 = AssetDatabase.LoadAssetAtPath<Object>(objectFilePath);
            if (prefab0 == null) return;
            foreach (var objectStringPath in ObjectNameList)
            {
                if (string.Equals(objectStringPath.FilePath, objectFilePath, StringComparison.Ordinal))
                {
                    objectFilePath += "(Clone)";
                }
            }
            ObjectNameList.Add(getObjectStringPath(objectFilePath));
        }
        
        public void RealLoadObject(ObjectStringPath data)
        {
            Object prefab0 = null;
            string file = data.FilePath.Replace("(Clone)", "");
            prefab0 = AssetDatabase.LoadAssetAtPath<Object>(file);
            if (objectWorldInfos == null || prefab0 == null) return;
            GameObject go = Utility.InstantiateObject(prefab0, Vector3.zero, Quaternion.identity, null);
            Objects.Add(go);
            stylingObejcts();
            DictObjects.Add(data, go);
        }
        
        public override void LoadObject(string objectFilePath)
        {
            Object prefab0 = null;
            objectFilePath = objectFilePath.Replace('\\', '/');
            prefab0 = AssetDatabase.LoadAssetAtPath<Object>(objectFilePath);
            if (objectWorldInfos == null || prefab0 == null) return;
            Objects.Add(Utility.InstantiateObject(prefab0, Vector3.zero, Quaternion.identity, null));
            stylingObejcts();
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
            Object prefab0 = null;
            foreach (var dictObject in DictObjects)
            {
                if (dictObject.Key.Equals(data))
                {
                    prefab0 = dictObject.Value;
                    Objects.Remove(dictObject.Value);
                    break;
                }
            }
            Object.DestroyImmediate(prefab0);
            DictObjects.Remove(data);
            ObjectNameList.Remove(data);
            stylingObejcts();
        }

        public void DestoryAllObjects()
        {
            foreach (var item in Objects)
            {
                Object.DestroyImmediate(item);
            }
            Objects.Clear();
            ObjectNameList.Clear();
            DictObjects.Clear();
        }

        public GameObject GetObjectByData(ObjectStringPath data)
        {
            foreach (var dictObject in DictObjects)
            {
                if (dictObject.Key.Equals(data))
                {
                    return dictObject.Value;
                }
            }
            return null;
        }
    }
}