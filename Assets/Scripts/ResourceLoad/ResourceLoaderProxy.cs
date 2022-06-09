using UnityEditor;
using UnityEngine;

public class ResourceLoaderProxy : Singleton<ResourceLoaderProxy>
{
    public T LoadAsset<T>(string assetPath) where T : Object
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }
#endif
        // TODO：加载资源打包出来
        // int hash = ResourceUtility.GetHashCodeByAssetPath(assetPath);
        // return manager.LoadAsset<T>(hash);
        return null;
    }
}
