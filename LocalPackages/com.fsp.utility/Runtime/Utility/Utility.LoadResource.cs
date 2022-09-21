using fsp.debug;
using UnityEngine;

namespace fsp.utility
{
    public static partial class Utility
    {
        public static GameObject InstantiateObject(Object original,
            Vector3 position,
            Quaternion rotation,
            Transform parent)
        {
            return Object.Instantiate(original, position, rotation, parent) as GameObject;
        }
        
        public static GameObject LoadPrefab(string path, Transform parent, bool checkExisted = false)
        {
            Object obj = Resources.Load(path);
            if (checkExisted)
            {
                if (obj == null)
                {
                    PrintSystem.LogError("LoadResources.LoadPrefab Path = " + path + " , Not Found!");
                    return null;
                }
            }

            return InstantiateObject(obj, parent);
        }

        public static GameObject InstantiateObject(Object obj, Transform parent)
        {
            GameObject go = InstantiateObject(obj);
            go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            return go;
        }
        
        public static GameObject InstantiateObject(Object obj)
        {
            return Object.Instantiate(obj) as GameObject;
        }
        
        
    }
}