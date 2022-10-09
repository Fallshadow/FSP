using UnityEngine;

namespace fsp.LittleSceneEnvironment
{
    public class LittleEnvironmentCreator : SingletonMonoBehavior<LittleEnvironmentCreator>
    {
        private GameObject curEnvironmentPrefab = null;

        public void SwitchToEnvironment(string environmentName)
        {
            if(curEnvironmentPrefab != null) DestroyImmediate(curEnvironmentPrefab);
            curEnvironmentPrefab = LittleEnvironmentSO.Instance.LoadEnvironment(environmentName);
        }
    }
}