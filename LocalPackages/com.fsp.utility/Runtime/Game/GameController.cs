using System.Collections;
using fsp.assetbundlecore;
using fsp.debug;
using UnityEngine;

namespace fsp.utility
{
    public class GameController : SingletonMonoBehaviorNoDestroy<GameController>
    {
        public readonly Fsm<GameController> FSM = new Fsm<GameController>();

        private IEnumerator Start()
        {
            SetManagersActive(false);
            InitResourceLoaderProxy();
            while (!IsResourceLoaderProxyInitOk())
            {
                yield return null;
            }
            initialize();
            SetManagersActive(true);
        }

        protected virtual void InitResourceLoaderProxy()
        {
            if (DebugConfig.instance.IsDisableAssetBundle)
            {
                ResourceLoaderProxy.instance.InitEditorLoadManager(new FastModeLoaderManager());
            }
            else
            {
                ResourceLoaderProxy.instance.InitRunTimeLoadManager();
            }
        }
        
        protected virtual bool IsResourceLoaderProxyInitOk()
        {
            return ResourceLoaderProxy.instance.IsOK;
        }
    
        protected virtual void initialize()
        {
            // FSM.SwitchToState();
        }

        private void SetManagersActive(bool isActive)
        {
            MonoBehaviour[] managers = GetComponents<MonoBehaviour>();
            for (int i = 0; i < managers.Length; ++i)
            {
                managers[i].enabled = isActive;
            }
        }
        
        private void FixedUpdate()
        {
            FSM.FixedUpdate();
        }

        private void Update()
        {
            FSM.Update();
        }

        private void LateUpdate()
        {
            FSM.LateUpdate();
        }
        
        private void OnDestroy()
        {
            FSM.Finalize();
        }
    }
}